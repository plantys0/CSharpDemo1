
// Security/SecurityConfiguration.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankingAPI.Security
{
    public static class SecurityConfiguration
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            // Log authentication failures
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                            logger.LogWarning("Authentication failed: {Message}", context.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });

            // Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomerOnly", policy =>
                    policy.RequireRole("Customer"));

                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("ManagerOrAdmin", policy =>
                    policy.RequireRole("Manager", "Admin"));
            });

            return services;
        }
    }
}

// Security/RateLimitingMiddleware.cs
using System.Collections.Concurrent;

namespace BankingAPI.Security
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private static readonly ConcurrentDictionary<string, List<DateTime>> _requests = new();
        private readonly int _maxRequests = 100; // requests per minute
        private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientId = GetClientIdentifier(context);
            var currentTime = DateTime.UtcNow;

            _requests.AddOrUpdate(clientId, 
                new List<DateTime> { currentTime },
                (key, existingRequests) =>
                {
                    // Remove old requests outside the time window
                    existingRequests.RemoveAll(req => currentTime - req > _timeWindow);
                    existingRequests.Add(currentTime);
                    return existingRequests;
                });

            if (_requests[clientId].Count > _maxRequests)
            {
                _logger.LogWarning("Rate limit exceeded for client: {ClientId}", clientId);
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsync("Rate limit exceeded");
                return;
            }

            await _next(context);
        }

        private string GetClientIdentifier(HttpContext context)
        {
            return context.User.Identity?.Name ?? 
                   context.Connection.RemoteIpAddress?.ToString() ?? 
                   "unknown";
        }
    }
}

// Security/EncryptionService.cs
using System.Security.Cryptography;
using System.Text;

namespace BankingAPI.Security
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        string HashData(string data);
        bool VerifyHash(string data, string hash);
    }

    public class EncryptionService : IEncryptionService
    {
        private readonly string _encryptionKey;

        public EncryptionService(IConfiguration configuration)
        {
            _encryptionKey = configuration["Encryption:Key"] ?? throw new ArgumentNullException("Encryption key not configured");
        }

        public string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32).Substring(0, 32));
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            var buffer = Convert.FromBase64String(cipherText);

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32).Substring(0, 32));

                var iv = new byte[16];
                Array.Copy(buffer, 0, iv, 0, iv.Length);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(buffer, 16, buffer.Length - 16))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string HashData(string data)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyHash(string data, string hash)
        {
            return HashData(data) == hash;
        }
    }
}

// Security/AuditingMiddleware.cs
using System.Text.Json;

namespace BankingAPI.Security
{
    public class AuditingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditingMiddleware> _logger;

        public AuditingMiddleware(RequestDelegate next, ILogger<AuditingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            var requestBody = await ReadRequestBody(context.Request);

            await _next(context);

            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;

            var auditLog = new
            {
                Timestamp = startTime,
                UserId = context.User.FindFirst("userId")?.Value,
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                StatusCode = context.Response.StatusCode,
                Duration = duration.TotalMilliseconds,
                ClientIP = context.Connection.RemoteIpAddress?.ToString(),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                RequestBody = SanitizeRequestBody(requestBody, context.Request.Path)
            };

            _logger.LogInformation("API Audit: {AuditLog}", JsonSerializer.Serialize(auditLog));
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            if (request.ContentLength == null || request.ContentLength == 0)
                return string.Empty;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            request.Body.Position = 0;

            return Encoding.UTF8.GetString(buffer);
        }

        private string SanitizeRequestBody(string requestBody, string path)
        {
            // Don't log sensitive data
            if (path.Contains("auth", StringComparison.OrdinalIgnoreCase) ||
                path.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                return "[REDACTED]";
            }

            return requestBody;
        }
    }
}

// Compliance/PciDssConfiguration.cs
namespace BankingAPI.Compliance
{
    public static class PciDssConfiguration
    {
        public static IServiceCollection AddPciDssCompliance(this IServiceCollection services)
        {
            // Add data protection for sensitive data
            services.AddDataProtection()
                .PersistKeysToAzureBlobStorage(/* Azure blob storage configuration */)
                .ProtectKeysWithAzureKeyVault(/* Key Vault configuration */);

            // Add HSTS for secure connections
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            return services;
        }

        public static IApplicationBuilder UsePciDssCompliance(this IApplicationBuilder app)
        {
            // Require HTTPS
            app.UseHttpsRedirection();
            app.UseHsts();

            // Security headers
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                context.Response.Headers.Add("Content-Security-Policy", 
                    "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:;");

                await next();
            });

            return app;
        }
    }
}

// Configuration/KeyVaultConfiguration.cs
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BankingAPI.Configuration
{
    public static class KeyVaultConfiguration
    {
        public static IConfigurationBuilder AddAzureKeyVault(this IConfigurationBuilder builder, string keyVaultUrl)
        {
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(new Uri(keyVaultUrl), credential);

            return builder.AddAzureKeyVault(client, new KeyVaultSecretManager());
        }
    }
}

// Updated Program.cs with Security
using BankingAPI.Security;
using BankingAPI.Compliance;
using BankingAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add Azure Key Vault if in production
if (builder.Environment.IsProduction())
{
    var keyVaultUrl = builder.Configuration["AzureKeyVault:KeyVaultUrl"];
    if (!string.IsNullOrEmpty(keyVaultUrl))
    {
        builder.Configuration.AddAzureKeyVault(keyVaultUrl);
    }
}

// Add services
builder.Services.AddControllers();
builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddPciDssCompliance();

// Register custom services
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

var app = builder.Build();

// Configure pipeline
app.UsePciDssCompliance();
app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<AuditingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

-- Azure Security Configuration
-- Key Vault Access Policies and Secrets

-- Store sensitive configuration in Key Vault
az keyvault secret set --vault-name "your-keyvault" --name "JwtKey" --value "YourSuperSecretKey"
az keyvault secret set --vault-name "your-keyvault" --name "DatabaseConnection" --value "your-connection-string"
az keyvault secret set --vault-name "your-keyvault" --name "EncryptionKey" --value "YourEncryptionKey"

-- Azure SQL Database Security Configuration
-- Enable Azure AD Authentication
ALTER AUTHORIZATION ON SCHEMA::dbo TO [azure-sql-database-ad-admin]

-- Create contained database users for Azure AD
CREATE USER [BankingAPIIdentity] FROM EXTERNAL PROVIDER
GO

-- Grant necessary permissions
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO [BankingAPIIdentity]
GO

-- Enable Transparent Data Encryption
ALTER DATABASE BankingDB SET ENCRYPTION ON
GO

-- Enable Always Encrypted for sensitive columns
-- (This would be configured during table creation)

-- Azure App Service Security Configuration
-- web.config for additional security headers
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <httpProtocol>
      <customHeaders>
        <add name="Strict-Transport-Security" value="max-age=31536000; includeSubDomains; preload" />
        <add name="X-Content-Type-Options" value="nosniff" />
        <add name="X-Frame-Options" value="DENY" />
        <add name="X-XSS-Protection" value="1; mode=block" />
        <add name="Referrer-Policy" value="strict-origin-when-cross-origin" />
      </customHeaders>
    </httpProtocol>
    <security>
      <requestFiltering>
        <requestLimits maxAllowedContentLength="10485760" /> <!-- 10MB -->
      </requestFiltering>
    </security>
  </system.webServer>
</configuration>

-- Monitoring and Compliance Scripts
-- Application Insights Configuration for monitoring
{
  "ApplicationInsights": {
    "InstrumentationKey": "your-instrumentation-key",
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "Compliance": {
    "PciDss": {
      "LogRetentionDays": 365,
      "AuditFailedLogins": true,
      "RequireHttps": true,
      "SessionTimeoutMinutes": 30
    }
  }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http; // Add this at the top of the file
using Microsoft.Extensions.Http; // Optional, but ensures AddHttpClient is available
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient();
                services.AddTransient<IWeatherService, WeatherService>();
                services.AddHostedService<WeatherForecastService>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .RunConsoleAsync();
    }
}

public class WeatherForecastService : BackgroundService
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(IWeatherService weatherService, ILogger<WeatherForecastService> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var forecast = await _weatherService.GetWeatherForecastAsync();
                _logger.LogInformation($"Current temperature: {forecast.Temperature}°C, Condition: {forecast.Condition}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching weather forecast");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}

public interface IWeatherService
{
    Task<WeatherForecast> GetWeatherForecastAsync();
}

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<WeatherForecast> GetWeatherForecastAsync()
    {
        var apiKey = _configuration["OpenWeatherMapApiKey"];
        var city = "London"; // You can make this configurable too
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WeatherData>(content);

        return new WeatherForecast
        {
            Temperature = weatherData.Main.Temp,
            Condition = weatherData.Weather[0].Main
        };
    }
}

public class WeatherForecast
{
    public double Temperature { get; set; }
    public string Condition { get; set; }
}

public class WeatherData
{
    public MainData Main { get; set; }
    public WeatherCondition[] Weather { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
}

public class WeatherCondition
{
    public string Main { get; set; }
}
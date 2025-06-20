using Microsoft.Extensions.DependencyInjection;
//LogProcessor processes log messages using an ILogger and maintains a count of processed logs per instance. It is registered as scoped to provide each scope with a separate instance and state. In web applications, this allows each request to track its own processed logs without interference from other requests.
namespace _DependencyInjectionIOC1
{
    public interface ILogger { void Log(string message); }  //contract for method=Log

    public class FileLogger : ILogger  //implements ILogger to provide a logging mechanism, 
    {
        private readonly string _filePath;
        public FileLogger() { _filePath = $"log_{DateTime.Now.Ticks}.txt"; }
        public void Log(string message) { File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n"); }
    }

    public class LogProcessor  //uses an ILogger for processing logs and tracking counts
    {
        private readonly ILogger _logger;
        public int ProcessedLogsCount { get; private set; }
        public LogProcessor(ILogger logger) { _logger = logger; ProcessedLogsCount = 0; }
        public void ProcessLog(string message) { _logger.Log(message); ProcessedLogsCount++; }
    }

    class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILogger, FileLogger>();
            services.AddScoped<LogProcessor>();

            var provider = services.BuildServiceProvider();
            using (var scope1 = provider.CreateScope())
            {
                var logProcessor1 = scope1.ServiceProvider.GetService<LogProcessor>();
                logProcessor1.ProcessLog("Log 1");
                logProcessor1.ProcessLog("Log 2");
                Console.WriteLine($"Scope 1 processed logs count: {logProcessor1.ProcessedLogsCount}");
            }
            using (var scope2 = provider.CreateScope())
            {
                var logProcessor2 = scope2.ServiceProvider.GetService<LogProcessor>();
                logProcessor2.ProcessLog("Log 3");
                Console.WriteLine($"Scope 2 processed logs count: {logProcessor2.ProcessedLogsCount}");
            }
        }
    }

}
using _DependencyInjectionIOC1; // Imports the custom namespace containing the interfaces and classes defined at the bottom of the file
using Microsoft.Extensions.DependencyInjection; // Imports Microsoft's built-in dependency injection framework that provides ServiceCollection and related classes

var services = new ServiceCollection(); // Creates a new instance of ServiceCollection, which is the container that holds service registrations and their configurations
services.AddSingleton<ILogger, FileLogger>(); // Registers FileLogger as the implementation for ILogger interface with singleton lifetime - only one instance will be created for the entire application lifetime
services.AddScoped<LogProcessor>(); // Registers LogProcessor as a scoped service - a new instance will be created for each scope (typically per request in web apps)

var provider = services.BuildServiceProvider(); // Builds the service provider from the service collection - this creates the actual dependency injection container that can resolve services

using (var scope1 = provider.CreateScope()) // Creates the first scope using the 'using' statement, which ensures proper disposal when the scope ends
{
    var logProcessor1 = scope1.ServiceProvider.GetService<LogProcessor>(); // Retrieves an instance of LogProcessor from the scoped service provider - creates a new LogProcessor instance for this scope
    logProcessor1.ProcessLog("Log 1"); // Calls ProcessLog method which logs the message using the injected ILogger and increments the internal counter
    logProcessor1.ProcessLog("Log 2"); // Processes another log message, incrementing the counter to 2
    Console.WriteLine($"Scope 1 processed logs count: {logProcessor1.ProcessedLogsCount}"); // Displays the count of processed logs for this LogProcessor instance (will show 2)
} // End of scope1 - the LogProcessor instance is disposed here, but the singleton FileLogger remains alive

using (var scope2 = provider.CreateScope()) // Creates a second, separate scope
{
    var logProcessor2 = scope2.ServiceProvider.GetService<LogProcessor>(); // Gets a NEW instance of LogProcessor for this scope (because it's registered as scoped)
    logProcessor2.ProcessLog("Log 3"); // Processes one log message, so this instance's counter will be 1
    Console.WriteLine($"Scope 2 processed logs count: {logProcessor2.ProcessedLogsCount}"); // Displays the count for this separate LogProcessor instance (will show 1)
} // End of scope2 - this LogProcessor instance is also disposed

//LogProcessor processes log messages using an ILogger and maintains a count of processed logs per instance. It is registered as scoped to provide each scope with a separate instance and state. In web applications, this allows each request to track its own processed logs without interference from other requests.

namespace _DependencyInjectionIOC1 // Declares a namespace to organize related classes and interfaces
{
    public interface ILogger { void Log(string message); }  // Defines a contract (interface) that specifies any logging implementation must have a Log method that takes a string parameter //@ what does void mean here

    public class FileLogger : ILogger  // Concrete implementation of ILogger that writes log messages to a file
    {
        private readonly string _filePath; // Private field to store the file path - readonly means it can only be set in constructor or at declaration
        public FileLogger() { _filePath = $"log_{DateTime.Now.Ticks}.txt"; } // Constructor that sets the file path using current date/time ticks to ensure unique filenames
        public void Log(string message) { File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n"); } // Implementation of Log method that appends timestamped messages to the file //@@
    }

    public class LogProcessor  // A service class that processes log messages and tracks how many have been processed
    {
        private readonly ILogger _logger; // Private field to hold the injected logger dependency - readonly ensures it cannot be changed after construction
        public int ProcessedLogsCount { get; private set; } // Auto-property that tracks the number of processed logs - public getter but private setter for encapsulation
        public LogProcessor(ILogger logger) { _logger = logger; ProcessedLogsCount = 0; } // Constructor that receives ILogger dependency and initializes the counter to zero
        public void ProcessLog(string message) { _logger.Log(message); ProcessedLogsCount++; } // Method that logs a message using the injected logger and increments the processed count
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AAA
{
    public class a03_ctor2a_logManager
    {
        public class LogManager : IAsyncDisposable
        {
            // 1. Static fields for global state
            private static readonly int SystemStartYear = DateTime.Now.Year;
            private static int InstanceCount;

            // 2. Properties for configuration
            public string LogLevel { get; private set; } = "INFO";  // Default log level
            public int MaxLogEntries { get; } = 1000;  // Read-only max
            private List<string> LogEntries { get; } = new List<string>();  // Store logs
            public string LogFilePath { get; init; } = "logs.txt";  // Set at creation
            private bool IsLoggingEnabled { get; set; } = true;  // Control logging

            // 3. Event for log notifications
            public event EventHandler<string>? LogAdded;

            // 4. Static constructor for one-time setup
            static LogManager()
            {
                InstanceCount = 0;
                Console.WriteLine("LogManager static constructor: System initialized.");
            }

            // 5. Parameterless constructor with useful setup
            public LogManager()
            {
                InstanceCount++;  // Track instances
                LogEntries.Add($"LogManager #{InstanceCount} started at {DateTime.Now}");
                // Create log file if not exists
                if (!File.Exists(LogFilePath))
                {
                    File.WriteAllText(LogFilePath, $"Log System - Started {DateTime.Now}\n");
                }
                Console.WriteLine("LogManager created with default settings.");
            }

            // 6. Methods for functionality
            public async ValueTask AddLogAsync(string message)
            {
                if (!IsLoggingEnabled) return;
                if (LogEntries.Count >= MaxLogEntries)
                {
                    throw new InvalidOperationException("Log capacity reached!");
                }
                string log = $"[{DateTime.Now}] {LogLevel}: {message}";
                LogEntries.Add(log);
                await File.AppendAllTextAsync(LogFilePath, log + "\n");
                LogAdded?.Invoke(this, log);  // Trigger event
            }

            public void SetLogLevel(string level)
            {
                if (!new[] { "INFO", "DEBUG", "ERROR" }.Contains(level))
                {
                    throw new ArgumentException("Invalid log level!");
                }
                LogLevel = level;
            }

            public int GetLogCount() => LogEntries.Count;

            // 7. IDisposable for cleanup
            private bool _disposed;
            public async ValueTask DisposeAsync()
            {
                if (!_disposed)
                {
                    LogEntries.Clear();
                    IsLoggingEnabled = false;
                    LogEntries.Add($"LogManager #{InstanceCount} disposed at {DateTime.Now}");
                    await File.AppendAllTextAsync(LogFilePath, LogEntries[^1] + "\n");
                    _disposed = true;
                }
            }

            // 8. Finalizer for safety
            ~LogManager()
            {
                if (!_disposed)
                {
                    LogEntries.Clear();
                    IsLoggingEnabled = false;
                    LogEntries.Add($"LogManager #{InstanceCount} disposed at {DateTime.Now} (finalizer)");
                    File.AppendAllText(LogFilePath, LogEntries[^1] + "\n");  // Sync for finalizer
                    _disposed = true;
                }
            }

            // 9. Static method to get instance count
            public static int GetInstanceCount() => InstanceCount;

        }
        // Test program
        /*
                    public static void Main()
                    {
                        Console.WriteLine("=== Testing LogManager ===");
                        var logger = new LogManager();  // Parameterless ctor
                        logger.AddLog("System started.");
                        logger.LogAdded += (sender, log) => Console.WriteLine($"New log: {log}");
                        logger.AddLog("User logged in.");
                        logger.SetLogLevel("DEBUG");
                        logger.AddLog("Debugging mode enabled.");
                        Console.WriteLine($"Total logs: {logger.GetLogCount()}");
                        Console.WriteLine($"Instances created: {LogManager.GetInstanceCount()}");
                        logger.Dispose();
                    }
        */
        //Following is an alternate to the above code.
        public static async Task Main()
        {
            Console.WriteLine("=== Testing LogManager ===");
            await using (var logger = new LogManager())  // Alternate: Uses 'using' instead of manual Dispose
            {
                await logger.AddLogAsync("System started.");
                logger.LogAdded += (sender, log) => Console.WriteLine($"New log: {log}");
                await logger.AddLogAsync("User logged in.");
                logger.SetLogLevel("DEBUG");
                await logger.AddLogAsync("Debugging mode enabled.");
                Console.WriteLine($"Total logs: {logger.GetLogCount()}");
                Console.WriteLine($"Instances created: {LogManager.GetInstanceCount()}");
            }  // Auto-calls Dispose() here
        }





    }
}

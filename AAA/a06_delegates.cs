// Add necessary using directives for file I/O and debug output
using System;
using System.IO;
using System.Diagnostics;

namespace AAA
{
    public class a06_delegates
    {
        // Define Severity enum for log levels
        public enum Severity
        {
            Verbose = 0,
            Information = 1,
            Warning = 2,
            Error = 3,
            Critical = 4
        }

        // Logger class: Acts as the "boss" that triggers logging via a delegate.
        // The delegate allows flexible attachment of different logging methods without changing this class.
        public static class Logger
        {
            // Delegate field: Action<string> is a pointer to methods that take a string and return void.
            // Nullable (?) so it can start as null.
            public static Action<string>? WriteMessage;

            // Log level property: Default to Warning
            public static Severity LogLevel { get; set; } = Severity.Warning;

            // Method to log a message: Checks level, formats, then invokes delegate if attached.
            public static void LogMessage(Severity s, string component, string msg)
            {
                if (s < LogLevel) return;
                var outputMsg = $"{DateTime.Now}\t{s}\t{component}\t{msg}";
                if (WriteMessage is not null)
                    WriteMessage(outputMsg);  // Invokes the attached method(s) with formatted message.
            }
        }

        // LoggingMethods class: Holds different worker methods for logging.
        // Each method matches the delegate signature: void Method(string).
        // This separation shows modularity - you can add/swap methods easily.
        public static class LoggingMethods
        {
            // Logs to console error stream (visible in console window).
            public static void LogToConsole(string message)
            {
                Console.Error.WriteLine(message);
            }

            // Logs to a file: Appends message to 'log.txt' in the current directory.
            // Demonstrates a different logging destination.
            public static void LogToFile(string message)
            {
                File.AppendAllText("log.txt", message + Environment.NewLine);
            }

            // Logs to debug output: Visible in VS Output window under Debug.
            // Useful for development logging without console or file.
            public static void LogToDebug(string message)
            {
                Debug.WriteLine(message);
            }
        }

        // FileLogger class: Instance-based logger that attaches to delegate.
        // Handles file logging with error tolerance.
        public class FileLogger
        {
            private readonly string logPath;
            public FileLogger(string path)
            {
                logPath = path;
                Logger.WriteMessage += LogMessage;
            }
            public void DetachLog() => Logger.WriteMessage -= LogMessage;
            // make sure this can't throw.
            private void LogMessage(string msg)
            {
                try
                {
                    using (var log = File.AppendText(logPath))
                    {
                        log.WriteLine(msg);
                        log.Flush();
                    }
                }
                catch (Exception)
                {
                    // Hmm. We caught an exception while
                    // logging. We can't really log the
                    // problem (since it's the log that's failing).
                    // So, while normally, catching an exception
                    // and doing nothing isn't wise, it's really the
                    // only reasonable option here.
                }
            }
        }

        // Main method: Entry point to demonstrate delegate benefits.
        // Shows switching between logging methods, multicast, severity filtering, and FileLogger.
        public static void Main()
        {
            // Set log level to Information for demo (logs Info and above)
            Logger.LogLevel = Severity.Information;

            // Attach console logging
            Logger.WriteMessage += LoggingMethods.LogToConsole;
            Logger.LogMessage(Severity.Warning, "Main", "Test1: This goes to console.");  // Output: Console

            // Demonstrate severity filtering: Verbose skipped since level is Information
            Logger.LogMessage(Severity.Verbose, "Main", "TestVerbose: This should NOT log.");

            // Detach console, attach debug
            Logger.WriteMessage -= LoggingMethods.LogToConsole;
            Logger.WriteMessage += LoggingMethods.LogToDebug;
            Logger.LogMessage(Severity.Error, "Main", "Test2: This goes to debug.");  // Output: VS Debug window

            // Create FileLogger instance (attaches itself to delegate)
            var fileLogger = new FileLogger("log.txt");
            Logger.LogMessage(Severity.Critical, "Main", "Test3: This goes to debug and file.");  // Output: Debug + file

            // Multicast: Add console too
            Logger.WriteMessage += LoggingMethods.LogToConsole;
            Logger.LogMessage(Severity.Information, "Main", "Test4: This goes to debug, console, and file.");  // Multiple outputs

            // Detach FileLogger and clean up
            fileLogger.DetachLog();

            /*
            In main, Logger.WriteMessage += LoggingMethods.LogToFile; This connects delegate to the method. Method can be switched at runtime.
            Note: Fixed comment - += attaches, -= detaches.
            */
        }
    }
}
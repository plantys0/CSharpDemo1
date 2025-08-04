// Add necessary using directives for file I/O and debug output
using System;
using System.IO;
using System.Diagnostics;

namespace AAA
{
    public class a06_delegates
    {
        // Logger class: Acts as the "boss" that triggers logging via a delegate.
        // The delegate allows flexible attachment of different logging methods without changing this class.
        public static class Logger
        {
            // Delegate field: Action<string> is a pointer to methods that take a string and return void.
            // Nullable (?) so it can start as null.
            public static Action<string>? WriteMessage;

            // Method to log a message: Checks if delegate is attached, then invokes it.
            // This stays the same no matter which logging method is attached.
            public static void LogMessage(string msg)
            {
                if (WriteMessage is not null)
                    WriteMessage(msg);  // Invokes the attached method(s) with the message.
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

        // Main method: Entry point to demonstrate delegate benefits.
        // Shows switching between logging methods and multicast (multiple at once).
        public static void Main()
        {
            // Attach first logging method: Log to console.
            // Benefit: Starts with console logging.
            Logger.WriteMessage += LoggingMethods.LogToConsole;
            Logger.LogMessage("Test1: This goes to console.");  // Output: Console

            // Detach console and attach file logging.
            // Benefit: Switch behavior at runtime without changing Logger code.
            Logger.WriteMessage -= LoggingMethods.LogToConsole;
            Logger.WriteMessage += LoggingMethods.LogToFile;
            Logger.LogMessage("Test2: This goes to file.");  // Output: log.txt file

            // Detach file and attach debug logging.
            // Benefit: Another switch - flexibility for different scenarios.
            Logger.WriteMessage -= LoggingMethods.LogToFile;
            Logger.WriteMessage += LoggingMethods.LogToDebug;
            Logger.LogMessage("Test3: This goes to debug.");  // Output: VS Debug window

            // Multicast: Attach multiple methods at once.
            // Benefit: One call logs to multiple places (console + file).
            Logger.WriteMessage += LoggingMethods.LogToConsole;  // Now debug + console
            Logger.WriteMessage += LoggingMethods.LogToFile;     // Now debug + console + file
            Logger.LogMessage("Test4: This goes to debug, console, and file.");  // Multiple outputs

            /*
           In main,  Logger.WriteMessage -= LoggingMethods.LogToFile; This connects delegate to the method. Method can be switched at runtime
            */
        }
    }
}
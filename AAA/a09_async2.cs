using System;
using System.Threading;
using System.Threading.Tasks;

namespace AAA
{
    public class a09_async2
    {
        public static async Task Main(string[] args)
        {

            System.Console.WriteLine("Starting demos...");

            //await SimpleAsyncAwait();
            //await AsyncWithReturn();
            //await MultipleAwaits();
            //await ParallelTasks();
            //await ErrorHandling();
            //await CancellationDemo();
            await SimpleCancellationTest();  // Calling the new simple test

            System.Console.WriteLine("All demos complete. Press Enter to exit.");
            System.Console.ReadLine();
        }

        #region #1 Simplest Implementation
        /*
         * Detailed Explanation:
         * This is the basic example of async/await in C# .NET 8.
         * - Marks a method as 'async Task' to allow awaiting inside.
         * - 'await Task.Delay(1000)' simulates waiting 1 second (e.g., for I/O like a web request) without blocking the main thread—keeps your app responsive.
         * - Code before await runs synchronously (prints "Start").
         * - At await, method pauses but thread is free; resumes after delay to print "End".
         * - Great for UI apps in Visual Studio 2022 to avoid freezing.
         * - Without async, use Thread.Sleep—blocks everything, bad practice!
         */
        static async Task SimpleAsyncAwait()
        {
            System.Console.WriteLine("Simple: Start");
            await Task.Delay(1000); // Wait 1 second without blocking
            System.Console.WriteLine("Simple: End after delay");
        }
        #endregion

        #region #2 With Return Value
        /*
         * Detailed Explanation:
         * Builds on #1: Async methods can return values via 'Task<T>' (here, Task<int>).
         * - 'GetNumberAsync' awaits a delay, then returns 42.
         * - In 'AsyncWithReturn', await gets the result when ready, like waiting for data from a SQL query in SSMS.
         * - Synchronous part: Prints "Start" immediately.
         * - Await yields control during wait.
         * - Use for fetching data async, e.g., Entity Framework in .NET 8 with SQL Server 2025.
         * - Alternative: Task.Result blocks—avoid for responsiveness.
         */
        static async Task AsyncWithReturn()
        {
            int result = await GetNumberAsync();
            System.Console.WriteLine($"With Return: Got {result}");
        }

        static async Task<int> GetNumberAsync()
        {
            System.Console.WriteLine("With Return: Start");
            await Task.Delay(500);
            return 42;
        }
        #endregion

        #region #3 Multiple Awaits
        /*
         * Detailed Explanation:
         * Shows chaining multiple awaits in sequence.
         * - Each await runs one after another: First delay 300ms, print, second 300ms, print.
         * - Total time: 600ms, as sequential.
         * - Useful for workflows like "await fetch data, then await process it".
         * - Synchronous start: Prints "Multiple: Start" right away.
         * - Each await frees thread during wait—ideal for non-blocking in console or WinForms apps.
         * - In SQL context: Like awaiting two async queries in EF Core.
         */
        static async Task MultipleAwaits()
        {
            System.Console.WriteLine("Multiple: Start");
            await Task.Delay(300);
            System.Console.WriteLine("Multiple: After first delay");
            await Task.Delay(300);
            System.Console.WriteLine("Multiple: After second delay");
        }
        #endregion

        #region #4 Parallel Tasks
        /*
         * Detailed Explanation:
         * Demonstrates running tasks in parallel for efficiency.
         * - Starts two delays (800ms and 500ms) without awaiting immediately.
         * - Uses .ContinueWith for callbacks (prints "done" when each finishes).
         * - 'await Task.WhenAll' waits for both to complete—total time ~800ms (longest one).
         * - Outputs may interleave: Task2 (shorter) finishes first.
         * - Great for independent ops, like parallel SQL queries in .NET 8.
         * - Alternative: Await sequentially—slower (1300ms).
         * - For errors: WhenAll wraps in AggregateException.
         */
        static async Task ParallelTasks()
        {
            System.Console.WriteLine("Parallel: Start");
            Task task1 = Task.Delay(800).ContinueWith(_ => System.Console.WriteLine("Parallel: Task1 done"));
            Task task2 = Task.Delay(500).ContinueWith(_ => System.Console.WriteLine("Parallel: Task2 done"));
            await Task.WhenAll(task1, task2);
            System.Console.WriteLine("Parallel: All done");
        }
        #endregion

        #region #5 Error Handling
        /*
         * Detailed Explanation:
         * Handles exceptions in async code like synchronous try-catch.
         * - 'ThrowErrorAsync' awaits delay, then throws Exception("Oops!").
         * - Await in try-catch: Error bubbles up when task faults.
         * - Catches and prints message.
         * - If no catch, propagates up (could crash app).
         * - For multiple tasks (e.g., WhenAll): Use AggregateException.
         * - In practice: Catch SQL exceptions in async EF Core queries.
         */
        static async Task ErrorHandling()
        {
            try
            {
                await ThrowErrorAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error Handling: Caught {ex.Message}");
            }
        }

        static async Task ThrowErrorAsync()
        {
            await Task.Delay(200);
            throw new Exception("Oops!");
        }
        #endregion

        #region #6 Cancellation
        /*
         * Detailed Explanation:
         * Shows canceling a long-running async task midway.
         * - Creates CancellationTokenSource (CTS)—your "cancel button".
         * - Passes token to LongRunningAsync.
         * - Starts task, waits 900ms, then cts.Cancel().
         * - Await task in try-catch: If canceled, throws OperationCanceledException.
         * - In loop: 'await Task.Delay(200, token)' checks for cancel each time.
         * - Runs partial steps before cancel.
         * - Useful for user-cancelable ops, like stopping a SQL query in SSMS.
         */
        static async Task CancellationDemo()
        {
            var cts = new CancellationTokenSource();
            var task = LongRunningAsync(cts.Token);

            await Task.Delay(900); // Let it run a bit
            cts.Cancel();
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                System.Console.WriteLine("Cancellation: Task canceled");
            }
        }

        static async Task LongRunningAsync(CancellationToken token)
        {
            System.Console.WriteLine("Cancellation: Start");
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(200, token);
                System.Console.WriteLine($"Cancellation: Step {i}");
            }
        }
        #endregion

        #region #7 Cancellation Token Explanation
        /*
         * Detailed Explanation of CancellationToken:
         * - CancellationToken is a struct passed to async methods to signal "stop early".
         * - Created from CancellationTokenSource (CTS)—call cts.Cancel() to trigger.
         * - In code: Pass token to awaits like Task.Delay(..., token)—it checks if canceled and throws OperationCanceledException if yes.
         * - Cooperative: Method must check token (e.g., token.ThrowIfCancellationRequested() in loops).
         * - Why? For responsiveness—e.g., cancel a slow SQL async query in EF Core if user clicks "stop".
         * - Safe: Doesn't force stop threads; just signals.
         * - Alternatives: Timeouts with Task.WhenAny.
         * - In your demo: Token lets delay throw on cancel, stopping loop early.
         * - Test in VS 2022: Adjust delays to see more/less steps before cancel.
         */
        // No executable code here—just for explanation. You can add a demo if needed.
        #endregion

        #region #8 Simple Cancellation Test
        /*
         * Detailed Explanation:
         * Super simple test just for cancellation token—no extras!
         * - Create CTS and token.
         * - Start a loop task with delays (500ms each, 5 steps).
         * - Wait 1.5 secs, then cancel.
         * - Await and catch OperationCanceledException to print "Canceled!".
         * - See it run 2-3 steps before stopping.
         * - Run in VS 2022: Hit F5, watch console.
         */
        static async Task SimpleCancellationTest()
        {
            var cts = new CancellationTokenSource();
            var task = Task.Run(async () =>
            {
                System.Console.WriteLine("Test: Start");
                for (int i = 0; i < 5; i++)
                {
                    await Task.Delay(500, cts.Token);
                    System.Console.WriteLine($"Test: Step {i}");
                }
            }, cts.Token);

            await Task.Delay(1500); // Let it run a bit
            cts.Cancel();
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                System.Console.WriteLine("Test: Canceled!");
            }
        }
        #endregion
    }
}
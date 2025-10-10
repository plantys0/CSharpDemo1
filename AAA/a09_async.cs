using System;
using System.Threading;
using System.Threading.Tasks;

namespace AAA
{
    public class a09_async
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            System.Console.WriteLine("Starting demos...");

            await SimpleAsyncAwait();
            await AsyncWithReturn();
            await MultipleAwaits();
            await ParallelTasks();
            await ErrorHandling();
            await CancellationDemo();

            System.Console.WriteLine("All demos complete. Press Enter to exit.");
            System.Console.ReadLine();
        }

        #region #1 Simplest Implementation
        static async Task SimpleAsyncAwait()
        {
            System.Console.WriteLine("Simple: Start");
            await Task.Delay(1000); // Wait 1 second without blocking
            System.Console.WriteLine("Simple: End after delay");
        }
        #endregion

        #region #2 With Return Value
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
        static async Task CancellationDemo()
        {
            var cts = new CancellationTokenSource();
            var task = LongRunningAsync(cts.Token);

            await Task.Delay(400); // Let it run a bit
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
    }
}
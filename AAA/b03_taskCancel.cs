using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
// For Example 19: Add 'Microsoft.EntityFrameworkCore' via NuGet if testing EF; define MyDbContext/User classes as needed.

namespace AAA
{
    public class b03_taskCancel
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Task examples...");

            await Example1();
            Console.WriteLine("Example 1 done. Press Enter...");
            Console.ReadLine();

            await Example2();
            Console.WriteLine("Example 2 done. Press Enter...");
            Console.ReadLine();

            await Example3();
            Console.WriteLine("Example 3 done. Press Enter...");
            Console.ReadLine();

            await Example4();
            Console.WriteLine("Example 4 done. Press Enter...");
            Console.ReadLine();

            await Example5();
            Console.WriteLine("Example 5 done. Press Enter...");
            Console.ReadLine();

            await Example6();
            Console.WriteLine("Example 6 done. Press Enter...");
            Console.ReadLine();

            await Example7();
            Console.WriteLine("Example 7 done. Press Enter...");
            Console.ReadLine();

            await Example8();
            Console.WriteLine("Example 8 done. Press Enter...");
            Console.ReadLine();

            await Example9();
            Console.WriteLine("Example 9 done. Press Enter...");
            Console.ReadLine();

            await Example10();
            Console.WriteLine("Example 10 done. Press Enter...");
            Console.ReadLine();

            await Example11();
            Console.WriteLine("Example 11 done. Press Enter...");
            Console.ReadLine();

            await Example12();
            Console.WriteLine("Example 12 done. Press Enter...");
            Console.ReadLine();

            await Example13();
            Console.WriteLine("Example 13 done. Press Enter...");
            Console.ReadLine();

            await Example14();
            Console.WriteLine("Example 14 done. Press Enter...");
            Console.ReadLine();

            await Example15();
            Console.WriteLine("Example 15 done. Press Enter...");
            Console.ReadLine();

            await Example16();
            Console.WriteLine("Example 16 done. Press Enter...");
            Console.ReadLine();

            await Example17();
            Console.WriteLine("Example 17 done. Press Enter...");
            Console.ReadLine();

            await Example18();
            Console.WriteLine("Example 18 done. Press Enter...");
            Console.ReadLine();

            await Example19();
            Console.WriteLine("Example 19 done. Press Enter...");
            Console.ReadLine();

            await Example20();
            Console.WriteLine("Example 20 done. Press Enter...");
            Console.ReadLine();

            Console.WriteLine("All examples complete. Press Enter to exit.");
            Console.ReadLine();
        }

        #region Example1: Simple Delay
        static async Task Example1()
        {
            Console.WriteLine("Example1: Starting simple delay...");
            Task delayTask = Task.Delay(1000);  // Create Task for 1 sec wait
            await delayTask;  // Wait async
            Console.WriteLine("Example1: Delay complete!");
        }
        #endregion

        #region Example2: Run Background Work
        static async Task Example2()
        {
            Console.WriteLine("Example2: Starting background work...");
            Task background = Task.Run(() => Console.WriteLine("Background done!"));  // Starts async
            await background;
            Console.WriteLine("Example2: Background complete!");
        }
        #endregion

        #region Example3: From Result
        static async Task Example3()
        {
            Console.WriteLine("Example3: Starting from result...");
            Task<int> quick = Task.FromResult(42);  // No real work
            int result = await quick;
            Console.WriteLine($"Example3: Got {result}!");
        }
        #endregion

        #region Example4: When All
        static async Task Example4()
        {
            Console.WriteLine("Example4: Starting WhenAll...");
            Task t1 = Task.Delay(500);
            Task t2 = Task.Delay(300);
            await Task.WhenAll(t1, t2);  // Both done
            Console.WriteLine("Example4: Both tasks complete!");
        }
        #endregion

        #region Example5: When Any
        static async Task Example5()
        {
            Console.WriteLine("Example5: Starting WhenAny...");
            Task t1 = Task.Delay(800);
            Task t2 = Task.Delay(400);
            Task winner = await Task.WhenAny(t1, t2);  // t2 wins
            Console.WriteLine("Example5: First task complete!");
        }
        #endregion

        #region Example6: Completion Source
        static async Task Example6()
        {
            Console.WriteLine("Example6: Starting TCS...");
            TaskCompletionSource<string> tcs = new();
            tcs.SetResult("Done!");  // Complete it
            string res = await tcs.Task;
            Console.WriteLine($"Example6: Got {res}!");
        }
        #endregion

        #region Example7: Async Method
        static async Task Example7()
        {
            Console.WriteLine("Example7: Starting async method...");
            async Task MyMethod() { await Task.Delay(100); }
            await MyMethod();  // Call it
            Console.WriteLine("Example7: Async method complete!");
        }
        #endregion

        #region Example8: Factory Start
        static async Task Example8()
        {
            Console.WriteLine("Example8: Starting factory...");
            Task<string> factoryTask = Task.Factory.StartNew(() => "Hi");
            string msg = await factoryTask;
            Console.WriteLine($"Example8: Got {msg}!");
        }
        #endregion

        #region Example9: Continue With
        static async Task Example9()
        {
            Console.WriteLine("Example9: Starting continue with...");
            Task original = Task.Delay(200);
            Task continuation = original.ContinueWith(_ => Console.WriteLine("Continued"));
            await continuation;
            Console.WriteLine("Example9: Continuation complete!");
        }
        #endregion

        #region Example10: Exception Task
        static async Task Example10()
        {
            Console.WriteLine("Example10: Starting error task...");
            Task errorTask = Task.Run(() => throw new Exception("Oops"));
            try { await errorTask; } catch { Console.WriteLine("Caught!"); }
            Console.WriteLine("Example10: Handled error!");
        }
        #endregion

        #region Example11: Delay with Token
        static async Task Example11()
        {
            Console.WriteLine("Example11: Starting delay with token...");
            CancellationTokenSource cts = new();
            Task delay = Task.Delay(1000, cts.Token);
            cts.Cancel();  // Stops early
            try { await delay; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion

        #region Example12: Loop with Token
        static async Task Example12()
        {
            Console.WriteLine("Example12: Starting loop with token...");
            async Task LoopAsync(CancellationToken token)
            {
                for (int i = 0; i < 10; i++)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(200);
                    Console.WriteLine($"Step {i}");
                }
            }
            var cts = new CancellationTokenSource();
            var task = LoopAsync(cts.Token);
            cts.CancelAfter(500);
            try { await task; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion

        #region Example13: Http Get with Token
        static async Task Example13()
        {
            Console.WriteLine("Example13: Starting Http with token...");
            using HttpClient client = new();
            CancellationTokenSource cts = new();
            Task<HttpResponseMessage> get = client.GetAsync("https://example.com", cts.Token);
            cts.Cancel();  // If needed
            try { await get; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion

        #region Example14: Run with Token
        static async Task Example14()
        {
            Console.WriteLine("Example14: Starting Run with token...");
            CancellationTokenSource cts = new();
            Task runTask = Task.Run(() => {
                for (int i = 0; i < 5; i++) cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine("Loop done");
            }, cts.Token);
            cts.Cancel();
            try { await runTask; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion

        #region Example15: WhenAll with Token
        static async Task Example15()
        {
            Console.WriteLine("Example15: Starting WhenAll with token...");
            CancellationTokenSource cts = new();
            Task t1 = Task.Delay(300, cts.Token);
            Task t2 = Task.Delay(500, cts.Token);
            var all = Task.WhenAll(t1, t2);
            cts.Cancel();
            try { await all; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion

        #region Example16: Linked Tokens
        static async Task Example16()
        {
            Console.WriteLine("Example16: Starting linked tokens...");
            CancellationTokenSource cts1 = new();
            CancellationTokenSource cts2 = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token);
            Task delay = Task.Delay(1000, cts2.Token);
            cts1.Cancel();  // Cancels both
            try { await delay; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion

        #region Example17: Timeout via Token
        static async Task Example17()
        {
            Console.WriteLine("Example17: Starting timeout...");
            CancellationTokenSource cts = new(800);  // Timeout 800ms
            Task longTask = Task.Delay(1000, cts.Token);
            try { await longTask; } catch (OperationCanceledException) { Console.WriteLine("Timed out"); }
        }
        #endregion

        #region Example18: Manual Check Token
        static async Task Example18()
        {
            Console.WriteLine("Example18: Starting manual check...");
            async Task WorkAsync(CancellationToken token)
            {
                int count = 0;
                while (count < 5)
                {
                    if (token.IsCancellationRequested) { Console.WriteLine("Canceled early!"); return; }
                    await Task.Delay(100);
                    count++;
                }
            }
            var cts = new CancellationTokenSource();
            var task = WorkAsync(cts.Token);
            cts.Cancel();
            await task;
        }
        #endregion

        #region Example19: EF Query with Token
        // Note: Needs EF Core NuGet and setup (e.g., define MyDbContext/User). Comment out if not set up.
        static async Task Example19()
        {
            Console.WriteLine("Example19: Starting EF with token (setup needed)...");
            // using var context = new MyDbContext();  // Assume setup
            CancellationTokenSource cts = new();
            // Task<List<User>> query = context.Users.ToListAsync(cts.Token);
            cts.Cancel();
            // try { await query; } catch (OperationCanceledException) { Console.WriteLine("Canceled!"); }
            Console.WriteLine("Example19: Demo (commented for setup)");
        }
        #endregion

        #region Example20: Custom Task with Token
        static async Task Example20()
        {
            Console.WriteLine("Example20: Starting custom TCS with token...");
            TaskCompletionSource<int> tcs = new();
            CancellationTokenSource cts = new();
            cts.Token.Register(() => tcs.TrySetCanceled());  // Link cancel
            cts.Cancel();
            try { await tcs.Task; } catch (TaskCanceledException) { Console.WriteLine("Canceled!"); }
        }
        #endregion
    }
}
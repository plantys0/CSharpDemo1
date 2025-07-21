using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

class LambdaDemoProgram
{
    static async Task Main(string[] args)
    {
        string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };
        // Aggregate to find the longest fruit name, starting with "banana" as seed.
        //TODO
        //public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> );

    //string longestName1 =fruits.Aggregate()
string longestName = fruits.Aggregate(
            "banana",  // Seed: Initial accumulator value.
            (longest, next) => next.Length > longest.Length ? next : longest,  // Func: Accumulator compares lengths.
            fruit => fruit.ToUpper()  // ResultSelector: Transform to uppercase.
        );
    Console.WriteLine($"The fruit with the longest name is {longestName}.");
        Console.WriteLine("end");



        bool IsEven(int n)
        {
            return n % 2 == 0;
        }
        Func<int, bool> predicate9 = IsEven;
        bool result = predicate9(4); // Calls IsEven(4), returns true
        int[] numbers = { 1, 2, 3, 4, 5 };
        var evens0 = numbers.Where(n => n % 2 == 0);
        var evens1 = numbers.Where(n => { return n % 2 == 0; });
        // Nothing is printed yet because the query is deferred
        foreach (var n in evens1)
        {
            // Now prints: Filtering 1, Filtering 2, Filtering 3, Filtering 4, Filtering 5
        }


        var evens = numbers.Where(n => n % 2 == 0).ToList();  
        evens.ForEach(n => Console.WriteLine(n));  
        Console.WriteLine("end");





















        #region MultiHighComplexity
        /*Output
    Demo 1 Input: 'radar', MinLength:3 | Output: Reversed= radar, Vowels= 2, Palindrome= True
    Demo 2 Input: [10, 25, 5, 100, -3, 42]
            DigitCount = 2, Avg= 25.666666666666668, SumSq= 2489
            DigitCount= 1, Avg= 5, SumSq= 25
            DigitCount= 3, Avg= 100, SumSq= 10000
    Demo 3 Input: Arr1= [1, 2, 3], Arr2= [4, 5, 6]
    Demo 4 Input: 'Hello world, this is a test.' | Most Common Letter: l
    Demo 5 Input: Predicate(multiples of 3), Start=1, End=20 | Sum of Squares: 819
    Demo 6 Input: Dict with apple:5, banana:-2, cherry:10 | Formatted: apple: 5; cherry: 10
    Demo 7 Input: Objects['hello', 123, 'world', 'test']
          Group Odd: Len=5,Hash=-49231396
          Group Even: Len=4,Hash=-2144557414
    Demo 8 Input: Simulated async fetch | Processed Lines: Line1, Line2, Line3
    Demo 9 Input: [1.5, 3.0, 0.5, 4.0] | Squared Above Avg: 9, 16
    Demo 10 Input: Expression for evens | Last 5 Evens in Hex Reversed: 64, 62, 60, 5E, 5C
    .*/
        Console.WriteLine("Lambda Demo Toolkit - Enter 'q' to quit or press Enter to run all demos sequentially.");
        string input = Console.ReadLine();

        if (input?.ToLower() != "q")
        {
            // Demo 1: String processing lambda
            var lambda1 = (string s, int minLength) =>
            {
                if (s?.Length < minLength) return null;
                var vowels = "aeiouAEIOU";
                var count = s.Count(c => vowels.Contains(c));
                return new { Reversed = new string(s.Reverse().ToArray()), VowelCount = count, IsPalindrome = s.SequenceEqual(s.Reverse()) };
            };
            var result1 = lambda1("radar", 3);
            Console.WriteLine($"Demo 1 Input: 'radar', MinLength:3 | Output: Reversed={result1?.Reversed}, Vowels={result1?.VowelCount}, Palindrome={result1?.IsPalindrome}");

            // Demo 2: List<int> processing
            var lambda2 = (List<int> nums) => nums?.Where(n => n > 0).OrderByDescending(n => n % 10).GroupBy(n => n.ToString().Length).Select(g => new { DigitCount = g.Key, Average = g.Average(), SumOfSquares = g.Sum(x => x * x) }).ToDictionary(g => g.DigitCount);
            var input2 = new List<int> { 10, 25, 5, 100, -3, 42 };
            var result2 = lambda2(input2);
            Console.WriteLine("Demo 2 Input: [10,25,5,100,-3,42]");
            // Assuming result2 is defined as before...

            if (result2 != null)
            {
                foreach (var kv in result2)
                {
                    // Your original code inside the loop, e.g.:
                    dynamic item = kv.Value;  // Use dynamic to access anonymous properties
                    Console.WriteLine($"  DigitCount={kv.Key}, Avg={item.Average}, SumSq={item.SumOfSquares}");
                }
            }
            else
            {
                Console.WriteLine("No data available (result2 was null).");
            }

            // Demo 3: Array zipping
            var lambda3 = (int[] arr1, int[] arr2) => arr1.Zip(arr2, (a, b) => a + b).Where(sum => sum % 2 == 0).Select(sum => (sum, Math.Sqrt(sum))).OrderBy(t => t.Item2).ToArray();
            var input3a = new int[] { 1, 2, 3 };
            var input3b = new int[] { 4, 5, 6 };
            var result3 = lambda3(input3a, input3b);
            Console.WriteLine("Demo 3 Input: Arr1=[1,2,3], Arr2=[4,5,6]");
            foreach (var t in result3)
            {
                Console.WriteLine($"  Sum={t.sum}, Sqrt={t.Item2}");
            }

            // Demo 4: Text to common letter
            var lambda4 = (string text) =>
            {
                var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return words.SelectMany(w => w.Where(c => char.IsLetter(c))).GroupBy(c => char.ToLower(c)).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key ?? ' ';
            };
            var input4 = "Hello world, this is a test.";
            var result4 = lambda4(input4);
            Console.WriteLine($"Demo 4 Input: '{input4}' | Most Common Letter: {result4}");

            // Demo 5: Range with predicate
            Func<int, bool> predicate = n => n % 3 == 0;  // Sample predicate: multiples of 3
            var lambda5 = (Func<int, bool> pred, int start, int end) => Enumerable.Range(start, end - start + 1).AsParallel().Where(pred).Select(n => n * n).Aggregate(0L, (acc, sq) => acc + sq);
            var result5 = lambda5(predicate, 1, 20);
            Console.WriteLine($"Demo 5 Input: Predicate(multiples of 3), Start=1, End=20 | Sum of Squares: {result5}");

            // Demo 6: Dictionary processing
            var lambda6 = (Dictionary<string, int> dict) => dict.Where(kv => kv.Value > 0).OrderBy(kv => kv.Key.Length).ThenBy(kv => kv.Key).Select(kv => $"{kv.Key}: {kv.Value.ToString("N0")}").Aggregate((acc, item) => acc + "; " + item);
            var input6 = new Dictionary<string, int> { { "apple", 5 }, { "banana", -2 }, { "cherry", 10 } };
            var result6 = lambda6(input6);
            Console.WriteLine($"Demo 6 Input: Dict with apple:5, banana:-2, cherry:10 | Formatted: {result6}");

            // Demo 7: IEnumerable<object> to lookup
            var lambda7 = (IEnumerable<object> items) => items.OfType<string>().Cast<string>().Select(s => (Length: s.Length, Hash: s.GetHashCode())).Where(t => t.Hash % 2 == 0).ToLookup(t => t.Length % 2 == 0 ? "Even" : "Odd");
            var input7 = new object[] { "hello", 123, "world", "test" };
            var result7 = lambda7(input7);
            Console.WriteLine("Demo 7 Input: Objects ['hello',123,'world','test']");
            foreach (var group in result7)
            {
                Console.WriteLine($"  Group {group.Key}: {string.Join(", ", group.Select(t => $"Len={t.Length},Hash={t.Hash}"))}");
            }

            // Demo 8: Async fetch (simulate with Task)
            Func<string, Task<string>> fetchAsync = async url => { await Task.Delay(100); return "Line1\nLine2 \nLine3\n"; };  // Simulated async fetch
            var lambda8 = (Func<string, Task<string>> fetchAsyncFunc, string url) =>
            {
                try
                {
                    var dataTask = fetchAsyncFunc(url);
                    dataTask.Wait(); // Sync wait for demo
                    var data = dataTask.Result;
                    return data.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToList();
                }
                catch (Exception ex)
                {
                    return new List<string> { $"Error: {ex.Message}" };
                }
            };
            var result8 = lambda8(fetchAsync, "fakeurl.com");
            Console.WriteLine("Demo 8 Input: Simulated async fetch | Processed Lines: " + string.Join(", ", result8));

            // Demo 9: Double array processing
            var lambda9 = (double[] values) => values.Select((v, i) => new { Value = v, Index = i }).Where(x => x.Value > values.Average()).OrderBy(x => x.Index).Select(x => Math.Pow(x.Value, 2)).ToArray();
            var input9 = new double[] { 1.5, 3.0, 0.5, 4.0 };
            var result9 = lambda9(input9);
            Console.WriteLine("Demo 9 Input: [1.5,3.0,0.5,4.0] | Squared Above Avg: " + string.Join(", ", result9));

            // Demo 10: Expression compile
            Expression<Func<int, bool>> expr = n => n % 2 == 0;  // Sample even numbers
            var lambda10 = (Expression<Func<int, bool>> exprParam) =>
            {
                var compiled = exprParam.Compile();
                return Enumerable.Range(1, 100).Where(compiled).Select(n => n.ToString("X")).Reverse().Take(5).ToList();
            };
            var result10 = lambda10(expr);
            Console.WriteLine("Demo 10 Input: Expression for evens | Last 5 Evens in Hex Reversed: " + string.Join(", ", result10));
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("15 Complex Lambda Expression Examples in C# (.NET 8)");




        // use of tuples
        (int Min, int Max) FindMinMax(int[] numbers)
        {
            return (numbers.Min(), numbers.Max()); // Also Sum(), Average(), Count(), Distinct(), OrderBy(), OrderByDescending(), Reverse().

        }

        var result = FindMinMax(new int[] { 3, 7, 1, 9, 4 });
        Console.WriteLine($"Min: {result.Min}, Max: {result.Max}");



        // 1. Recursive factorial calculation
        Func<int, int> factorial = null;
        factorial = n => n <= 1 ? 1 : n * factorial(n - 1);
        Console.WriteLine($"1. Factorial of 5: {factorial(5)}");

        // 2. Higher-order function that returns a function 
        //TODO understand this
        Func<int, Func<int, int>> multiplier = x => y => x * y;
        var triple = multiplier(3);
        Console.WriteLine($"2. Triple of 7: {triple(7)}");

        // 3. Async lambda with multiple parameters
        Func<int, string, Task<bool>> asyncValidator = async (id, name) =>
            await Task.Delay(100).ContinueWith(_ => id > 0 && !string.IsNullOrEmpty(name));
        Console.WriteLine($"3. Async validation result: {await asyncValidator(1, "John")}");

        // 4. LINQ query with multiple conditions
        Func<IEnumerable<int>, IEnumerable<int>> complexFilter = nums =>
            nums.Where(n => n % 2 == 0 && n > 10 && Math.Sqrt(n) % 1 == 0);
        var numbers = new[] { 4, 9, 16, 25, 36, 49, 64, 81, 100 };
        Console.WriteLine($"4. Filtered numbers: {string.Join(", ", complexFilter(numbers))}");

        // 5. Tuple deconstruction in lambda
        Func<(int, int, int), int> maxOfThree = ((int a, int b, int c) t) => Math.Max(t.a, Math.Max(t.b, t.c));
        Console.WriteLine($"5. Max of (10, 5, 8): {maxOfThree((10, 5, 8))}");

        // 6. Pattern matching in lambda
        Func<object, string> typeChecker = obj => obj switch
        {
            int i when i > 0 => "Positive integer",
            string s when s.Length > 5 => "Long string",
            _ => "Unknown type"
        };
        Console.WriteLine($"6. Type check results: {typeChecker(42)}, {typeChecker("Hello, World!")}, {typeChecker(3.14)}");

        // 7. Currying with lambda expressions
        Func<int, Func<int, Func<int, int>>> curryAdd = x => y => z => x + y + z;
        Console.WriteLine($"7. Curried addition: {curryAdd(1)(2)(3)}");

        // 8. Composition of functions
        Func<Func<int, int>, Func<int, int>, Func<int, int>> compose = (f, g) => x => f(g(x));
        Func<int, int> double_ = x => x * 2;
        Func<int, int> addFive = x => x + 5;
        var doubleThenAddFive = compose(addFive, double_);
        Console.WriteLine($"8. Composed function result: {doubleThenAddFive(10)}");

        // 9. Lazy evaluation with lambda
        Func<Func<int>, Lazy<int>> lazyEval = f => new Lazy<int>(f);
        var lazyResult = lazyEval(() => { Console.WriteLine("Evaluating..."); return 42; });
        Console.WriteLine("9. Lazy evaluation:");
        Console.WriteLine($"   Value: {lazyResult.Value}");
        Console.WriteLine($"   Value (cached): {lazyResult.Value}");

        // 10. Generic type constraints in lambda
        //Func<T, bool> isDefault<T> = x => EqualityComparer<T>.Default.Equals(x, default(T));
        //Console.WriteLine($"10. Is default: int(0): {isDefault(0)}, string(empty): {isDefault("")}");

        // 11. Using local functions within lambda
        Func<int, int> fibonacciGenerator = n => {
            int Fib(int x) => x switch
            {
                0 => 0,
                1 => 1,
                _ => Fib(x - 1) + Fib(x - 2)
            };
            return Fib(n);
        };
        Console.WriteLine($"11. 10th Fibonacci number: {fibonacciGenerator(10)}");

        // 12. Lambda with exception handling
        Func<string, int> safeParser = s => {
            try
            {
                return int.Parse(s);
            }
            catch (FormatException)
            {
                return -1;
            }
        };
        Console.WriteLine($"12. Safe parsing: '123': {safeParser("123")}, 'abc': {safeParser("abc")}");

        // 13. Combining multiple lambdas
        Func<int, int, int, int> complexOperation = (a, b, c) =>
            ((Func<int, int>)(x => x * x))(a) +
            ((Func<int, int>)(y => y + 5))(b) +
            ((Func<int, int>)(z => z / 2))(c);
        Console.WriteLine($"13. Complex operation result: {complexOperation(3, 4, 10)}");

        // 14. Using closure in lambda
        Func<int, Func<int, int>> counterGenerator = start => {
            int count = start;
            return increment => {
                count += increment;
                return count;
            };
        };
        var counter = counterGenerator(10);
        Console.WriteLine($"14. Counter: {counter(5)}, {counter(3)}, {counter(2)}");

        // 15. Lambda with dynamic LINQ expression
        var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30 },
            new Person { Name = "Bob", Age = 25 },
            new Person { Name = "Charlie", Age = 35 }
        };
        Func<IEnumerable<Person>, string, IOrderedEnumerable<Person>> dynamicOrderBy =
            (source, propertyName) => propertyName.ToLower() switch
            {
                "name" => source.OrderBy(p => p.Name),
                "age" => source.OrderBy(p => p.Age),
                _ => throw new ArgumentException("Invalid property name")
            };
        var orderedPeople = dynamicOrderBy(people, "Age");
        Console.WriteLine("15. Dynamically ordered people by age:");
        foreach (var person in orderedPeople)
        {
            Console.WriteLine($"   {person.Name}: {person.Age}");
        }
    }
}

// Helper class
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
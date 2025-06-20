using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<string> fruits = new List<string> { "apple", "banana", "cherry", "date", "elderberry" };
        List<Person> people = new List<Person>
        {
            new Person("Alice", 25, "USA"),
            new Person("Bob", 30, "Canada"),
            new Person("Charlie", 35, "UK"),
            new Person("David", 40, "Australia"),
            new Person("Eve", 45, "France")
        };

        // Existing examples rewritten as lambda expressions
        Console.WriteLine("Even numbers: " + string.Join(", ", numbers.Where(n => n % 2 == 0)));
        Console.WriteLine("Squared numbers: " + string.Join(", ", numbers.Select(n => n * n)));
        Console.WriteLine("Sum of numbers: " + numbers.Sum());
        Console.WriteLine("Average of numbers: " + numbers.Average());

        Console.WriteLine("First fruit starting with 'b': " + fruits.First(f => f.StartsWith("b")));
        Console.WriteLine("Any fruit longer than 6 characters? " + fruits.Any(f => f.Length > 6));
        Console.WriteLine("All numbers less than 20? " + numbers.All(n => n < 20));

        var groupedByLength = fruits.GroupBy(f => f.Length);
        Console.WriteLine("Fruits grouped by length:");
        groupedByLength.ToList().ForEach(group => Console.WriteLine($"Length {group.Key}: {string.Join(", ", group)}"));

        Console.WriteLine("Ordered people by age:");
        people.OrderBy(p => p.Age).ToList().ForEach(person => Console.WriteLine($"{person.Name}: {person.Age}"));

        Console.WriteLine("Skip 2, take 3 numbers: " + string.Join(", ", numbers));
        Console.WriteLine("Skip 2, take 3 numbers: " + string.Join(", ", numbers.Skip(2).Take(3)));

        var zippedLists = numbers.Zip(fruits, (n, f) => $"{n}: {f}");
        Console.WriteLine("Zipped numbers and fruits: " + string.Join(", ", zippedLists));

        Console.WriteLine("Distinct even numbers: " + string.Join(", ", numbers.Where(n => n % 2 == 0).Distinct()));

        var aggregateResult = numbers.Aggregate((a, b) => a * b);
        Console.WriteLine("Aggregate (product) of numbers: " + aggregateResult);

        var chunks = numbers.Chunk(3);
        Console.WriteLine("Numbers in chunks of 3:");
        chunks.ToList().ForEach(chunk => Console.WriteLine(string.Join(", ", chunk)));

        // 10 more complex examples
        // 1. SelectMany with custom projection
        var allLetters = fruits.SelectMany(f => f.Select((c, i) => new { Fruit = f, Letter = c, Index = i }));
        Console.WriteLine("All letters with their positions:");
        allLetters.ToList().ForEach(l => Console.WriteLine($"{l.Fruit}[{l.Index}]: {l.Letter}"));

        // 2. Complex grouping and aggregation
        var ageGroups = people
            .GroupBy(p => p.Age / 10 * 10)
            .Select(g => new { AgeGroup = $"{g.Key}-{g.Key + 9}", Count = g.Count(), AvgAge = g.Average(p => p.Age) });
        Console.WriteLine("Age groups:");
        ageGroups.ToList().ForEach(g => Console.WriteLine($"{g.AgeGroup}: Count = {g.Count}, Avg Age = {g.AvgAge:F1}"));

        // 3. Composite key grouping
        var compositeGroups = people
            .GroupBy(p => new { AgeGroup = p.Age / 10 * 10, Country = p.Country })
            .Select(g => new { g.Key.AgeGroup, g.Key.Country, Count = g.Count() });
        Console.WriteLine("Composite groups:");
        compositeGroups.ToList().ForEach(g => Console.WriteLine($"Age {g.AgeGroup}-{g.AgeGroup + 9}, {g.Country}: {g.Count}"));

        // 4. Pairwise comparison
        var pairwise = numbers.Zip(numbers.Skip(1), (a, b) => new { A = a, B = b, Diff = b - a });
        Console.WriteLine("Pairwise differences:");
        pairwise.ToList().ForEach(p => Console.WriteLine($"{p.A} -> {p.B}: Diff = {p.Diff}"));

        // 5. Custom aggregation with seed
        var runningTotal = numbers.Aggregate(
            new List<int>(),
            (list, next) => { list.Add(list.LastOrDefault() + next); return list; }
        );
        Console.WriteLine("Running total: " + string.Join(", ", runningTotal));

        // 6. Complex filtering with multiple conditions
        var complexFilter = people
            .Where(p => p.Age > 30 && p.Name.Length > 3 && p.Country.StartsWith("U"))
            .Select(p => new { p.Name, AgeGroup = p.Age / 10 * 10 });
        Console.WriteLine("Complex filter result:");
        complexFilter.ToList().ForEach(p => Console.WriteLine($"{p.Name} (Age group: {p.AgeGroup}s)"));

        // 7. Nested grouping
        var nestedGroups = people
            .GroupBy(p => p.Country)
            .Select(g => new
            {
                Country = g.Key,
                AgeGroups = g.GroupBy(p => p.Age / 10 * 10)
                             .Select(ag => new { AgeGroup = $"{ag.Key}-{ag.Key + 9}", Count = ag.Count() })
            });
        Console.WriteLine("Nested groups:");
        nestedGroups.ToList().ForEach(ng =>
        {
            Console.WriteLine($"{ng.Country}:");
            ng.AgeGroups.ToList().ForEach(ag => Console.WriteLine($"  {ag.AgeGroup}: {ag.Count}"));
        });

        // 8. Custom sorting with multiple criteria
        var customSorted = people
            .OrderByDescending(p => p.Age)
            .ThenBy(p => p.Name.Length)
            .ThenBy(p => p.Name);
        Console.WriteLine("Custom sorted people:");
        customSorted.ToList().ForEach(p => Console.WriteLine($"{p.Name} (Age: {p.Age}, Name Length: {p.Name.Length})"));

        // 9. Windowing function (moving average)
        int windowSize = 3;
        var movingAverage = numbers
            .Select((n, i) => new
            {
                Number = n,
                Average = numbers.Skip(Math.Max(0, i - windowSize + 1)).Take(windowSize).Average()
            });
        Console.WriteLine($"Moving average (window size {windowSize}):");
        movingAverage.ToList().ForEach(ma => Console.WriteLine($"Number: {ma.Number}, Moving Avg: {ma.Average:F2}"));

        // 10. Complex projection with conditional aggregation
        var complexProjection = fruits
            .Select(f => new
            {
                Fruit = f,
                Length = f.Length,
                VowelCount = f.Count(c => "aeiou".Contains(c)),
                IsLong = f.Length > fruits.Average(fr => fr.Length)
            })
            .OrderByDescending(f => f.VowelCount)
            .ThenBy(f => f.Length);
        Console.WriteLine("Complex fruit projection:");
        complexProjection.ToList().ForEach(f => Console.WriteLine($"{f.Fruit}: Length={f.Length}, Vowels={f.VowelCount}, IsLong={f.IsLong}"));
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }

    public Person(string name, int age, string country)
    {
        Name = name;
        Age = age;
        Country = country;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
// Define a simple entity for database example
public class Fruit
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Define DbContext for Entity Framework (IQueryable demo)
public class AppDbContext : DbContext
{
    public DbSet<Fruit> Fruits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("TestDb");  // In-memory DB for demo (install Microsoft.EntityFrameworkCore.InMemory via NuGet)
}

class Program
{
    static void Main()
    {
        // Step 1: Prepare in-memory data for IEnumerable examples
        // This is an IEnumerable<string> (implements the interface for iteration)
        IEnumerable<string> iEnumerableFruits = new List<string> { "apple", "banana", "cherry", "date", "elderberry" };

        // Demonstrate Extension Methods and Chaining (LINQ methods extend IEnumerable)
        // Method Syntax: Using lambdas
        var methodSyntaxQuery = iEnumerableFruits.Where(f => f.Length > 5).OrderBy(f => f);

        // Query Syntax: SQL-like, compiles to same as method syntax
        var querySyntaxQuery = from f in iEnumerableFruits
                               where f.Length > 5
                               orderby f
                               select f.ToUpper();

        // Step 2: Demonstrate Lazy Loading (Deferred Execution)
        // This query is lazy – doesn't execute yet, even with side effect in lambda
        var lazyIEnumerableQuery = iEnumerableFruits.Where(f =>
        {
            Console.WriteLine($"Lazy check for: {f}");  // Won't print yet!
            return f.Contains('a');
        });

        Console.WriteLine("Lazy query defined, but not executed yet.");

        // Step 3: Trigger execution by iterating (still lazy, but now pulls data)
        Console.WriteLine("Executing lazy query via foreach:");
        foreach (var fruit in lazyIEnumerableQuery)
        {
            Console.WriteLine(fruit);  // Now prints checks and results: apple, banana, date
        }

        // Step 4: Demonstrate Materialized (Eager Execution)
        // Force immediate execution and store in memory
        List<string> materializedList = lazyIEnumerableQuery.ToList();  // Executes now, prints checks again!

        Console.WriteLine("Materialized list ready (eager execution done).");
        // Now loop uses stored results, no re-execution
        foreach (var fruit in materializedList)
        {
            Console.WriteLine($"From materialized: {fruit}");  // apple, banana, date (no checks printed)
        }

        // Step 5: Demonstrate IQueryable<T> with Entity Framework
        using var context = new AppDbContext();
        // Seed data
        context.Fruits.AddRange(
            new Fruit { Name = "apple" },
            new Fruit { Name = "banana" },
            new Fruit { Name = "cherry" },
            new Fruit { Name = "date" },
            new Fruit { Name = "elderberry" }
        );
        context.SaveChanges();

        // IQueryable<Fruit> – builds expression tree for efficient querying (e.g., on DB)
        IQueryable<Fruit> iQueryableFromDb = context.Fruits;

        // Chaining with lazy loading (query built but not sent to DB yet)
        var lazyIQueryableQuery = iQueryableFromDb.Where(f => f.Name.Contains('a')).OrderBy(f => f.Name.Length);

        Console.WriteLine("IQueryable defined, but not executed yet (deferred to DB).");

        // Materialize: Executes on DB side, fetches only needed data
        List<Fruit> materializedFromDb = lazyIQueryableQuery.ToList();  // Query sent to DB now

        Console.WriteLine("Materialized from DB:");
        foreach (var fruit in materializedFromDb)
        {
            Console.WriteLine(fruit.Name);  // apple, banana, date (sorted by length)
        }

        // Bonus: Show query syntax on IQueryable
        var querySyntaxOnIQueryable = from f in iQueryableFromDb
                                      where f.Name.Length > 5
                                      select f.Name.ToUpper();

        // Materialize it
        var resultsFromQuerySyntax = querySyntaxOnIQueryable.ToList();
        Console.WriteLine("From query syntax on IQueryable:");
        foreach (var name in resultsFromQuerySyntax)
        {
            Console.WriteLine(name);  // BANANA, CHERRY, ELDERBERRY
        }
    }
}
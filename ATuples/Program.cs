using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Simple tuple usage
        (string name, int age) person = GetPersonInfo();
        Console.WriteLine($"Person: {person.name}, Age: {person.age}");

        // Medium complexity tuple usage
        var library = new Library();
        var bookInfo = library.GetBookInfo(1);
        PrintBookInfo(bookInfo);

        // Using tuple with function
        var mathOps = GetMathOperations();
        foreach (var op in mathOps)
        {
            Console.WriteLine($"{op.Name}: 5 {op.Symbol} 3 = {op.Operation(5, 3)}");
        }
    }

    static (string, int) GetPersonInfo()
    {
        return ("John Doe", 30);
    }

    static void PrintBookInfo((int Id, string Title, DateTime ReleaseDate, List<string> Authors) book)
    {
        Console.WriteLine($"Book ID: {book.Id}");
        Console.WriteLine($"Title: {book.Title}");
        Console.WriteLine($"Release Date: {book.ReleaseDate.ToShortDateString()}");
        Console.WriteLine($"Authors: {string.Join(", ", book.Authors)}");
    }

    static List<(string Name, Func<int, int, int> Operation, string Symbol)> GetMathOperations()
    {
        return new List<(string, Func<int, int, int>, string)>
        {
            ("Add", (a, b) => a + b, "+"),
            ("Subtract", (a, b) => a - b, "-"),
            ("Multiply", (a, b) => a * b, "*"),
            ("Divide", (a, b) => a / b, "/")
        };
    }
}

class Library
{
    public (int Id, string Title, DateTime ReleaseDate, List<string> Authors) GetBookInfo(int id)
    {
        // Simulating database fetch
        return (1, "C# in Depth", new DateTime(2019, 3, 23), new List<string> { "Jon Skeet" });
    }
}
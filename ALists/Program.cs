using System;
using System.Collections.Generic;
using System.Linq;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Simple List usage
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        Console.WriteLine("Sum of numbers: " + numbers.Sum());

        List<string> fruits = new List<string> { "Apple", "Banana", "Orange" };
        fruits.Add("Mango");// Other operations -  Add, Access elements, Remove elements, Insert elements, Sort elements, Find elements, Count elements
        Console.WriteLine("Fruits: " + string.Join(", ", fruits));

        // Medium complexity List usage
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30 },
            new Person { Name = "Bob", Age = 25 },
            new Person { Name = "Charlie", Age = 35 }
        };
        
        var oldestPerson = people.OrderByDescending(p => p.Age).First();
        Console.WriteLine($"Oldest person: {oldestPerson.Name}, Age: {oldestPerson.Age}");

        // List of tuples
        List<(string Name, int Age)> peopleData = new List<(string, int)>
        {
            ("David", 28),
            ("Eva", 32),
            ("Frank", 22)
        };

        foreach (var person in peopleData)
        {
            Console.WriteLine($"{person.Name} is {person.Age} years old");
        }

        // Nested List
        List<List<int>> matrix = new List<List<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };

        Console.WriteLine("Matrix:");
        foreach (var row in matrix)
        {
            Console.WriteLine(string.Join(" ", row));
        }

        // Finding an element in the matrix
        int target = 5;
        var found = matrix.SelectMany((row, i) => row.Select((value, j) => new { i, j, value }))
                          .FirstOrDefault(x => x.value == target);

        if (found != null)
        {
            Console.WriteLine($"Found {target} at position ({found.i}, {found.j})");
        }
    }
}
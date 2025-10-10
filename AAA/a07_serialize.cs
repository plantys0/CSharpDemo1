

namespace AAA
{
    public class a07_serialize
    {

// Person class with 5 properties
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string Job { get; set; }
    public string Hobby { get; set; }
}


    static void Main()
    {
        // Step 1: Create a list of 4 C# Person objects (each with 5 properties)
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30, City = "New York", Job = "Developer", Hobby = "Reading" },
            new Person { Name = "Bob", Age = 25, City = "San Francisco", Job = "Designer", Hobby = "Painting" },
            new Person { Name = "Charlie", Age = 35, City = "Chicago", Job = "Manager", Hobby = "Hiking" },
            new Person { Name = "Dana", Age = 28, City = "Boston", Job = "Analyst", Hobby = "Cooking" }
        };

        // Print original list
        Console.WriteLine("Original List:");
        foreach (var p in people)
        {
            Console.WriteLine($"{p.Name}, {p.Age}, {p.City}, {p.Job}, {p.Hobby}");
        }

        // Step 2: Serialize (List → JSON String)
        string jsonString = JsonSerializer.Serialize(people);
        Console.WriteLine("\nSerialized JSON: " + jsonString);

        // Step 3: Deserialize (JSON String → List)
        List<Person> deserializedPeople = JsonSerializer.Deserialize<List<Person>>(jsonString);

        // Print deserialized list
        Console.WriteLine("\nDeserialized List:");
        foreach (var p in deserializedPeople)
        {
            Console.WriteLine($"{p.Name}, {p.Age}, {p.City}, {p.Job}, {p.Hobby}");
        }

        // Bonus: Pretty JSON with options
        var options = new JsonSerializerOptions { WriteIndented = true };
        string prettyJson = JsonSerializer.Serialize(people, options);
        Console.WriteLine("\nPretty JSON:\n" + prettyJson);
    }

    }
}

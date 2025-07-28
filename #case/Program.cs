using System;



// Define a simple class for pattern matching  

        Console.WriteLine("Enter a number (1-5) to demo switch features:");
        string input = Console.ReadLine();
        int choice;
        int.TryParse(input, out choice);  // Safe parse  

        // Basic Switch: On integers with multiple cases and default  
        Console.WriteLine("\n1. Basic Switch on Integer:");
        switch (choice)
        {
            case 1:
            case 2:  // Multiple labels (fall-through if no break)  
                Console.WriteLine("You chose 1 or 2 - Low number!");
                break;
            case 3:
                Console.WriteLine("Three - Middle!");
                goto case 4;  // Rare: Jump to another case  
            case 4:
                Console.WriteLine("Four - High!");
                break;
            default:
                Console.WriteLine("Default: Not 1-4.");
                break;
        }

        // Switch with Strings and When Clause (Guard)  
        Console.WriteLine("\n2. Switch on String with When:");
        string fruit = choice % 2 == 0 ? "Apple" : "Banana";  // Based on choice  
        switch (fruit)
        {
            case "Apple" when choice > 2:
                Console.WriteLine("Big Apple!");
                break;
            case "Apple":
                Console.WriteLine("Small Apple.");
                break;
            case "Banana":
                Console.WriteLine("Yellow Banana.");
                break;
        }

        // Switch Expression (C# 8+): Short form, returns value  
        Console.WriteLine("\n3. Switch Expression:");
        string result = choice switch
        {
            1 => "One",
            2 => "Two",
            _ => "Other"  // _ is discard/default  
        };
        Console.WriteLine($"Choice as word: {result}");

        // Pattern Matching: Types, Relational, Property  
        Console.WriteLine("\n4. Pattern Matching Switch:");
        object obj = choice < 3 ? new Person { Name = "Alice", Age = 25 } : choice;  // Mix types  
        switch (obj)
        {
            case int i when i > 3:  // Relational pattern (C# 9+)  
                Console.WriteLine($"Integer greater than 3: {i}");
                break;
            case int i:
                Console.WriteLine($"Small Integer: {i}");
                break;
            case Person p when p.Age >= 18:  // Property pattern with when  
                Console.WriteLine($"Adult Person: {p.Name}");
                break;
            case Person p:
                Console.WriteLine($"Young Person: {p.Name}");
                break;
        }

        // Switch on Enum  
        Day today = (Day)(choice % 6);  // Cycle through enum  
        Console.WriteLine("\n5. Switch on Enum:");
        switch (today)
        {
            case Day.Monday or Day.Tuesday or Day.Wednesday or Day.Thursday or Day.Friday:  // Or pattern (C# 9+)  
                Console.WriteLine("Weekday!");
                break;
            case Day.Weekend:
                Console.WriteLine("Rest time!");
                break;
        }

        // Switch on Tuple  
        Console.WriteLine("\n6. Switch on Tuple:");
        (int num, string text) = (choice, fruit);
        switch ((num, text))
        {
            case (1, "Apple"):
                Console.WriteLine("One Apple.");
                break;
            case ( >= 2, "Banana"):  // Relational in tuple  
                Console.WriteLine("Multiple Bananas.");
                break;
            default:
                Console.WriteLine("Other combo.");
                break;
        }

        // Nested Switch: Inside another  
        Console.WriteLine("\n7. Nested Switch:");
        switch (choice)
        {
            case 1:
                switch (fruit)  // Nested  
                {
                    case "Apple": Console.WriteLine("Nested: 1 and Apple."); break;
                }
                break;
        }

        Console.WriteLine("\nDone! Run again with different input.");

// Define an enum for switch demo  
enum Day { Monday, Tuesday, Wednesday, Thursday, Friday, Weekend };
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
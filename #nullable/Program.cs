#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
#nullable enable
// Define User class first with required properties
public class User
{
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
}

class NullCoalescingExamples
{
    static void Main()
    {
        // Test UserService
        var userService = new UserService();
        var user = userService.CreateUser("John Doe");
        Console.WriteLine($"Created user: {user.Name}, Email: {user.Email}");

        // 2. Chaining multiple ?? operators (right-associative)
        string? a = null, b = null, c = "Found";
        string final = a ?? b ?? c ?? "Last Resort";  // Returns "Found"
        Console.WriteLine(final);

        // 3. With nullable value types
        int? age = null;
        int finalAge = age ?? 18;  // Returns 18 since age is null
        Console.WriteLine(finalAge);

        // 4. Null-coalescing assignment (??=) - C# 8.0+
        List<int>? numbers = null;
        numbers ??= new List<int>();  // Assigns new list only if numbers is null
        numbers.Add(5);  // Safe to use now

        // 5. Combined with null-conditional operator (?.)
        string? name = null;
        int length = name?.Length ?? 0;  // Safe navigation with default
        Console.WriteLine(length);

        // 6. In method parameters/returns
        PrintMessage(null);  // Will use default message

        // 7. With throw expressions - throws if null
        try
        {
            string required = GetNullableValue() ?? throw new ArgumentException("Required value missing");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        // 8. In LINQ expressions
        var items = new List<string?> { null, "Item1", null, "Item2" };
        var firstValid = items.FirstOrDefault(x => x != null) ?? "No items";
        Console.WriteLine(firstValid);

        // 9. With complex expressions
        Customer? customer = GetCustomer();
        string address = customer?.Address?.Street ?? "Address unknown";
        Console.WriteLine(address);
    }

    static void PrintMessage(string? message) =>
        Console.WriteLine(message ?? "Default Message");  // Provides default if null

    static string? GetNullableValue() => null;  // Simulates method that might return null

    static Customer? GetCustomer() => null;  // Simulates getting customer data
}

class Customer
{
    public Address? Address { get; set; }
}

class Address
{
    public string? Street { get; set; }
}

public class UserService
{
    public User CreateUser(string name, string? email = null)
    {
        return new User
        {
            Name = name,  // Safe - cannot be null
            Email = email ?? "no-email@example.com"  // Handle nullable email
        };
    }
}



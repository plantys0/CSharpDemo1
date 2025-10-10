using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{
    public class a08_nullable
    {
        static void Main(string[] args)
        {
            // Null coalescing operator (??): Returns left operand if not null, else right operand.
            string name = null;
            string result = name ?? "Default Name"; // Uses default if name is null
            Console.WriteLine($"Null coalescing: {result}"); // Output: Default Name

            // Null coalescing assignment operator (??=): Assigns right operand only if left is null.
            string greeting = null;
            greeting ??= "Hello"; // Assigns only if greeting is null
            Console.WriteLine($"Null coalescing assignment: {greeting}"); // Output: Hello

            // Null conditional operator (?.): Accesses member only if object is not null.
            List<int> numbers = null;
            numbers?.Add(1); // Does nothing if numbers is null
            Console.WriteLine($"Null conditional: Numbers count = {numbers?.Count ?? 0}"); // Output: 0 (combines ?. and ??)

            // Variation: Combining null conditional and null coalescing.
            numbers = new List<int> { 10, 20 };
            int index = 1;
            int value = numbers?[index] ?? 0; // Accesses index only if not null, else 0
            Console.WriteLine($"Combined: Value at index {index} = {value}"); // Output: 20

            // Variation: Using ??= to conditionally create object without 'if'.
            List<int> items = null;
            (items ??= new List<int>()).Add(5); // Creates list if null, then adds
            Console.WriteLine($"Conditional create: Items count = {items.Count}"); // Output: 1

            // Nullable type example (using ?): Allows value types to be null.
            int? nullableInt = null;
            Console.WriteLine($"Nullable type: {nullableInt ?? 0}"); // Output: 0
        }
    }
}




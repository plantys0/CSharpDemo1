namespace _ConsoleWriteLineBacktick {
    using System;

    class Program {
        static void Main(string[] args) {
            // Sample input
            int x = 3, y = 4;
            string name = "Alice";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);

            // 1. Output an empty line
            Console.WriteLine(); // Expected output: (empty line)

            // 2. Output a static string
            Console.WriteLine("Hello!"); // Expected output: Hello!

            // 3. Output an integer
            Console.WriteLine(x); // Expected output: 3

            // 4. Output a floating-point number
            Console.WriteLine(3.14f); // Expected output: 3.14

            // 5. Output a formatted string with placeholders and corresponding values
            Console.WriteLine("{0} * {1} = {2}", x, y, x * y); // Expected output: 3 * 4 = 12

            // $ = in-place interpolation
            // @ = use special characters like backslashes and newlines without needing to escape them
            //$@ = when used together
            // 6. Output an interpolated string, embedding expressions within the string
            Console.WriteLine($"Sum of {x} and {y} is {x + y}"); // Expected output: Sum of 3 and 4 is 7

            // 7. Output a formatted date using a custom date format
            Console.WriteLine(dateOfBirth.ToString("MMMM dd, yyyy")); // Expected output: January 01, 1990

            // 8. Output the result of an operation on a collection (sum of numbers 1 to 10)
            Console.WriteLine("Sum: {0}", Enumerable.Range(1, 10).Sum()); // Expected output: Sum: 55

            // 9. Output properties of an object
            Console.WriteLine("Name: {0}, Date of Birth: {1}", name, dateOfBirth); // Expected output: Name: Alice, Date of Birth: 01/01/1990 00:00:00

            // 10. Output an interpolated verbatim string literal
            Console.WriteLine($@"Hello, my name is {name}.
Today is {DateTime.Now.ToString("MMMM dd, yyyy")}."); // Expected output: (multiline text with name and current date)

            // 11. Output a verbatim string literal with @
            Console.WriteLine(@"C:\Users\{name}\Documents"); // Expected output: C:\Users\{name}\Documents

            // 11A. Use of $@ (if remove @ --> syntax error)
            Console.WriteLine($@"C:\Users\{name}\Documents"); // Expected output: C:\Users\Alice\Documents

        }
    }

}
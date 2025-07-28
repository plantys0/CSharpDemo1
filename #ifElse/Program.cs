using System;

        // Input: A number to test conditions (change this to test)
        int number = 10;  // Try changing to 5, 0, -3, etc.
        string name = "Alice";  // For string checks
        string maybeNull = null;  // For null checks

        // 1. Most Compact: Ternary Operator (written as condition ? do-if-true : do-if-false)
        // Like a short if-else: if (condition) ? then-do-this : else-do-that. Returns a value.
        // Note: No 'if' keyword; it's just (condition) ? true-value : false-value
        string result1 = (number > 0) ? "Positive" : "Not Positive";
        Console.WriteLine($"Ternary (as if? :): Number {number} is {result1}");  // Output depends on number

        // Extra Example of Ternary as 'Simple If-Then-Else': Imagine 'if (number > 5) ? "Big" : "Small"'
        string size = (number > 5) ? "Big" : "Small";  // Like if (number > 5) then "Big" else "Small"
        Console.WriteLine($"Another Ternary: Number {number} is {size}");

        // 2. Simple If (no else) - Do something only if true
        if (number > 0)
        {
            Console.WriteLine($"Simple If: Number {number} is positive");
        }

        // 3. If-Else - Do one thing if true, another if false
        if (number % 2 == 0)
        {
            Console.WriteLine($"If-Else: Number {number} is even");
        }
        else
        {
            Console.WriteLine($"If-Else: Number {number} is odd");
        }

        // 4. If-ElseIf-Else - Check multiple conditions in order
        if (number > 0)
        {
            Console.WriteLine($"If-ElseIf: Number {number} is positive");
        }
        else if (number < 0)
        {
            Console.WriteLine($"If-ElseIf: Number {number} is negative");
        }
        else
        {
            Console.WriteLine($"If-ElseIf: Number {number} is zero");
        }

        // 5. Nested If - If inside another if (for deeper checks)
        if (number > 0)
        {
            if (number % 2 == 0)
            {
                Console.WriteLine($"Nested If: Number {number} is positive and even");
            }
            else
            {
                Console.WriteLine($"Nested If: Number {number} is positive and odd");
            }
        }

        // 6. If with Logical Operators (&& for AND, || for OR)
        // Compact way to combine conditions
        if (number > 0 && number % 2 == 0)
        {
            Console.WriteLine($"Logical If: Number {number} is positive AND even");
        }
        else if (number < 0 || number == 0)
        {
            Console.WriteLine($"Logical If: Number {number} is negative OR zero");
        }

        // 7. Null-Coalescing Operator (??) - Like if not null else (compact for nulls)
        // Returns left if not null, else right
        string safeName = maybeNull ?? "Default Name";
        Console.WriteLine($"Null-Coalescing: Safe name is {safeName}");  // Output: Default Name if null

        // 8. If with Pattern Matching (C# 8+ feature, more compact for types/checks)
        // Checks value and type in one go
        if (name is string { Length: > 3 })  // Checks if string and length > 3
        {
            Console.WriteLine($"Pattern If: Name '{name}' is a string longer than 3 chars");
        }

        // Run this in Visual Studio: Press F5, see outputs. Change 'number' or 'name' to test!

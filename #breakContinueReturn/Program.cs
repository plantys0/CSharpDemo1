using System;

            // Brief Explanation:
            // - **break**: Stops the entire loop or switch immediately and exits it.
            // - **continue**: Skips the remaining code in the current loop iteration and jumps to the next iteration.
            // - **return**: Exits the current method (like Main here) completely, optionally returning a value. It doesn't just affect loops.

            Console.WriteLine("=== Demo of break, continue, and return in C# .NET 8 ===");
            Console.WriteLine("We'll use a for-loop from 1 to 5 as base.");

            // Example 1: Normal loop without any keywords
            Console.WriteLine("\nNormal loop (no break/continue/return):");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Iteration {i}: Hello!");
            }

            // Example 2: Using break
            // When i == 3, break exits the loop entirely.
            Console.WriteLine("\nLoop with break (stops at i=3):");
            for (int i = 1; i <= 5; i++)
            {
                if (i == 3)
                {
                    Console.WriteLine($"At {i}: Breaking out!");
                    break; // Exits the loop here. Use when you want to stop the loop early, e.g., found what you need.
                }
                Console.WriteLine($"Iteration {i}: Hello!");
            }

            // Example 3: Using continue
            // When i == 3, continue skips the rest of this iteration and goes to next.
            Console.WriteLine("\nLoop with continue (skips i=3):");
            for (int i = 1; i <= 5; i++)
            {
                if (i == 3)
                {
                    Console.WriteLine($"At {i}: Continuing (skipping rest)!");
                    continue; // Skips below code for this i. Use when you want to ignore certain cases but keep looping.
                }
                Console.WriteLine($"Iteration {i}: Hello!");
            }

            // Example 4: Using return
            // When i == 3, return exits the entire Main method (program ends).
            // Note: This will stop the whole program, so place it last in demo.
            Console.WriteLine("\nLoop with return (exits method at i=3):");
            for (int i = 1; i <= 5; i++)
            {
                if (i == 3)
                {
                    Console.WriteLine($"At {i}: Returning (exiting program)!");
                    return; // Exits Main. Use when you want to end the function early, e.g., error or done processing.
                    // Code after return won't run.
                }
                Console.WriteLine($"Iteration {i}: Hello!");
            }

            // This line won't run if return is hit above.
            Console.WriteLine("End of demo (won't see if return was used).");

            // To test: Run this in Visual Studio 2022. Press F5 to debug.
            // Observe console output for each section.
            // Input: No user input needed; it runs automatically.

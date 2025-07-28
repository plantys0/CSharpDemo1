using System;
using System.Collections.Generic;

class StackDemo
{
    static void Main()
    {
        // Create a new Stack of strings  
        Stack<string> myStack = new Stack<string>();

        // 1. Push: Add items to top  
        myStack.Push("Apple");  // Bottom  
        myStack.Push("Banana");
        myStack.Push("Cherry"); // Top  
        Console.WriteLine("After Push:");
        PrintStack(myStack);  // Output: Cherry, Banana, Apple  

        // 2. Peek: Look at top without removing  
        string topItem = myStack.Peek();
        Console.WriteLine($"\nPeek: Top is {topItem}");  // Cherry  

        // 3. Pop: Remove and get top  
        string popped = myStack.Pop();
        Console.WriteLine($"\nPopped: {popped}");  // Cherry  
        PrintStack(myStack);  // Banana, Apple  

        // 4. Contains: Check if item exists  
        bool hasBanana = myStack.Contains("Banana");
        Console.WriteLine($"\nContains 'Banana'? {hasBanana}");  // True  

        // 5. Count: Number of items  
        Console.WriteLine($"Count: {myStack.Count}");  // 2  

        // 6. ToArray: Convert to array  
        string[] stackArray = myStack.ToArray();
        Console.WriteLine("\nToArray:");
        foreach (string item in stackArray)
        {
            Console.WriteLine(item);  // Banana (top), Apple  
        }

        // 7. Clear: Remove all  
        myStack.Clear();
        Console.WriteLine("\nAfter Clear:");
        PrintStack(myStack);  // Empty  

        // 8. TryPeek and TryPop (safer, no error if empty)  
        myStack.Push("Date");  // Add one back  
        string tryPeekItem;
        bool peeked = myStack.TryPeek(out tryPeekItem);
        Console.WriteLine($"\nTryPeek: Success? {peeked}, Item: {tryPeekItem}");  // True, Date  

        string tryPopItem;
        bool poppedSafe = myStack.TryPop(out tryPopItem);
        Console.WriteLine($"TryPop: Success? {poppedSafe}, Item: {tryPopItem}");  // True, Date  

        // Now empty, try again  
        bool emptyPeek = myStack.TryPeek(out _);  // _ ignores output  
        Console.WriteLine($"TryPeek on empty: Success? {emptyPeek}");  // False  
    }

    // Helper method to print stack (top to bottom)  
    static void PrintStack(Stack<string> stack)
    {
        foreach (string item in stack)
        {
            Console.WriteLine(item);
        }
    }
}
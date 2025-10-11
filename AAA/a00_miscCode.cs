using System;
using System.Collections.Generic;
using System.Linq;

namespace AAA;

// Unique class for this file's entry point
public class a00_miscCode
{
    // Delegates (types inside class or namespace)
    delegate int MathOp(int x);
    delegate void Alert();

    // Class for stack
    class MyStack<T>
    {
        private List<T> items = new();
        public void Push(T item) => items.Add(item);
        public T Pop()
        {
            if (items.Count == 0) throw new InvalidOperationException("Stack is empty");
            T item = items[^1];  // Get last
            items.RemoveAt(items.Count - 1);  // Remove it
            return item;
        }
    }

    // This Main runs only this file's code when selected as startup
    public static void Main(string[] args)
    {
        // Usage
        var stack = new MyStack<string>();
        stack.Push("Hello");
        Console.WriteLine(stack.Pop());  // Output: Hello

        // Method to assign
        int Square(int num) => num * num;

        // Usage
        MathOp op = Square;  // Assign method to delegate
        Console.WriteLine(op(5));  // Output: 25

        MathOp square = x => x * x;
        Console.WriteLine(square(6));  // Output: 36

        // Methods to assign
        void SayHello() => Console.WriteLine("Hello!");
        void SayWorld() => Console.WriteLine("World!");

        // Usage
        Alert alerts = SayHello;  // Start with one
        alerts += SayWorld;       // Chain another
        alerts();                 // Output: Hello! \n World!

        List<int> nums = new() { 1, 2, 3, 4 };
        var evens = nums.Where(n => n % 2 == 0);  // Lambdas filter evens
        foreach (var n in evens) Console.WriteLine(n);  // Output: 2 \n 4
    }
}
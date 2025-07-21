using System;

// Define a delegate type for math operations
public delegate int MathOperation(int a, int b);

// Publisher class for events (demonstrates publisher-subscriber pattern)
class Publisher
{
    // Event: A special delegate for publisher-subscriber patterns (e.g., button clicks).
    public event MathOperation OnCalculate;

    public int RaiseEvent(int x, int y)
    {
        return OnCalculate?.Invoke(x, y) ?? 0; // Invokes event handlers if subscribed.
    }
}

// Interface for polymorphism
interface IMath { int Compute(int a, int b); }

// Classes demonstrating polymorphism
class Adder : IMath { public int Compute(int a, int b) => a + b; } // Different behavior via same interface.
class Multiplier : IMath { public int Compute(int a, int b) => a * b; } // Delegates enable polymorphism (different objects behaving differently via the same interface).

class Program
{
    // Method matching delegate signature
    static int Subtract(int x, int y) => x - y;

    // Callback method: Takes delegate as param (delegates as callbacks; passing behavior as data; loose coupling by not hardcoding logic).
    static int PerformOperation(int x, int y, MathOperation op) => op(x, y);

    static void Main()
    {
        // Functions as variables: Passed around like variables.
        MathOperation add = (a, b) => a + b; // Lambda function assigned to delegate (functions can be passed around like variables).
        MathOperation subtract = Subtract; // Method reference (passing behavior as data).

        // Multicast delegate: References multiple methods, invoked in sequence.
        MathOperation multi = add + subtract; // Multicast Delegate: A delegate that can reference multiple methods (invoked in sequence).
        int multiResult = multi(5, 3); // Calls add then subtract; returns last (2).

        // Built-in delegates
        Func<int, int, int> multiply = (a, b) => a * b; // Func<T>: Built-in generic delegate for functions with return (e.g., Func<int, int, int> takes two ints, returns int).
        Action<int, int> print = (a, b) => Console.WriteLine(a + b); // Action<T>: Built-in generic delegate for void methods (no return).

        // Callback usage
        int callbackResult = PerformOperation(5, 3, multiply); // Delegates as callbacks; loose coupling (caller doesn't know implementation).

        // Event subscription
        Publisher pub = new Publisher();
        pub.OnCalculate += add; // Subscribe to event (publisher-subscriber).
        pub.OnCalculate += multiply; // Multicast in events.
        int eventResult = pub.RaiseEvent(5, 3); // Raises event, invokes subscribers.

        // Polymorphism with delegates
        MathOperation polyAdd = new Adder().Compute; // Delegate points to Adder's method.
        MathOperation polyMult = new Multiplier().Compute; // Same delegate type, different behavior (polymorphism and loose coupling).

        // Outputs (compact)
        Console.WriteLine($"Multi: {multiResult} Callback: {callbackResult} Event: {eventResult} PolyAdd: {polyAdd(5, 3)} PolyMult: {polyMult(5, 3)}");
        print(5, 3); // Action invocation.
    }
}
using System;

// No custom delegate needed; use built-in Func for math operations

// Publisher class for events
class Publisher
{
    // Event: A special delegate for publisher-subscriber patterns (e.g., button clicks).
    public event Func<int, int, int> OnCalculate; // Delegates as events.

    public int RaiseEvent(int x, int y) => OnCalculate?.Invoke(x, y) ?? 0; // Invokes event handlers if subscribed.
}

// Interface for polymorphism
interface IMath { int Compute(int a, int b); }

// Classes demonstrating polymorphism
class Adder : IMath { public int Compute(int a, int b) => a + b; } // Different behavior via same interface.
class Multiplier : IMath { public int Compute(int a, int b) => a * b; } // Delegates enable polymorphism (different objects behaving differently via the same interface) and loose coupling.

class Program
{
    // Method matching Func signature
    static int Subtract(int x, int y) => x - y;

    // Callback method: Takes delegate as param (delegates as callbacks; passing behavior as data; loose coupling by not hardcoding logic).
    static int PerformOperation(int x, int y, Func<int, int, int> op) => op(x, y); // Functions can be passed around like variables.

    static void Main()
    {
        // Functions as variables: Passed around like variables.
        Func<int, int, int> add = (a, b) => a + b; // Lambda function (functions can be passed around like variables).
        Func<int, int, int> subtract = Subtract; // Method reference (passing behavior as data).

        // Multicast delegate: References multiple methods, invoked in sequence.
        Func<int, int, int> multi = Delegate.Combine(add, subtract) as Func<int, int, int>; // Multicast Delegate: A delegate that can reference multiple methods (invoked in sequence). Note: Func requires explicit Combine for multicast.

        // Built-in delegates
        Func<int, int, int> multiply = (a, b) => a * b; // Func<T>: Built-in generic delegate for functions with return (e.g., Func<int, int, int> takes two ints, returns int).
        Action<int, int> print = (a, b) => Console.WriteLine(a + b); // Action<T>: Built-in generic delegate for void methods (no return).

        // Results
        int addResult = add(5, 3);
        int subtractResult = subtract(5, 3);
        int multiplyResult = multiply(5, 3);
        int multiResult = multi?.Invoke(5, 3) ?? 0; // Invokes multicast; returns last result.

        // Callback usage
        int callbackResult = PerformOperation(5, 3, multiply); // Delegates as callbacks; loose coupling (caller doesn't know implementation).

        // Event subscription
        Publisher pub = new Publisher();
        pub.OnCalculate += add; // Subscribe to event (publisher-subscriber).
        pub.OnCalculate += multiply; // Multicast in events.
        int eventResult = pub.RaiseEvent(5, 3); // Raises event, invokes subscribers.

        // Polymorphism with delegates
        Func<int, int, int> polyAdd = new Adder().Compute; // Delegate points to Adder's method.
        Func<int, int, int> polyMult = new Multiplier().Compute; // Same delegate type, different behavior (polymorphism and loose coupling).

        // Outputs
        Console.WriteLine($"add: {addResult} subtract: {subtractResult} multiply: {multiplyResult} Combined (last): {multiResult}");
        Console.WriteLine($"Callback: {callbackResult} Event: {eventResult} PolyAdd: {polyAdd(5, 3)} PolyMult: {polyMult(5, 3)}");
        print(5, 3); // Action invocation.
        Console.WriteLine("End");
    }
}
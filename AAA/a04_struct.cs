using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{
    public class a04_struct
    {
        // **Struct Declaration**: Value type, no inheritance, good for small data.
        struct PointStruct
    {
        public int X, Y;  // Fields (no initializers allowed here outside ctors)

        // Constructor: Must have params if custom, init ALL fields.
        public PointStruct(int x, int y)
        {
            X = x;
            Y = y;
        }

        // No custom parameterless ctor allowed: public PointStruct() { } // Error!
    }

    // **Readonly Struct**: Immutable, all fields readonly.
    readonly struct ReadOnlyPoint
    {
        public readonly int X, Y;  // Fields must be readonly

        public ReadOnlyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // **Ref Struct**: Stack-only, no boxing, for performance (e.g., spans).
    ref struct RefPoint
    {
        public int X, Y;

        public RefPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // **Class Declaration**: Reference type, supports inheritance.
    class PointClass
    {
        public int X, Y;  // Fields (initializers OK, but not used here)

        // Custom parameterless ctor allowed.
        public PointClass() { X = 5; }  // Runs custom init

        // Another ctor with params.
        public PointClass(int x, int y) { X = x; Y = y; }
    }

    // **Struct with Members**: Fields, methods, etc.
    struct Person
    {
        public string Name;
        public int Age;

        // Constructor: Must init ALL fields.
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Method example.
        public void Print() => Console.WriteLine($"Name: {Name}, Age: {Age}");
    }

    class Program
    {
        static void Main()
        {
            // **Diff Struct vs Class: Copying Behavior**
            Console.WriteLine("**Struct vs Class: Copying**");
            PointStruct ps1 = new PointStruct(1, 2);  // Uses custom ctor
            PointStruct ps2 = ps1;  // Copies data (value type)
            ps2.X = 10;
            Console.WriteLine($"ps1.X: {ps1.X}");  // 1 (original unchanged)

            PointClass pc1 = new PointClass(1, 2);  // Uses param ctor
            PointClass pc2 = pc1;  // Copies reference
            pc2.X = 10;
            Console.WriteLine($"pc1.X: {pc1.X}");  // 10 (changed)

            // **Auto-Default for Structs**
            Console.WriteLine("\n**Struct Auto-Default**");
            PointStruct psDefault = new PointStruct();  // Auto-default: X=0, Y=0
            Console.WriteLine($"Default X: {psDefault.X}");  // 0

            // **Class Parameterless Ctor**
            Console.WriteLine("\n**Class Default Ctor**");
            PointClass pcDefault = new PointClass();  // Runs custom: X=5
            Console.WriteLine($"Default X: {pcDefault.X}");  // 5

            // **Readonly Struct: Immutable**
            Console.WriteLine("\n**Readonly Struct**");
            ReadOnlyPoint rop = new ReadOnlyPoint(3, 4);
            Console.WriteLine($"ROP X: {rop.X}");  // 3
            // rop.X = 5;  // Error: readonly!

            // **Ref Struct: Stack-Only**
            Console.WriteLine("\n**Ref Struct**");
            RefPoint rp = new RefPoint(6, 7);  // OK on stack
            Console.WriteLine($"RP X: {rp.X}");  // 6
            // Can't box: object o = rp;  // Error!

            // **Struct Members & Constructors**
            Console.WriteLine("\n**Struct Members**");
            Person p = new Person("Alice", 30);
            p.Print();  // Name: Alice, Age: 30

            // **Boxing/Unboxing Demo**
            Console.WriteLine("\n**Boxing/Unboxing**");
            PointStruct psBox = new PointStruct(8, 9);
            object boxed = psBox;  // Boxing: Copy to heap
            PointStruct unboxed = (PointStruct)boxed;  // Unboxing - casting to the struct
            unboxed.X = 20;
            Console.WriteLine($"Original after unbox change: {psBox.X}");  // 8 (copy, original unchanged)

            // **Fields & Initializers**: No field inits in structs outside ctors (see declarations).
        }
    }





    }
}

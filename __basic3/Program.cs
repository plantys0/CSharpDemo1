namespace __basic3 {
    using System; // Import System namespace
    using System.Collections.Generic; // Import System.Collections.Generic namespace for List usage

    class Animal {
        public string Name { get; set; } // Property

        public Animal(string name) { Name = name; } // Constructor

        public virtual void Speak() { Console.WriteLine($"I am a {Name}"); } // Method
    }

    class Dog : Animal {
        public Dog(string name) : base(name) { } // Constructor using base class constructor

        public override void Speak() { Console.WriteLine($"I am a {Name} and I bark"); } // Method overriding
    }

    class Program {
        static void Main() {
            /*expected output
 1 2 3 4 5
1 2 3 4 5
1 2 3 4 5
1 2 3 4 5
I am a Animal
I am a Dog and I bark
Caught exception: Cannot divide by zero
Division complete
            */
            // Sample input: List of integers
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            PrintNumbers(numbers); // Function call

            // Polymorphism example using Animal class and derived Dog class
            Animal myAnimal = new Animal("Animal");
            Dog myDog = new Dog("Dog");
            myAnimal.Speak(); // Expected output: I am a Animal
            myDog.Speak(); // Expected output: I am a Dog and I bark

            // Exception handling using try, catch and finally
            try {
                int result = Divide(5, 0); // Division by zero
            } catch (DivideByZeroException ex) { Console.WriteLine("Caught exception: " + ex.Message); } finally { Console.WriteLine("Division complete"); }

            // using statement with IDisposable (Example: file handling)
            string filePath = "example.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath)) { file.WriteLine("Example text"); }
        }

        // Function: Prints the list of numbers using different loop constructs
        static void PrintNumbers(List<int> numbers) {
            // for loop: Print each number
            for (int i = 0; i < numbers.Count; i++) { Console.Write($"{numbers[i]} "); }
            Console.WriteLine();

            // foreach loop: Print each number
            foreach (int num in numbers) { Console.Write($"{num} "); }
            Console.WriteLine();

            // while loop: Print each number
            int counter = 0;
            while (counter < numbers.Count) { Console.Write($"{numbers[counter]} "); counter++; }
            Console.WriteLine();

            // do-while loop: Print each number
            counter = 0;
            do { Console.Write($"{numbers[counter]} "); counter++; } while (counter < numbers.Count);
            Console.WriteLine();
        }

        // Function: Divides two integers and throws an exception if divisor is zero
        static int Divide(int dividend, int divisor) {
            if (divisor == 0) { throw new DivideByZeroException("Cannot divide by zero"); }
            return dividend / divisor;
        }
    }

}
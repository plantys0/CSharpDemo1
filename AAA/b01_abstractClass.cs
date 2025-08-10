
namespace AAA
{
    public class b01_abstractClass
    {
        abstract class Animal
        {  // Base
            public abstract string GetSound();  // Abstract: Subclasses decide
            public abstract string GetMove();  // Abstract: Subclasses decide

            public string MakeSound() =>$"Animal says: {GetSound()}";

            public string Move()
            {  // Concrete: Shared
                var move = GetMove();  // Calls override at runtime
                return $"Animal says: {move}";
            }
        }

        class Dog : Animal
        {  // Subclass
            public override string GetSound() => "Woof";  // Override
            public override string GetMove() => "DogMove";  // Override
        }

        class Cat : Animal
        {  // Subclass
            public override string GetSound() => "Meow";  // Override
                        public override string GetMove() => "CatMove";  // Override
        }

                class Duck : Animal
        {  // Subclass
            public override string GetSound() => "Quack";  // Override
                                    public override string GetMove() => "DuckMove";  // Override
        }
                static void Main(string[] args)
        {
        // Usage in Main:
        Animal myPet = new Duck();  // Base ref, subclass object
        Console.WriteLine(myPet.MakeSound());  // Output: "Animal says: Woof"
                    Console.WriteLine(myPet.GetMove());  // Output: "Animal says: Woof"
        }

    }
}

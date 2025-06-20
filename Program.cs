using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*Abstraction, inheritance, and polymorphism: The abstract Animal class and the derived Dog and Cat classes showcase these concepts.
Encapsulation: The AnimalShelter class demonstrates encapsulation by having a private field _animals and a public method AddAnimal to manage the animals in the shelter.
Delegates: The AnimalDelegate delegate is used to define a method signature that takes an Animal parameter and returns void. This is used as a parameter in the ProcessAnimals method to process each animal in the shelter.
Lambda expressions: An anonymous function (lambda expression) is used in the shelter.ProcessAnimals method call, allowing you to define the action to be performed on each animal without having to create a separate named method.
Async-await: The ProcessAnimalsAsync and ComputeValueAsync methods are asynchronous methods that use the async and await keywords to enable non-blocking execution.
LINQ: Language Integrated Query (LINQ) is used in the ProcessAnimalsAsync method to access the private _animals field from the AnimalShelter class using reflection. This is just an example to demonstrate the use of LINQ, and in a real-world application, you should avoid accessing private members in this manner.
 */
// Base abstract class (Abstraction & Inheritance)
public abstract class Animal
{
    protected string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }

    // Abstract method (Abstraction)
    public abstract void Speak();
}

// Derived class (Inheritance)
public class Dog : Animal
{
    public Dog(string name) : base(name) { }

    // Polymorphism (Method overriding)
    public override void Speak()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

public class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public override void Speak()
    {
        Console.WriteLine($"{Name} says: Meow!");
    }
}

// Delegate
public delegate void AnimalDelegate(Animal animal);

public class AnimalShelter
{
    private List<Animal> _animals = new List<Animal>();

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    // Using delegate as a parameter
    public void ProcessAnimals(AnimalDelegate action)
    {
        foreach (Animal animal in _animals)
        {
            action(animal);
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        Dog dog = new Dog("Buddy");
        Cat cat = new Cat("Whiskers");

        AnimalShelter shelter = new AnimalShelter();
        shelter.AddAnimal(dog);
        shelter.AddAnimal(cat);

        // Lambda expression (Anonymous function)
        shelter.ProcessAnimals(animal =>
        {
            animal.Speak();
            Console.WriteLine("---------------");
        });

        // Async-await
        await ProcessAnimalsAsync(shelter);

        // Loops and switch case
        for (int i = 1; i <= 5; i++)
        {
            int result = await ComputeValueAsync(i);
            Console.WriteLine($"Result for {i}: {result}");
        }
    }

    public static async Task ProcessAnimalsAsync(AnimalShelter shelter)
    {
        // LINQ and Lambda expression
        IEnumerable<Animal> animals = shelter
            .GetType()
            .GetField("_animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(shelter) as IEnumerable<Animal>;

        await Task.Run(() =>
        {
            foreach (Animal animal in animals)
            {
                animal.Speak();
            }
        });
    }

    public static async Task<int> ComputeValueAsync(int value)
    {
        return await Task.Run(() =>
        {
            int result;
            switch (value)
            {
                case 1:
                    result = value * 10;
                    break;
                case 2:
                    result = value * 20;
                    break;
                case 3:
                    result = value * 30;
                    break;
                default:
                    result = value * 5;
                    break;
            }

            return result;
        });
    }
}
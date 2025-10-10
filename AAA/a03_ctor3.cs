namespace AAA
{
    public class a03_ctor3
    {
        public static void Main(string[] args)
        {
            // Demo static constructor - runs once automatically
            Console.WriteLine("Accessing StaticClass - static ctor should run now.");
            StaticClass.StaticMethod();

            // Demo private constructor via singleton
            Console.WriteLine("\nGetting Singleton instance - private ctor used internally.");
            var instance = Singleton.GetInstance();
            instance.ShowMessage();

            // Demo abstract factory - creates families of related objects
            Console.WriteLine("\nUsing Abstract Factory - for 'modern' shape family.");
            IShapeFactory modernFactory = new ModernShapeFactory();
            Console.WriteLine("Creating modern circle via factory.");
            var circle = modernFactory.CreateCircle();
            circle.Draw();
            Console.WriteLine("Creating modern square via factory.");
            var square = modernFactory.CreateSquare();
            square.Draw();

            // Demo builder - step-by-step complex object creation
            Console.WriteLine("\nUsing Builder - for a custom meal.");
            MealBuilder builder = new MealBuilder();
            Meal meal = builder.AddBurger().AddDrink().AddFries().Build();
            meal.ShowItems();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    // Class with static constructor
    public class StaticClass
    {
        // Static field to init
        private static string _staticData;

        // Static constructor - runs once, auto, no params
        static StaticClass()
        {
            _staticData = "Initialized in static ctor!";
            Console.WriteLine("Static constructor ran.");
        }

        // Static method to show data
        public static void StaticMethod()
        {
            Console.WriteLine($"Static data: {_staticData}");
        }
    }

    // Class with private constructor (singleton example)
    public class Singleton
    {
        // Private instance
        private static Singleton _instance;

        // Private constructor - can't create from outside
        private Singleton()
        {
            Console.WriteLine("Private constructor ran.");
        }

        // Public way to get instance
        public static Singleton GetInstance()
        {
            Console.WriteLine("Getting singleton instance.");
            if (_instance == null)
            {
                _instance = new Singleton(); // Uses private ctor internally
            }
            return _instance;
        }

        // Method to demo
        public void ShowMessage()
        {
            Console.WriteLine("Singleton instance active.");
        }
    }

    // Abstract Factory demo - interface for creating related shapes
    public interface IShapeFactory
    {
        IShape CreateCircle();
        IShape CreateSquare();
    }

    // Abstract product interface
    public interface IShape
    {
        void Draw();
    }

    // Concrete products for modern style
    public class ModernCircle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing modern circle.");
        }
    }

    public class ModernSquare : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing modern square.");
        }
    }

    // Concrete factory for modern shapes
    public class ModernShapeFactory : IShapeFactory
    {
        public IShape CreateCircle()
        {
            Console.WriteLine("Factory creating modern circle.");
            return new ModernCircle();
        }

        public IShape CreateSquare()
        {
            Console.WriteLine("Factory creating modern square.");
            return new ModernSquare();
        }
    }

    // Builder demo - for complex Meal object
    public class Meal
    {
        private List<string> _items = new List<string>();

        public void AddItem(string item)
        {
            Console.WriteLine($"Adding item: {item}");
            _items.Add(item);
        }

        public void ShowItems()
        {
            Console.WriteLine("Meal items:");
            foreach (var item in _items)
            {
                Console.WriteLine($"- {item}");
            }
        }
    }

    // Builder class with fluent methods
    public class MealBuilder
    {
        private Meal _meal = new Meal();

        public MealBuilder AddBurger()
        {
            _meal.AddItem("Burger");
            return this; // Fluent for chaining
        }

        public MealBuilder AddDrink()
        {
            _meal.AddItem("Drink");
            return this;
        }

        public MealBuilder AddFries()
        {
            _meal.AddItem("Fries");
            return this;
        }

        public Meal Build()
        {
            Console.WriteLine("Building final meal.");
            return _meal;
        }
    }
}
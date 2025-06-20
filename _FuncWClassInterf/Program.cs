namespace _FuncWClassInterf
{
    using System;
    using System.Collections.Generic;
    interface IDiscountable { decimal ApplyDiscount(decimal price); }
    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Product(string name, decimal price) { Name = name; Price = price; }
    }
    class DiscountedProduct : Product, IDiscountable
    {
        public decimal DiscountPercentage { get; set; }
        public DiscountedProduct(string name, decimal price, decimal discountPercentage) : base(name, price) { DiscountPercentage = discountPercentage; }
        public decimal ApplyDiscount(decimal price) { return price * (1 - DiscountPercentage / 100); }
    }
    class Program
    {
        static void Main()
        {
            var products = new List<Product> { new Product("Apple", 1.0M), new DiscountedProduct("Orange", 2.0M, 10) };
            foreach (Product p in products)
            {
                IDiscountable discountable = p as IDiscountable;
                decimal price = discountable != null ? discountable.ApplyDiscount(p.Price) : p.Price;
                Console.WriteLine($"{p.Name}: {price:C}");
            }
        }
    }

    //using System;
    //using System.Collections.Generic;
    //interface MyInterface { bool MeetsCondition(); }
    //class MyClass { public string Name { get; set; } public MyClass(string name) { Name = name; } }
    //class MyClassWithInterface : MyClass, MyInterface
    //{
    //    public MyClassWithInterface(string name) : base(name) { }
    //    public bool MeetsCondition() { return Name.Length >= 5; }
    //}
    //class Program
    //{
    //    static void Main()
    //    {
    //        List<MyClass> objects = new List<MyClass> { new MyClass("Apple"), new MyClassWithInterface("Orange"), new MyClass("Grape"), new MyClassWithInterface("Banana") };
    //        Func<MyClass, MyInterface, bool> checkCondition = (obj, myInterface) => myInterface != null && myInterface.MeetsCondition();

    //        foreach (MyClass obj in objects) { Console.WriteLine($"{obj.Name}: {(checkCondition(obj, obj as MyInterface) ? "Meets condition" : "Doesn't meet condition")}"); }
    //    }
    //}
}
namespace _Linked_Hash_ShoppingCart {
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main(string[] args) {
            // Define a hash table to store item prices
            Dictionary<string, decimal> prices = new Dictionary<string, decimal>();
            prices.Add("apple", 0.50m);
            prices.Add("banana", 0.25m);
            prices.Add("orange", 0.75m);
            prices.Add("pear", 1.00m);

            // Create a linked list to store items in the shopping cart
            LinkedList<string> cart = new LinkedList<string>();

            // Add some items to the shopping cart
            cart.AddLast("apple");
            cart.AddLast("banana");
            cart.AddLast("banana");
            cart.AddLast("pear");
            cart.AddLast("orange");
            cart.AddLast("apple");

            // Print the items in the shopping cart
        //    Console.WriteLine("Shopping Cart:");
            foreach (string item in cart) {
       //         Console.WriteLine("- " + item);
            }

            // Calculate the total cost of the items in the shopping cart
            decimal totalCost = 0;
            foreach (string item in cart) {
                decimal price;
                if (prices.TryGetValue(item, out price)) {
                    totalCost += price;
                } else {
                    Console.WriteLine("Error: Unknown item '" + item + "'");
                }
            }

            // Print the total cost of the items in the shopping cart
            Console.WriteLine("Total Cost: $" + totalCost.ToString("0.00"));
        }
    }

}
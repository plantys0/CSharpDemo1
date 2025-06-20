namespace _Linked_Hash_Dictionary_ShoppingCart {
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main(string[] args) {
            // Create a dictionary to store product information
            Dictionary<string, Product> products = new Dictionary<string, Product>();
            products.Add("p1", new Product("p1", "Product 1", 10.0));
            products.Add("p2", new Product("p2", "Product 2", 20.0));
            products.Add("p3", new Product("p3", "Product 3", 30.0));

            // Create a shopping cart using a linked list
            LinkedList<CartItem> cart = new LinkedList<CartItem>();

            // Add items to the cart
            cart.AddLast(new CartItem(products["p1"], 2));
            cart.AddLast(new CartItem(products["p2"], 1));
            cart.AddLast(new CartItem(products["p3"], 3));

            // Calculate the total price of the items in the cart
            double totalPrice = 0.0;
            foreach (CartItem item in cart) {
                totalPrice += item.Product.Price * item.Quantity;
            }

            // Print the total price
            Console.WriteLine("Total price: $" + totalPrice);
        }
    }

    // Represents a product
    class Product {
        public string Id { get; }
        public string Name { get; }
        public double Price { get; }

        public Product(string id, string name, double price) {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    // Represents an item in the shopping cart
    class CartItem {
        public Product Product { get; }
        public int Quantity { get; }

        public CartItem(Product product, int quantity) {
            Product = product;
            Quantity = quantity;
        }
    }

}
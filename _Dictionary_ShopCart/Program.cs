namespace _Dictionary_ShopCart {
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main(string[] args) {
            // Sample input
            List<Product> products = new List<Product> {
            new Product("iPhone 13", 999.99m),
            new Product("Samsung Galaxy S21", 799.99m),
            new Product("Google Pixel 6", 749.99m),
            new Product("OnePlus 9 Pro", 899.99m),
            new Product("Xiaomi Mi 11", 599.99m)
        };

            // Initialize the shopping cart
            ShoppingCart cart = new ShoppingCart();

            // Add products to the cart
            cart.AddProduct(products[0], 2);
            cart.AddProduct(products[1], 1);
            cart.AddProduct(products[2], 3);
            cart.AddProduct(products[3], 1);
            Console.WriteLine($"Total price: {cart.GetTotalPrice()}");
            // Remove a product from the cart
            cart.RemoveProduct(products[1]);
            Console.WriteLine($"Total price: {cart.GetTotalPrice()}");
            // Update the quantity of a product in the cart
            cart.UpdateProductQuantity(products[0], 1);

            // Print the total price of the cart
            Console.WriteLine($"Total price: {cart.GetTotalPrice()}");
            Console.WriteLine();
            // Expected output: Total price: 3248.94
        }
    }

    class Product {
        public string Name { get; }
        public decimal Price { get; }

        public Product(string name, decimal price) {
            Name = name;
            Price = price;
        }
    }

    class ShoppingCart {
        private Dictionary<Product, int> cartItems;

        public ShoppingCart() {
            cartItems = new Dictionary<Product, int>();
        }

        public void AddProduct(Product product, int quantity) {
            if (cartItems.ContainsKey(product)) {
                cartItems[product] += quantity;
            } else {
                cartItems.Add(product, quantity);
            }
        }

        public void RemoveProduct(Product product) {
            if (cartItems.ContainsKey(product)) {
                cartItems.Remove(product);
            }
        }

        public void UpdateProductQuantity(Product product, int newQuantity) {
            if (cartItems.ContainsKey(product)) {
                cartItems[product] = newQuantity;
            }
        }

        public decimal GetTotalPrice() {
            decimal totalPrice = 0;

            foreach (KeyValuePair<Product, int> item in cartItems) {
                totalPrice += item.Key.Price * item.Value;
            }

            return totalPrice;
        }
    }

}
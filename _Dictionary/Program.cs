namespace _Dictionary {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program {
        static readonly Random random = new Random();
        static readonly string[] words = { "apple", "banana", "cherry", "date", "fig", "grape", "kiwi", "lemon", "mango", "nectarine", "orange", "papaya", "pear", "plum", "quince", "raspberry", "strawberry", "tangerine", "watermelon" };

        static void Main(string[] args) {
            // 1. Create a Dictionary (unordered collection) to store key-value pairs
            Dictionary<int, string> dict = new Dictionary<int, string>();
            PopulateDictionary(dict);

            // 2. Look-up values by keys (fast retrieval)
            Console.WriteLine("Enter key to retrieve value: ");
            int key = int.Parse(Console.ReadLine());
            Console.WriteLine(dict.TryGetValue(key, out string value) ? $"Value: {value}" : "Key not found");

            // 3. Add new key-value pairs with unique key constraint
            Console.WriteLine("Enter a new key and a random value will be assigned: ");
            int newKey = int.Parse(Console.ReadLine());
            string newValue = GetRandomWord();
            if (!dict.TryAdd(newKey, newValue)) // Unique key constraint
            {
                Console.WriteLine("Key already exists. Updating value..."); // Modify value
                dict[newKey] = newValue;
            }

            // 4. Delete an entry
            Console.WriteLine("Enter a key to delete: ");
            int keyToDelete = int.Parse(Console.ReadLine());
            dict.Remove(keyToDelete, out string deletedValue);
            Console.WriteLine(deletedValue != null ? $"Deleted value: {deletedValue}" : "Key not found");

            // 5. Modify the Dictionary with more substantial changes
            Console.WriteLine("Square all keys:");
            dict = dict.ToDictionary(pair => pair.Key * pair.Key, pair => pair.Value); // Modify keys using LINQ

            // 6. Filter Dictionary based on values
            Console.WriteLine("Remove entries with values containing 'a':");
            dict = dict.Where(pair => !pair.Value.Contains('a')).ToDictionary(pair => pair.Key, pair => pair.Value); // Use LINQ and lambda

            // 7. Get the count of items in the Dictionary
            Console.WriteLine($"Dictionary count: {dict.Count}");

            // 8. Check if the Dictionary contains a specific key
            Console.WriteLine("Enter a key to check if it exists: ");
            int keyToCheck = int.Parse(Console.ReadLine());
            Console.WriteLine(dict.ContainsKey(keyToCheck) ? "Key exists" : "Key not found");

            // 9. Check if the Dictionary contains a specific value
            Console.WriteLine("Enter a value to check if it exists: ");
            string valueToCheck = Console.ReadLine();
            Console.WriteLine(dict.ContainsValue(valueToCheck) ? "Value exists" : "Value not found");

            // 10. Clear the Dictionary
            dict.Clear();
            Console.WriteLine("Dictionary cleared.");

            // Unordered key-value pairs
            Console.WriteLine("Unordered key-value pairs:");
            foreach (var pair in dict) {
                Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
            }
        }

        static void PopulateDictionary(Dictionary<int, string> dict) {
            for (int i = 1; i <= 10; i++) {
                dict.Add(i, GetRandomWord());
            }
        }

        static string GetRandomWord() {
            return words[random.Next(words.Length)];
        }
    }


}
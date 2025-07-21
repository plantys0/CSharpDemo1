namespace _CompareLists {
     using System;
    using System.Collections.Generic;
     using System.Linq;
    using static System.Runtime.InteropServices.JavaScript.JSType;
    class Person { public string Name { get; set; } }
    class Item { public int Id; public string Val; }
    class Product { public int Price; public string Name; }

    class Program {
        static void Main(string[] args) {

            List<Person> people = new()
        {
            new() { Name = "alice" },
            new() { Name = "Bob" },
            new() { Name = "Charlie" }
        };
            people.Sort(Comparer<Person>.Create((p1, p2) => string.Compare(p1.Name, p2.Name, true)));
            int idx1 = people.BinarySearch(new() { Name = "bob" }, Comparer<Person>.Create((p1, p2) => string.Compare(p1.Name, p2.Name, true)));

            // Example 2: Subrange Search with Reverse Comparer on Duplicates
            List<int> nums = new() { 10, 8, 8, 6, 4, 2 };
            var revComp = Comparer<int>.Create((x, y) => y.CompareTo(x));
            int idx2 = nums.BinarySearch(1, 3, 8, revComp);

            // Example 3: Not Found with Bitwise Complement for Insertion
            List<double> vals = new() { 1.1, 2.2, 3.3, 4.4 };
            int res3 = vals.BinarySearch(2.5); int insert3 = ~res3;

            // Example 4: Large List Subrange with Custom Object Comparer
            List<Item> items = new List<Item>(System.Linq.Enumerable.Range(1, 1000).Select(i => new Item { Id = i, Val = $"Item{i}" }));
            items.Sort(Comparer<Item>.Create((a, b) => a.Id.CompareTo(b.Id)));
            int idx4 = items.BinarySearch(500, 300, new Item { Id = 700 }, Comparer<Item>.Create((a, b) => a.Id.CompareTo(b.Id)));

            // Example 5: Edge Case - Empty Subrange
            List<string> emptyRange = new() { "a", "b", "c" };
            try { int idx5 = emptyRange.BinarySearch(1, 0, "b", null); }
            catch (ArgumentOutOfRangeException ex) { /* handle */ }

            // Example 6: Null Item Search with Default Comparer
            List<string?> strs = new() { null, "a", "b", null }; strs.Sort();
            int idx6 = strs.BinarySearch(null);

            // Example 7: Unsorted List Behavior (Incorrect Result)
            List<int> unsorted = new() { 5, 1, 3, 2 };
            int idx7 = unsorted.BinarySearch(3);

            // Example 8: Subrange with Custom Lambda-Like Comparer for Complex Objects
            List<Product> prods = new()
        {
            new() { Price = 10, Name = "A" },
            new() { Price = 20, Name = "B" },
            new() { Price = 30, Name = "C" },
            new() { Price = 40, Name = "D" }
        };
            prods.Sort(Comparer<Product>.Create((p1, p2) => p1.Price.CompareTo(p2.Price)));
            int idx8 = prods.BinarySearch(1, 2, new() { Price = 25 }, Comparer<Product>.Create((p1, p2) => p1.Price.CompareTo(p2.Price)));

            // Example 9: Duplicates Across Full List with Custom Comparer
            List<int> dups = new() { 1, 2, 2, 2, 3, 4 }; dups.Sort();
            var evenComp = Comparer<int>.Create((x, y) => (x % 2).CompareTo(y % 2));
            int idx9 = dups.BinarySearch(2, evenComp);

            // Example 10: Overflow Edge with Max Int Values and Subrange
            List<int> maxInts = new List<int>(System.Linq.Enumerable.Repeat(int.MaxValue, 100));
            int idx10 = maxInts.BinarySearch(50, 30, int.MaxValue, null);


            // Declare and initialize a list of lists
            List<List<int>> myLists = new List<List<int>> {
                                        new List<int> { 1, 2, 3 },
                                         new List<int> { 4, 5, 6 },
                                        new List<int> { 7, 8, 9 }
                                     };
                            // Add a new column to each row// Add a new element with value 0 to the end of the list
                             foreach (List<int> list in myLists) {                  list.Add(0);               }
                            // Remove the second column from each row // Remove the element at index 1 (the second element)
                             foreach (List<int> list in myLists) {                list.RemoveAt(1);             }

            //ARRAY - Jagged
            int[][] myArray = new int[4][]
            {
                    new int[] {1, 2, 3},
                    new int[] {4, 5, 6},
                    new int[] {7, 8, 9},
                    new int[] {10, 11, 12}
            };

            //ARRAY - MultiDim
            int[,] myArray1 = new int[4, 3]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9},
                {10, 11, 12}
            };

            //Compare lists
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
             List<int> list2 = new List<int> { 5, 4, 3, 2, 1 };
            List<int> list3 = new List<int> { 1, 2, 3, 4, 5 };
             bool result1 = list1.SequenceEqual(list2); // returns false
            bool result2 = list1.SequenceEqual(list3); // returns true
             Console.WriteLine(result1);
            Console.WriteLine(result2);
             Console.WriteLine();
        }
     }

 }
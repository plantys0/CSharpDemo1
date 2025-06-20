namespace _HashSet {
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main(string[] args) {
            // 1. Creating a new HashSet
            HashSet<string> hashSet = new HashSet<string>();

            // 2. Adding items to the HashSet
            hashSet.Add("apple");
            hashSet.Add("banana");
            hashSet.Add("cherry");
            hashSet.Add("date");
            hashSet.Add("date");

            // 3. Displaying the count of items in the HashSet
            Console.WriteLine("The HashSet contains {0} items:", hashSet.Count);

            // 4. Iterating over the items in the HashSet using foreach
            foreach (string item in hashSet) {
                Console.WriteLine(item);
            }

            // 5. Removing an item from the HashSet
            hashSet.Remove("banana");

            // 6. Checking if an item is in the HashSet
            bool containsApple = hashSet.Contains("apple");
            bool containsBanana = hashSet.Contains("banana");

            Console.WriteLine("The HashSet {0} apple.", containsApple ? "contains" : "does not contain");
            Console.WriteLine("The HashSet {0} banana.", containsBanana ? "contains" : "does not contain");

            // 7. Clearing all items from the HashSet
            hashSet.Clear();

            // 8. Creating a new HashSet with an initial capacity
            HashSet<int> hashSet2 = new HashSet<int>(capacity: 10);

            // 9. Adding multiple items to the HashSet using AddRange
            int[] numbers = { 1, 2, 3, 4, 5 };
            hashSet2.UnionWith(numbers);

            // 10. Removing multiple items from the HashSet using RemoveWhere
            hashSet2.RemoveWhere(num => num % 2 == 0);

            // 11. Checking if two HashSets are equal
            HashSet<int> hashSet3 = new HashSet<int> { 1, 3, 5 };
            bool areEqual = hashSet2.SetEquals(hashSet3);
            Console.WriteLine("The two HashSets are {0}equal.", areEqual ? "" : "not ");

            // 12. Creating a new HashSet with a custom equality comparer
            HashSet<string> caseInsensitiveSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            caseInsensitiveSet.Add("Apple");
            caseInsensitiveSet.Add("aPpLe");
            Console.WriteLine("The case-insensitive HashSet contains {0} items:", caseInsensitiveSet.Count);

            // 13. Intersecting two HashSets and storing the result in a new HashSet
            HashSet<int> hashSet4 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> hashSet5 = new HashSet<int> { 2, 3, 4 };
            HashSet<int> intersection = new HashSet<int>(hashSet4);
            intersection.IntersectWith(hashSet5);
            Console.WriteLine("The intersection of the two HashSets contains {0} items:", intersection.Count);

            // 14. Unioning two HashSets and storing the result in a new HashSet
            HashSet<int> union = new HashSet<int>(hashSet4);
            union.UnionWith(hashSet5);
            Console.WriteLine("The union of the two HashSets contains {0} items:", union.Count);

            // 15. Symmetrically excepting two HashSets and storing the result in a new HashSet - elements that are present in either the current HashSet or the specified collection, but not both
            HashSet<int> symmetricExcept = new HashSet<int>(hashSet4);
            symmetricExcept.SymmetricExceptWith(hashSet5);
            Console.WriteLine("The symmetric except of the two HashSets contains {0} items:", symmetricExcept.Count);

            // 16. Copying the items of a HashSet to an array
            int[] array = new int[hashSet4.Count];
            hashSet4.CopyTo(array);

            // 17. Checking if a HashSet is a proper subset of another HashSet
            HashSet<int> hashSet6 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> hashSet7 = new HashSet<int> { 1, 2, 3, 4 };
            bool isProperSubset = hashSet6.IsProperSubsetOf(hashSet7);
            Console.WriteLine("The first HashSet is {0}a proper subset of the second HashSet.", isProperSubset ? "" : "not ");

            // 18. Checking if a HashSet is a proper superset of another HashSet
            bool isProperSuperset = hashSet7.IsProperSupersetOf(hashSet6);
            Console.WriteLine("The second HashSet is {0}a proper superset of the first HashSet.", isProperSuperset ? "" : "not ");

            // 19. Checking if a HashSet overlaps with another HashSet
            HashSet<int> hashSet8 = new HashSet<int> { 3, 4, 5 };
            bool overlaps = hashSet7.Overlaps(hashSet8);
            Console.WriteLine("The second HashSet {0}overlaps with the third HashSet.", overlaps ? "" : "does not ");

            // 20. Checking if a HashSet is a subset of another HashSet
            bool isSubset = hashSet3.IsSubsetOf(hashSet7);
            Console.WriteLine("The first HashSet is {0}a subset of the second HashSet.", isSubset ? "" : "not ");
        }

    }

    }
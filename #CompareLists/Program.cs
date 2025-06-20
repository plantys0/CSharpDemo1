namespace _CompareLists {     using System;
    using System.Collections.Generic;     using System.Linq;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    class Program {
        static void Main(string[] args) {

            // Declare and initialize a list of lists             List<List<int>> myLists = new List<List<int>> {
                                        new List<int> { 1, 2, 3 },                                         new List<int> { 4, 5, 6 },
                                        new List<int> { 7, 8, 9 }                                     };
                            // Add a new column to each row// Add a new element with value 0 to the end of the list                             foreach (List<int> list in myLists) {                  list.Add(0);               }
                            // Remove the second column from each row // Remove the element at index 1 (the second element)                             foreach (List<int> list in myLists) {                list.RemoveAt(1);             }

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
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };             List<int> list2 = new List<int> { 5, 4, 3, 2, 1 };
            List<int> list3 = new List<int> { 1, 2, 3, 4, 5 };             bool result1 = list1.SequenceEqual(list2); // returns false
            bool result2 = list1.SequenceEqual(list3); // returns true             Console.WriteLine(result1);
            Console.WriteLine(result2);             Console.WriteLine();
        }     }
 }
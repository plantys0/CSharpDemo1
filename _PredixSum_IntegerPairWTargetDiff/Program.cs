namespace _HashSet_IntegerPairWTargetDiff {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program {
        static void Main(string[] args) {
            int[] arr = { 1, 5, 3, 4, 2, 5 };
            int targetDiff = 3;

            int count = CountPairsWithTargetDiff(arr, targetDiff);
            Console.WriteLine($"Count of pairs with target difference: {count}");

            Console.WriteLine("Pairs with target difference:");
            PrintPairsWithTargetDiff(arr, targetDiff);
        }

        static int CountPairsWithTargetDiff(int[] arr, int targetDiff) {
            int count = 0;
            HashSet<int> hs = new HashSet<int>(arr);
            foreach (int num in arr) {
                if (hs.Contains(num - targetDiff)) {
                    count++;
                }
                if (hs.Contains(num + targetDiff)) {
                    count++;
                }
                hs.Remove(num);
            }
            return count;
        }

        static void PrintPairsWithTargetDiff(int[] arr, int targetDiff) {
            HashSet<int> hs = new HashSet<int>(arr);
            foreach (int num in arr) {
                if (hs.Contains(num - targetDiff)) {
                    Console.WriteLine($"({num - targetDiff}, {num})");
                }
                if (hs.Contains(num + targetDiff)) {
                    Console.WriteLine($"({num}, {num + targetDiff})");
                }
                hs.Remove(num);
            }
        }
    }


}
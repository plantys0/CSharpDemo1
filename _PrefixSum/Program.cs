namespace _PrefixSum {
    using System;

    class PrefixSumAlgorithm {
        static void Main() {
            // Sample input array
            int[] arr = { 2, 5, 1, 3, 7, 2, 8, 4 };

            // Calculate the prefix sum array
            int[] prefixSum = CalculatePrefixSum(arr);

            // Print the original array and prefix sum array
            Console.WriteLine("Original Array: [{0}]", string.Join(", ", arr));
            Console.WriteLine("Prefix Sum Array: [{0}]", string.Join(", ", prefixSum));

            // Sample output:
            // Original Array: [2, 5, 1, 3, 7, 2, 8, 4]
            // Prefix Sum Array: [2, 7, 8, 11, 18, 20, 28, 32]
        }

        static int[] CalculatePrefixSum(int[] arr) {
            // Create a new array to store prefix sums
            int[] prefixSum = new int[arr.Length];

            // Calculate prefix sum of first element
            prefixSum[0] = arr[0];

            // Calculate prefix sum of remaining elements
            for (int i = 1; i < arr.Length; i++) {
                prefixSum[i] = prefixSum[i - 1] + arr[i];
            }

            // Return the prefix sum array
            return prefixSum;
        }
    }

}
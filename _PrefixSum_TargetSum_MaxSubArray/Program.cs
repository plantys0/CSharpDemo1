namespace _PrefixSum_TargetSum_MaxSubArray {
    using System;

    class Program {
        static void Main(string[] args) {
            int[] arr = { 5, 3, 4, 1, 2, 5 };
            int targetSum = 8;
            int n = arr.Length;

            // Create an array to store prefix sum
            int[] prefixSum = new int[n + 1];

            // Initialize first element of prefix sum as 0
            prefixSum[0] = 0;

            // Calculate prefix sum of the array
            for (int i = 1; i <= n; i++) {
                prefixSum[i] = prefixSum[i - 1] + arr[i - 1];
            }

            int maxLength = 0;

            // Loop through each element of the prefix sum array
            for (int i = 0; i < n; i++) {
                // Find the index of the element in prefix sum array whose value is prefixSum[i] - targetSum
                int index = Array.IndexOf(prefixSum, prefixSum[i] - targetSum);

                // If the index is found and the length of subarray is greater than maxLength, update maxLength
                if (index != -1 && i - index > maxLength) {
                    maxLength = i - index;
                }
            }

            // Print the length of the longest subarray with target sum
            Console.WriteLine(maxLength);
        }
    }

}
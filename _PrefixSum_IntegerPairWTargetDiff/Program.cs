namespace _PrefixSum_IntegerPairWTargetDiff {
    using System;

    class Program {
        static void Main(string[] args) {
            int[] arr = { 1, 5, 3, 4, 2 };
            int targetDiff = 3;

            int count = 0;
            int n = arr.Length;

            // Calculate prefix sum array
            int[] prefixSum = new int[n + 1];
            for (int i = 1; i <= n; i++) {
                prefixSum[i] = prefixSum[i - 1] + arr[i - 1];
            }

            // Find pairs with target difference
            for (int i = 0; i < n; i++) {
                int low = i + 1; int high = n;
                while (low <= high) {
                    int mid = (low + high) / 2;
                    if (prefixSum[mid] - prefixSum[i] == targetDiff) {
                        count++; 
                        Console.WriteLine("(" + arr[i] + ", " + arr[mid - 1] + ")");
                        break;
                    } else if (prefixSum[mid] - prefixSum[i] > targetDiff) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                }
            }

            Console.WriteLine("Number of pairs with target difference: " + count);
        }
    }

}
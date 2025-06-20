namespace _PrefixSum_MaxSubMatrixSum {
    using System;

    public class MaximumSumSubmatrix {
        public static void Main() {
            int[,] matrix = { { 1, 2, -1, -4, -20 },
                                       { -8, -3, 4, 2, 1 },
                          { 3, 8, 10, 1, 3 },
                          { -4, -1, 1, 7, -6 }
                        };
            int k = 2;

            int[,] result = GetMaxSubMatrix(matrix, k);

            Console.WriteLine("Maximum submatrix of size {0} x {0}:", k);
            PrintMatrix(result);
        }

        // Function to print a matrix
        public static void PrintMatrix(int[,] matrix) {
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    Console.Write("{0} ", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        // Function to get the maximum sum submatrix of size k x k
        public static int[,] GetMaxSubMatrix(int[,] matrix, int k) {
            int n = matrix.GetLength(0);
            int[,] result = new int[k, k];

            // Create a prefix sum matrix
            int[,] prefixSum = new int[n + 1, n + 1];
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= n; j++) {
                    prefixSum[i, j] = matrix[i - 1, j - 1] +
                                      prefixSum[i - 1, j] +
                                      prefixSum[i, j - 1] -
                                      prefixSum[i - 1, j - 1];
                }
            }

            int maxSum = int.MinValue;

            // Find the maximum sum submatrix of size k x k
            for (int i = k; i <= n; i++) {
                for (int j = k; j <= n; j++) {
                    int sum = prefixSum[i, j] -
                              prefixSum[i - k, j] -
                              prefixSum[i, j - k] +
                              prefixSum[i - k, j - k];

                    if (sum > maxSum) {
                        maxSum = sum;

                        for (int x = i - k, r = 0; x < i; x++, r++) {
                            for (int y = j - k, c = 0; y < j; y++, c++) {
                                result[r, c] = matrix[x, y];
                            }
                        }
                    }
                }
            }

            return result;
        }
    }


}
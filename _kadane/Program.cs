namespace _kadane
{
    using System;

    class KadaneAlgorithm
    {
        static void Main()
        {
            int[] sampleInput = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            (int maxSum, int[] maxSubArray) = Kadane(sampleInput);
            Console.WriteLine($"Max sum: {maxSum}");
            Console.WriteLine("Max subarray: " + string.Join(", ", maxSubArray));
        }

        static (int, int[]) Kadane(int[] arr)
        {
            int maxSoFar = arr[0], maxEndingHere = arr[0];
            int start = 0, end = 0, tempStart = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > maxEndingHere + arr[i])
                {
                    maxEndingHere = arr[i];
                    tempStart = i;
                }
                else
                {
                    maxEndingHere += arr[i];
                }

                if (maxEndingHere > maxSoFar)
                {
                    maxSoFar = maxEndingHere;
                    start = tempStart;
                    end = i;
                }
            }
            int[] maxSubArray = new int[end - start + 1];
            Array.Copy(arr, start, maxSubArray, 0, end - start + 1);
            return (maxSoFar, maxSubArray);
        }
    }

}
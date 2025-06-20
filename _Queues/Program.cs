namespace _Queues {
    using System;
    using System.Collections.Generic;

    class RunningMedian {
        static void Main(string[] args) // Main method to read input and call methods
        {
            int[] nums = { 5, 15, 1, 3 }; // Sample Input
            List<double> medians = FindRunningMedian(nums);
            foreach (double median in medians) Console.Write(median + " "); // Sample Output: 5 10 5 3
        }

        static List<double> FindRunningMedian(int[] nums) // Find running median of stream of integers
        {
            List<double> medians = new List<double>();
            SortedSet<NumWrapper> maxHeap = new SortedSet<NumWrapper>(new DescComparer());
            SortedSet<NumWrapper> minHeap = new SortedSet<NumWrapper>(new AscComparer());

            foreach (int num in nums) // For each integer in the stream
            {
                if (maxHeap.Count == 0 || num <= maxHeap.Max.Value) maxHeap.Add(new NumWrapper(num)); // Add num to maxHeap
                else minHeap.Add(new NumWrapper(num)); // Else add num to minHeap

                if (maxHeap.Count > minHeap.Count + 1) { minHeap.Add(maxHeap.Max); maxHeap.Remove(maxHeap.Max); } // Rebalance heaps
                else if (minHeap.Count > maxHeap.Count) { maxHeap.Add(minHeap.Min); minHeap.Remove(minHeap.Min); } // Rebalance heaps

                medians.Add(maxHeap.Count == minHeap.Count ? (maxHeap.Max.Value + minHeap.Min.Value) / 2.0 : maxHeap.Max.Value); // Add median to list
            }
            return medians;
        }
    }
    class NumWrapper // Wrapper class to store value and unique index
    {
        public int Value { get; }
        public int Index { get; }
        private static int _nextIndex = 0;

        public NumWrapper(int value) { Value = value; Index = _nextIndex++; }
    }

    class DescComparer : IComparer<NumWrapper> // Custom comparer for maxHeap (descending order)
    {
        public int Compare(NumWrapper x, NumWrapper y) { return x.Value != y.Value ? y.Value.CompareTo(x.Value) : x.Index.CompareTo(y.Index); }
    }

    class AscComparer : IComparer<NumWrapper> // Custom comparer for minHeap (ascending order)
    {
        public int Compare(NumWrapper x, NumWrapper y) { return x.Value != y.Value ? x.Value.CompareTo(y.Value) : x.Index.CompareTo(y.Index); }
    }



}
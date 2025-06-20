namespace _QueuesRunningMedian {
    using System;
    using System.Collections.Generic;

    class RunningMedian {
        static void Main(string[] args) {
            int[] nums = { 5, 15, 1, 3 }; // Sample Input
            List<double> medians = FindRunningMedian(nums);
            foreach (double median in medians) Console.Write(median + " "); // Output: 5 10 5 4
        }

        static List<double> FindRunningMedian(int[] nums) {
            List<double> medians = new List<double>();
            SortedSet<NumWrapper> maxHeap = new SortedSet<NumWrapper>(Comparer<NumWrapper>.Create((x, y) => x.Value != y.Value ? y.Value.CompareTo(x.Value) : x.Index.CompareTo(y.Index)));
            SortedSet<NumWrapper> minHeap = new SortedSet<NumWrapper>(Comparer<NumWrapper>.Create((x, y) => x.Value != y.Value ? x.Value.CompareTo(y.Value) : x.Index.CompareTo(y.Index)));

            foreach (int num in nums) {
                if (maxHeap.Count == 0 || num <= maxHeap.Max.Value) maxHeap.Add(new NumWrapper(num));
                else minHeap.Add(new NumWrapper(num));

                if (maxHeap.Count > minHeap.Count + 1) { minHeap.Add(maxHeap.Max); maxHeap.Remove(maxHeap.Max); } else if (minHeap.Count > maxHeap.Count) { maxHeap.Add(minHeap.Min); minHeap.Remove(minHeap.Min); }

                if (maxHeap.Count == minHeap.Count) medians.Add((maxHeap.Max.Value + minHeap.Min.Value) / 2.0);
                else medians.Add(maxHeap.Max.Value);
            }
            return medians;
        }
    }

    class NumWrapper {
        public int Value { get; }
        public int Index { get; }
        private static int _nextIndex = 0;

        public NumWrapper(int value) { Value = value; Index = _nextIndex++; }
    }

}
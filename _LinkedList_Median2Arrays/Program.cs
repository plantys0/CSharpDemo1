namespace _LinkedList_Median2Arrays {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program {
        static void Main(string[] args) {
            // Input
            int[] nums1 = new int[] { 1, 3 };
            int[] nums2 = new int[] { 2 };

            // Combine the two arrays and sort them
            var combinedList = new LinkedList<int>((nums1.Concat(nums2)).OrderBy(x => x));

            // Get the median of the sorted list
            int medianIndex = combinedList.Count / 2;
            double median = combinedList.ElementAt(medianIndex);
            if (combinedList.Count % 2 == 0) {
                median = (median + combinedList.ElementAt(medianIndex - 1)) / 2.0;
            }

            // Output
            Console.WriteLine($"The median of [{string.Join(", ", nums1)}] and [{string.Join(", ", nums2)}] is {median}");

            /* Expected output:
               The median of [1, 3] and [2] is 2
            */
        }
    }

}
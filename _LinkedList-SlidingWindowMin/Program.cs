namespace _LinkedList_SlidingWindowMin {
    using System;
    using System.Collections.Generic;

    public class Program {
        public static void Main() {
            int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 }; //expected output [-1, -3, -3, -3, 3, 3]
            int k = 3;
            int[] result = SlidingWindowMin(nums, k);
            Console.WriteLine("Input: [" + string.Join(", ", nums) + "]");
            Console.WriteLine("Sliding window size: " + k);
            Console.WriteLine("Minimum values for each window: [" + string.Join(", ", result) + "]");
        }

        public static int[] SlidingWindowMin(int[] nums, int k) {
            var result = new int[nums.Length - k + 1];
            var deque = new LinkedList<int>();
            for (int i = 0; i < nums.Length; i++) {
                // remove elements outside of current window
                if (deque.Count > 0 && deque.First.Value < i - k + 1) deque.RemoveFirst();
                // remove elements smaller than current element
                while (deque.Count > 0 && nums[deque.Last.Value] > nums[i]) deque.RemoveLast();
                deque.AddLast(i);
                // store the minimum element in current window
                if (i - k + 1 >= 0) result[i - k + 1] = nums[deque.First.Value];
            }
            return result;
        }
    }


}
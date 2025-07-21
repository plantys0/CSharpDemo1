/*Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
Notice that the solution set must not contain duplicate triplets.
Input: nums = [-1,0,1,2,-1,-4]
Output: [[-1,-1,2],[-1,0,1]]
*/
using System.Globalization;
using System;
using System.Collections.Generic;

static IList<IList<int>> ThreeSum(int[] nums)
{
    Array.Sort(nums);
    var result = new List<IList<int>>();
    int n = nums.Length;

    for (int i = 0; i < n - 2; i++)
    {
        if (i > 0 && nums[i] == nums[i - 1]) continue; // skip duplicates

        int left = i + 1, right = n - 1;
        while (left < right)
        {
            int sum = nums[i] + nums[left] + nums[right];
            if (sum == 0)
            {
                result.Add(new List<int> { nums[i], nums[left], nums[right] });
                while (left < right && nums[left] == nums[left + 1]) left++; // skip duplicates
                while (left < right && nums[right] == nums[right - 1]) right--; // skip duplicates
                left++;
                right--;
            }
            else if (sum < 0)
            {
                left++;
            }
            else
            {
                right--;
            }
        }
    }
    return result;
}

// Example usage:
int[] nums = new int[] { -1, 0, 1, 2, -1, -4 };
var result = ThreeSum(nums);
foreach (var triplet in result)
{
    Console.WriteLine($"[{string.Join(",", triplet)}]");
}
Console.WriteLine("End");

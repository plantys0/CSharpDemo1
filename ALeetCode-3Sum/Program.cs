
using System.Globalization;
using System;
using System.Collections.Generic;

static IList<IList<int>> ThreeSum(int[] nums)
{
    //var result = new List<IList<int>>();
    //result.Add(new List<int> { 1,  2});
    //result.Add(new List<int> { 2, 3 });
    Array.Sort(nums);
    var result = new List<IList<int>>();

    int n = nums.Length;
    int i = 0;
    //    int j = i + 1;
    int left = i + i;
    //    int k = n - 1;
    int right = n - 1;
    for (i = 0; i < n - 2; i++)
    {
        if (nums[i] + nums[left] + nums[right] == 0)
        {
            result.Add(new List<int> { i, left, right });
            if (nums[left] == nums[left + 1])
            {
                left++;
            }
            if (nums[right] == nums[right - 1])
            {
                right--;
            }
        }
        else if (nums[i] + nums[left] + nums[right] > 0)
        {
            right--;
        }
        else
        {
            left++;
        }
        i++;
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

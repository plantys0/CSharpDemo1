// Sample Input 1: nums = [3,2,2,3], val = 3
// Expected: Return 2, nums becomes [2,2,_,_]
// Sample Input 2: nums = [0,1,2,2,3,0,4,2], val = 2
// Expected: Return 5, nums becomes [0,1,3,0,4,_,_,_] (order may vary)
using System.Runtime.CompilerServices;
var solution = new Solution();
 /*Sample Input 3: nums = [9,1,2,2,3,6,4,2,5], val = 2*/
int[] nums1 = [9,1,2,2,3,6,4,2,5];
int val1 = 2;
int result1 = solution.RemoveElement(nums1, val1);
public class Solution
{
    public int RemoveElement(int[] nums, int val)
    {
        int l=0;
        int r = nums.Length - 1;
        for (int i = 0; i < 10; i++)
        {
            if (nums[r] == val) {r--;} 
            else if (nums[l] != val) { l++;} 
            else if (nums[l] == val && nums[r]  != val) { 
                (nums[l], nums[r]) = (nums[r], nums[l]); 
            };
        }

        return l; 
    }
}
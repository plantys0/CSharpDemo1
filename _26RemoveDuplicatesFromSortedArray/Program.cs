int[] nums = { 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 4 };
Solution solution = new Solution();
int result1 = solution.RemoveDuplicates(nums);
Console.Write("End");

public class Solution
{
    public int RemoveDuplicates(int[] nums)
    {
        //input1 0,1,2,3,4
        //input2 0,0,0,0,0,1,1,1,2
        // keep moving read pointerto the right till the end.
        // if the next value is same as current --> do nothing
        // if the next value is diff from current --> save it in 
        int i = 0;
        int w = 0;
        while (i < nums.Length-1)
        {
            if (nums[i] != nums[i + 1])
            {
                w++;
                nums[w] = nums[i + 1];
            }
            ;
            i++;
        }
        ;
        return w;
    }
}
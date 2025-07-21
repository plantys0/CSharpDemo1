Console.WriteLine("Hello, World!");

/*
pick first number
loop through the rest of the numbers  if nums[i] + nums[j] == target then exit
pick 2nd number loop through the rest of the numbers  if nums[i] + nums[j] == target then exit
*/
static int[] TwoSum(int[] nums, int target)
{
    for (int i = 0; i < nums.Length; i++)
    {
        for (int j = i + 1; j < nums.Length; j++)
        {
            if (nums[i] + nums[j] == target)
            {
                return new int[] { i, j };
            }
        }
    }
    return new int[] { -1, -1 };
}

// Example usage:
int[] nums = new int[] { 2, 7, 11, 15 };
int target = 9;
int[] result = TwoSum(nums, target);
Console.WriteLine($"Indices: {result[0]}, {result[1]}");
Console.WriteLine("End");
namespace ALeetCode_3Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static int[] TwoSum(int[] nums, int target)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[i] + nums[j] == target)
                        {
                            return [i, j];
                        }
                    }
                }
                return [-1, -1];
            }

            // Example usage:
            int[] nums = [2, 7, 11, 15];
            int target = 9;
            int[] result = TwoSum(nums, target);
            Console.WriteLine($"Indices: {result[0]}, {result[1]}");
            Console.WriteLine("End");
        }
    }
}

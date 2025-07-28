Solution sol = new Solution();  // Make an object  
int testNum = 0;  // Example number (change this to test)  
bool result = sol.IsPalindrome(testNum);  // Call the method  
Console.WriteLine($"Is {testNum} a palindrome? {result}");  // Print result  
                                                            // Output example: Is 121 a palindrome? True  

Console.ReadKey();  // Wait for key press to close

public class Solution
{
    public bool IsPalindrome(int x)
    {
        if (x == 0) return true;
        if (x < 0) return false;
        char[] xArray = x.ToString().ToCharArray();
        if (x < 100)
        {
            if (xArray[0] == xArray[1]) {
                return true; }
            else return false;
        };
        int iterateOver = xArray.Length / 2 - 1;
        for (int i = 0; i < iterateOver; i++)
        {
            if (xArray[i] != xArray[xArray.Length - i]) return false;
        }
        ;
        return true;
    }
}
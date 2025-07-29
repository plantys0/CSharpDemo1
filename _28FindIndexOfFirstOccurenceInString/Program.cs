// Define the Solution class with the method to implement


// Create an instance and test with sample inputs
var solution = new Solution();

// Sample Input 1: Expected output 0
Console.WriteLine("Test 1: " + solution.StrStr("sadbutsad", "sadbu"));

// Sample Input 2: Expected output -1
Console.WriteLine("Test 2: " + solution.StrStr("leetcode", "leeto"));
public class Solution
{
    public int StrStr(string haystack, string needle)
    {
/*
a) I will take first character of needle and find its first occurence in haystack. if found, i wil immediately see if the next several characters in haystack match the needle. Then I will look for 2nd occurence
b) I will create a string array of n characters (where n is # of chars in needle, say '5' ) starting with the first char of needle (say 's'). So this string array will have multiple strings from haystack. Each string will have 5 contiguous characters starting withe very occurence of 's' in haystack. Then I will look for first match of the needle in this string array.  
 
 */
        return -1; // Placeholder - replace with your code
    }
}
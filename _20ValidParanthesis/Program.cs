
using System;

var solution = new Solution();


string[] samples = { "()", "(){}[]", "(]" };


foreach (var input in samples)
{
    bool result = solution.IsValid(input);
    Console.WriteLine($"Input: \"{input}\" -> Result: {result}");
}
public class Solution
{

    public bool IsValid(string s)
    {

        var stack = new Stack<char>();
        foreach (char c in s)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                char open = stack.Pop();
                if (((open == '(') && c == ')') || ((open == '[') && c == ']') || ((open == '{') && c == '}'))
                {
                }
                else
                {
                    return false;
                }

            }
            ;
        }
        return true;
    }





}


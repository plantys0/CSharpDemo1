namespace _Stacks {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program {
        static void Main(string[] args) {
            string input = "(abc{[d(e)fgh]ety}jh)"; // Sample input: balanced parentheses
            bool isBalanced = CheckBalanced(input);
            Console.WriteLine(isBalanced ? "Balanced" : "Not Balanced"); // Sample output: "Balanced"
        }

        static bool CheckBalanced(string s) {
            Stack<char> stack = new Stack<char>(); // Create a stack to store parentheses
            var pairs = new Dictionary<char, char> { { '(', ')' }, { '{', '}' }, { '[', ']' } }; // Store the pairs of parentheses

            // @@try to use LINQ
            //return s.All(c => {
            //    if (pairs.ContainsKey(c)) { stack.Push(c); return true; }
            //    if (pairs.ContainsValue(c)) {
            //        return stack.Count > 0 && c == pairs[stack.Pop()];
            //    }
            //    return !pairs.ContainsKey(c) && !pairs.ContainsValue(c);
            //}) && stack.Count == 0;

            //@@try without LINQ
            bool balanced = true;

            foreach (char c in s) {
                if (pairs.ContainsKey(c)) { stack.Push(c); } 
                else if (pairs.ContainsValue(c) && stack.Count > 0 && c == pairs[stack.Peek()]) { stack.Pop(); } 
                else if (pairs.ContainsValue(c)) { balanced = false; break; }
            }

            return balanced && stack.Count == 0;



        }
    }


}
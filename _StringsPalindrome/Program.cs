namespace _StringsPalindrome {
    using System;
    using System.Linq;
    class PalindromeChecker {
        static bool IsPalindrome(string input) {
            var cleanedInput = new string(input.Where(c => Char.IsLetterOrDigit(c)).Select(c => Char.ToLower(c)).ToArray()); // Filter non-alphanumeric characters and convert to lowercase
            return cleanedInput == new string(cleanedInput.Reverse().ToArray()); // Check if the cleaned input and its reverse are the same
        }
        static void Main(string[] args) {
            string input = "A man, a plan, a canal: Panama"; // Sample input
            bool isPalindrome = IsPalindrome(input);
            Console.WriteLine($"Is \"{input}\" a palindrome? {IsPalindrome(input)}"); // Output: True
            input = "Coding is fun!"; // Sample input
            Console.WriteLine($"Is \"{input}\" a palindrome? {IsPalindrome(input)}"); // Output: False
        }
    }

}
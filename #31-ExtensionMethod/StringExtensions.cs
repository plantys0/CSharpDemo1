using System;

// Define a static class for the extension method
public static class StringExtensions
{
    // Extension method for the string type
    public static int WordCount(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return 0;

        // Split the string by whitespace and count non-empty words
        return input.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
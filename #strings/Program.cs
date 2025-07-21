using System.Text;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

#region  stringLINQ

var words9 = new List<string>
        {
            "apple",      // Length 5
            "banana",     // Length 6
            "cherry",     // Length 6
            "date",       // Length 4
            "elderberry", // Length 10
            "fig",        // Length 3
            "grape"       // Length 5
        };

var grouped = words9.GroupBy(
    key => key.Length//,  // Key selector: group by the length of each string
    //(k, g) => new       // Result selector: for each group, create an anonymous object
    //{
    //    Key = k,                        // The group key (length)
    //    Sorted = g.OrderBy(x => x).ToList()  // Sorted list of strings in the group
    //}
);

// Sort the groups by key (length) for display
var sortedGroups = grouped.OrderBy(group => group.Key);






int[]  inputNums = [ -101, 2, -13, 4, 5, 6, 7, 8, 9, 10, 123, 461, 864 ];
var outputNums = inputNums
     //.Where(n => n > 0 && Math.Sqrt(n) % 1 == 0)
   //  .Where(n => { if (n < 0) return false; var sum = 0; for (int i = n; i > 0; i /= 10) sum += i % 10; return sum % 2 == 0; })
   .ToList();
Console.WriteLine("done");

string sentence = "This is a test sentence dummy    difference     abcd abcde  fgd hjuk.";
var combinedQuery = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries)
//     .Where(s => s?.Length > 3 && s.IndexOf('a', StringComparison.OrdinalIgnoreCase) >= 0)
//     .Where(s => { var vowels = "aeiou"; return s.Count(c => vowels.Contains(char.ToLower(c))) >= 2; })
//.GroupBy(s => s.Length % 2 == 0 ? "Even" : "Odd")
//.GroupBy(n => n.ToString()[0])
//.GroupBy(s => { return ("aeiou".Contains(s[0].ToString().ToLower()))  ? "VowelStart": "ConsonantStart"; })
.GroupBy(s => { var first = s[0].ToString().ToLower();  return ("aeiou".Contains(first)) ? "VowelStart" : "ConsonantStart"; })
    .ToList();

    Console.WriteLine("done");



#endregion



#region stringComplex
string text = "Hello World 123!";
        Console.WriteLine($"Original text: '{text}'\n");
// 1. Extract digits using Where + Aggregate
// Where(char.IsDigit) filters characters using method group (shorthand for c => char.IsDigit(c))
// Aggregate("", (acc, c) => acc + c) is a fold operation that accumulates results using lambda with accumulator and current item
// This pattern replaces traditional foreach loops with functional programming approach
string digits = text.Where(char.IsDigit).Aggregate("", (acc, c) => acc + c);
        Console.WriteLine($"1. Digits extracted: '{digits}'"); // Output: "123"

        // 2. Reverse string using LINQ Reverse + constructor
        // Reverse() returns IEnumerable<char> in reverse order, strings are immutable so need new constructor
        // ToArray() materializes the enumerable into char[] array
        // new string(char[]) constructor creates string from character array - specific overload for this pattern
        string reversed = new string(text.Reverse().ToArray());
        Console.WriteLine($"2. Reversed: '{reversed}'"); // Output: "!321 dlroW olleH"

        // 3. Split with options + string join
        // Split(' ', StringSplitOptions.RemoveEmptyEntries) uses overload with enum parameter to filter empty strings
        // Enum controls splitting behavior, eliminating need for additional LINQ filtering
        // string.Join() concatenates array elements with delimiter - more efficient than manual concatenation
        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"3. Words split: {string.Join("|", words)}"); // Output: "Hello|World|123!"

        // 4. Case toggle using Select with index
        // Select((c, i) => ...) uses overload providing both element and index as parameters
        // Lambda (c, i) destructures into character and position
        // Ternary operator ?: creates conditional logic in expression form
        // string.Concat() efficiently joins IEnumerable<char> without intermediate array allocation
        string toggled = string.Concat(text.Select((c, i) => i % 2 == 0 ? char.ToUpper(c) : char.ToLower(c)));
        Console.WriteLine($"4. Case toggled: '{toggled}'"); // Output: "HeLlO WoRlD 123!"

        // 5. Character frequency using GroupBy + ToDictionary
        // GroupBy(c => c) groups characters by themselves, creating IGrouping<char, char> objects
        // Each group has Key (the character) and contains all occurrences
        // ToDictionary(g => g.Key, g => g.Count()) uses two lambda selectors - key and value selector
        var freq = text.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        Console.WriteLine($"5. Letter 'l' frequency: {freq.GetValueOrDefault('l', 0)} times"); // Output: 3 times

        // 6. Remove duplicates using Distinct
        // Distinct() uses default equality comparer for char type to eliminate duplicates
        // Preserves first occurrence order unlike sorting approaches
        // Combination with ToArray() and string constructor materializes lazy enumerable into concrete string
        string unique = new string(text.Distinct().ToArray());
        Console.WriteLine($"6. Unique chars: '{unique}'"); // Output: "Helo Wrd123!"

        // 7. Pattern matching with switch expression (C# 8.0)
        // Switch expressions use 'switch' keyword followed by pattern arms with => syntax instead of case blocks
        // >= 'A' and <= 'Z' uses relational patterns with logical 'and' combinator
        // _ is discard pattern (catch-all), switch expressions return values directly, not statements
        string classified = string.Concat(text.Select(c => c switch
        {
            >= 'A' and <= 'Z' => 'U',  // Uppercase letter
            >= 'a' and <= 'z' => 'L',  // Lowercase letter
            >= '0' and <= '9' => 'N',  // Number
            _ => 'S'                   // Symbol/Space
        }));
        Console.WriteLine($"7. Character classification: '{classified}'"); // Output: "ULLLL ULLLL NNNS"

        // 8. Range slicing (C# 8.0)
        // Range operator .. creates System.Range objects for slicing operations
        // ^2 uses hat operator for "index from end" - ^2 means "2nd from end"
        // 2..^2 creates range from index 2 to 2nd-from-end (exclusive)
        // Replaces Substring() calls with more intuitive syntax for complex slicing
        string middle = text[2..^2]; // Skip first 2 and last 2 characters
        Console.WriteLine($"8. Middle slice [2..^2]: '{middle}'"); // Output: "llo World 12"

        // 9. Regex replace with lambda delegate
        // Regex.Replace() with lambda allows dynamic replacement logic instead of static strings
        // m parameter is Match object containing match details
        // m.Value gets matched text, enabling transformation of each match individually
        // Enables complex conditional replacements within single method call
        string vowelCaps = Regex.Replace(text, "[aeiou]", m => m.Value.ToUpper());
        Console.WriteLine($"9. Vowels capitalized: '{vowelCaps}'"); // Output: "HEllO WOrld 123!"

        // 10. Any condition with method group
        // Any(char.IsDigit) uses method group syntax - compiler converts char.IsDigit to Func<char, bool> delegate
        // Avoids explicit lambda c => char.IsDigit(c) syntax
        // Any() short-circuits on first true result, efficient for existence checks on large sequences
        bool hasDigits = text.Any(char.IsDigit);
        Console.WriteLine($"10. Contains digits: {hasDigits}"); // Output: True

        // 11. Take for substring operations
        // Take(n) creates lazy enumerable yielding first n elements without materializing entire collection
        // Combined with string.Concat() provides efficient substring extraction
        // Works with any IEnumerable<char> source, more flexible than Substring() for dynamic lengths
        string firstHalf = string.Concat(text.Take(text.Length / 2));
        Console.WriteLine($"11. First half: '{firstHalf}'"); // Output: "Hello Wo"

        // 12. Zip for character pairing
        // Zip() combines elements from two sequences pairwise using selector function
        // text.Skip(1) creates offset sequence starting from 2nd character
        // Lambda (a, b) => $"({a}{b})" processes each pair with string interpolation
        // Zip() stops when shorter sequence exhausts, handles boundary automatically
        string paired = string.Concat(text.Zip(text.Skip(1), (a, b) => $"({a}{b})"));
        Console.WriteLine($"12. Character pairs: '{paired}'"); // Output: "(He)(el)(ll)(lo)(o )( W)(Wo)(or)(rl)(ld)(d )( 1)(12)(23)(3!)"

        // 13. FirstOrDefault with predicate
        // FirstOrDefault(predicate) returns first matching element or type's default value (\0 for char)
        // Eliminates exception handling for empty results
        // != '\0' tests against char's default value
        // Safely handles "not found" scenarios without try-catch blocks
        char firstDigit = text.FirstOrDefault(char.IsDigit);
        Console.WriteLine($"13. First digit found: '{(firstDigit != '\0' ? firstDigit : "None")}'"); // Output: '1'

        // 14. Index from end operator (C# 8.0)
        // Hat operator ^ creates System.Index from end of collection
        // ^1 means "1st from end" (last element), ^0 would be invalid
        // Replaces text[text.Length - 1] calculations with readable syntax
        // Compiler translates ^n to text.Length - n automatically for arrays and strings
        char lastChar = text[^1];
        Console.WriteLine($"14. Last character [^1]: '{lastChar}'"); // Output: '!'

        // 15. SelectMany for collection flattening
        // SelectMany() flattens nested collections by applying selector returning IEnumerable<T> for each element
        // w => w.ToCharArray() converts each word to char array
        // SelectMany() concatenates all resulting arrays into single flat sequence
        // Replaces nested loops with functional approach for collection flattening operations
        string flattened = string.Concat(text.Split().SelectMany(w => w.ToCharArray()));
        Console.WriteLine($"15. Flattened (spaces removed): '{flattened}'"); // Output: "HelloWorld123!"

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
#endregion

#region stringBasic

string message1;
//preferred
string message3 = System.String.Empty; //GOOD  or ""
//not preferred
string message2 = null;  // will give warning
string? message9 = null;  // will not give warning. we are telling that nullable is expected and safe value

// Initialize with a regular string literal.
string oldPath = "c:\\Program Files\\Microsoft Visual Studio 8.0";

// Initialize with a verbatim string literal.
string newPath = @"c:\Program Files\Microsoft Visual Studio 9.0";


// Use a const string to prevent 'message4' from
// being used to store another string value.
const string message4 = "You can't get rid of me!";

// Use the String constructor only when creating
// a string from a char*, char[], or sbyte*. See
// System.String documentation for details.
char[] letters = { 'A', 'B', 'C' };
string alphabet = new string(letters);

string s101 = "A string is more ";
string s201 = "than the sum of its chars.";
// Concatenate s101 and s202. This actually creates a new
// string object and stores it in s101, releasing the
// reference to the original object.
s101 += s201;
System.Console.WriteLine(s101);
// Output: A string is more than the sum of its chars.

string str1 = "Hello ";
string str2 = str1;
str1 += "World";
System.Console.WriteLine(str2);
// Output: Hello 

string columns = "Column 1\tColumn 2\tColumn 3";
// Output: Column 1   Column 2   Column 3

string rows = "Row 1\r\nRow 2\r\nRow 3";
/* Output:
Row 1
Row 2
Row 3
*/

string title = "\"The \u00C6olean Harp\", by Samuel Taylor Coleridge";
// Output: "The Æolean Harp", by Samuel Taylor Coleridge

string filePath = @"C:\Users\scoleridge\Documents\";
// Output: C:\Users\scoleridge\Documents\

string text9 = @"My pensive SARA ! thy soft cheek reclined
Thus on mine arm, most soothing sweet it is
To sit beside our Cot,...";
/* Output:
My pensive SARA ! thy soft cheek reclined
Thus on mine arm, most soothing sweet it is
To sit beside our Cot,...
*/

string quote = @"Her name was ""Sara.""";
// Output: Her name was "Sara."

string singleLine = """Friends say "hello" as they pass by.""";
string multiLine = """
    Hello World!
    " is typically the first program someone writes.
""";
string embeddedXML = """
 Here is the main text
 Excerpts from "An amazing story"
""";


// CS8997: Unterminated raw string literal.
var multiLineStart = """This is the beginning of a string """;

// CS9000: Raw string literal delimiter must be on its own line.
var multiLineEnd = """ This is the beginning of a string """;

// CS8999: Line does not start with the same whitespace as the closing line
// of the raw string literal
var noOutdenting = """
 A line of text.
 Trying to outdent the second line.
""";

string jsonString = """
{
  "Date": "2019-08-01T00:00:00-07:00",
  "TemperatureCelsius": 25,
  "Summary": "Hot",
  "DatesAvailable": [
    "2019-08-01T00:00:00-07:00",
    "2019-08-02T00:00:00-07:00"
  ],
  "TemperatureRanges": {
    "Cold": {
      "High": 20,
      "Low": -10
    },
    "Hot": {
      "High": 60,
      "Low": 20
    }
  },
  "SummaryWords": [
    "Cool",
    "Windy",
    "Humid"
  ]
}
""";

var jh = (firstName: "Jupiter", lastName: "Hammon", born: 1711, published: 1761);
Console.WriteLine($"{jh.firstName} {jh.lastName} was an African American poet born in {jh.born}.");
Console.WriteLine($"He was first published in {jh.published} at the age of {jh.published - jh.born}.");
Console.WriteLine($"He'd be over {Math.Round((2018d - jh.born) / 100d) * 100d} years old today.");
// Output:
// Jupiter Hammon was an African American poet born in 1711.
// He was first published in 1761 at the age of 50.
// He'd be over 300 years old today.

int X = 2;
int Y = 3;
var pointMessage = $$"""
The point {{{X}}, {{Y}}} is {{Math.Sqrt(X * X + Y * Y)}} from the origin.
""";
Console.WriteLine(pointMessage);
// Output:
// The point {2, 3} is 3.605551275463989 from the origin.

var jh2 = (firstName: "Jupiter", lastName: "Hammon", born: 1711, published: 1761);
Console.WriteLine($@"
{jh2.firstName} {jh2.lastName} was an African American poet born in {jh2.born}.");
Console.WriteLine(@$"He was first published in {jh2.published} at the age of {jh2.published - jh2.born}.");
// Output:
// Jupiter Hammon
// was an African American poet born in 1711.
// He was first published in 1761
// at the age of 50.

var pw = (firstName: "Phillis", lastName: "Wheatley", born: 1753, published: 1773);
Console.WriteLine("{0} {1} was an African American poet born in {2}.", pw.firstName, pw.lastName, pw.born);
Console.WriteLine("She was first published in {0} at the age of {1}.", pw.published, pw.published - pw.born);
Console.WriteLine("She'd be over {0} years old today.", Math.Round((2018d - pw.born) / 100d) * 100d);
// Output:
// Phillis Wheatley was an African American poet born in 1753.
// She was first published in 1773 at the age of 20.
// She'd be over 300 years old today.

string s3 = "Visual C# Express";
System.Console.WriteLine(s3.Substring(7, 2)); // Output: "C#"
System.Console.WriteLine(s3.Replace("C#", "Basic")); // Output: "Visual Basic Express"

// Index values are zero-based
int index = s3.IndexOf("C"); // index = 7

string s5 = "Printing backwards";
for (int i = 0; i < s5.Length; i++)
{
    System.Console.Write(s5[s5.Length - i - 1]);
}
// Output: "sdrawkcab gnitnirP"

string question = "hOW DOES mICROSOFT wORD DEAL WITH THE cAPS lOCK KEY?";
System.Text.StringBuilder sb = new System.Text.StringBuilder(question);
for (int j = 0; j < sb.Length; j++)
{
    if (System.Char.IsLower(sb[j]) == true)
        sb[j] = System.Char.ToUpper(sb[j]);
    else if (System.Char.IsUpper(sb[j]) == true)
        sb[j] = System.Char.ToLower(sb[j]);
}
// Store the new string.
string corrected = sb.ToString();
System.Console.WriteLine(corrected);
// Output: How does Microsoft Word deal with the Caps Lock key?

string s = String.Empty;

string str = "hello";
string? nullStr = null;
string emptyStr = String.Empty;
string tempStr = str + nullStr;
// Output of the following line: hello
Console.WriteLine(tempStr);

bool b = (emptyStr == nullStr);
// Output of the following line: False
Console.WriteLine(b);

// The following line creates a new empty string.
string newStr = emptyStr + nullStr;

// Null strings and empty strings behave differently. The following
// two lines display 0.
Console.WriteLine(emptyStr.Length);
Console.WriteLine(newStr.Length);

// The following line raises a NullReferenceException.
//Console.WriteLine(nullStr.Length);

// The null character can be displayed and counted, like other chars.
string s1 = "\x0" + "abc";
string s2 = "abc" + "\x0";
// Output of the following line: * abc*
Console.WriteLine("*" + s1 + "*");
// Output of the following line: *abc *
Console.WriteLine("*" + s2 + "*");
// Output of the following line: 4
Console.WriteLine(s2.Length);

System.Text.StringBuilder sb2 = new System.Text.StringBuilder("Rat: the ideal pet");
sb2[0] = 'C';
System.Console.WriteLine(sb2.ToString());
// Outputs Cat: the ideal pet

var sb3 = new StringBuilder();
// Create a string composed of numbers 0 - 9
for (int i = 0; i < 10; i++)
{
    sb3.Append(i.ToString());
}
Console.WriteLine(sb3); // displays 0123456789

// Copy one character of the string (not possible with a System.String)
sb3[0] = sb3[9];
Console.WriteLine(sb3); // displays 9123456789
#endregion
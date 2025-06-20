namespace _Regex {
    using System;
    using System.Text.RegularExpressions;

    class Program {
        static void Main(string[] args) {
            string input = "Hello, my name is Jane Doe. My email address is jane.doe@example.com. My phone number is (555) 123-4567. I live at 1234 Elm St., Springfield, IL 62704.";

            // 1. Match word characters. start with @"........"
            Regex regex1 = new Regex(@"\w+");
            MatchCollection matches1 = regex1.Matches(input);
            Console.WriteLine("1. Match word characters:");
            foreach (Match match in matches1) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 2. Match items with 1 or more digits  -\d for digit, + for 1 or more
            Regex regex2 = new Regex(@"\d+");
            MatchCollection matches2 = regex2.Matches(input);
            Console.WriteLine("2. Match digits:");
            foreach (Match match in matches2) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 3. Match whitespace - \s
            Regex regex3 = new Regex(@"\s");
            MatchCollection matches3 = regex3.Matches(input);
            Console.WriteLine("3. Match whitespace:");
            foreach (Match match in matches3) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 4. Match non-word characters
            Regex regex4 = new Regex(@"\W+");
            MatchCollection matches4 = regex4.Matches(input);
            Console.WriteLine("4. Match non-word characters:");
            foreach (Match match in matches4) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 5. Match word boundaries - \b is called a word boundary. It is a zero-width 
            Regex regex5 = new Regex(@"\b");
            MatchCollection matches5 = regex5.Matches(input);
            Console.WriteLine("5. Match word boundaries:");
            foreach (Match match in matches5) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 6. Match email addresses  @"   \b   []+   @  []+   \.     []  {2,}                  \b"
            Regex regex6 = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b");
            Match match6 = regex6.Match(input);
            Console.WriteLine("6. Match email addresses:");
            Console.WriteLine(match6.Value);
            Console.WriteLine();

            // 7. Match phone numbers   (    d3  }   d3  -d4
            Regex regex7 = new Regex(@"\(\d{3}\) \d{3}-\d{4}");
            Match match7 = regex7.Match(input);
            Console.WriteLine("7. Match phone numbers:");
            Console.WriteLine(match7.Value);
            Console.WriteLine();

            // 8. Match street addresses
            Regex regex8 = new Regex(@"\d+ [A-Za-z0-9]+ St\.");
            Match match8 = regex8.Match(input);
            Console.WriteLine("8. Match street addresses:");
            Console.WriteLine(match8.Value);
            Console.WriteLine();

            // 9. Match city names
            Regex regex9 = new Regex(@"[A-Za-z]+,");
            Match match9 = regex9.Match(input);
            Console.WriteLine("9. Match city names:");
            Console.WriteLine(match9.Value);
            Console.WriteLine();

            // 10. Match state abbreviations
            Regex regex10 = new Regex(@"[A-Z]{2}");
            Match match10 = regex10.Match(input);
            Console.WriteLine("10. Match state abbreviations:");
            Console.WriteLine(match10.Value);
            Console.WriteLine();

            // 11. Match zip codes
            Regex regex11 = new Regex(@"\d{5}");
            Match match11 = regex11.Match(input);
            Console.WriteLine("11. Match zip codes:");
            Console.WriteLine(match11.Value);
            Console.WriteLine();

            // 12. Match sentences
            Regex regex12 = new Regex(@"[^.!?]+[.!?]");
            MatchCollection matches12 = regex12.Matches(input);
            Console.WriteLine("12. Match sentences:");
            foreach (Match match in matches12) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 13. Match words that end with 'e'
            Regex regex13 = new Regex(@"\b\w+e\b");
            MatchCollection matches13 = regex13.Matches(input);
            Console.WriteLine("13. Match words that end with 'e':");
            foreach (Match match in matches13) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 14. Match words that start with a capital letter
            Regex regex14 = new Regex(@"\b[A-Z][a-z]*\b");
            MatchCollection matches14 = regex14.Matches(input);
            Console.WriteLine("14. Match words that start with a capital letter:");
            foreach (Match match in matches14) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 15. Match words that contain the letter 'a'
            Regex regex15 = new Regex(@"\b\w*a\w*\b");
            MatchCollection matches15 = regex15.Matches(input);
            Console.WriteLine("15. Match words that contain the letter 'a':");
            foreach (Match match in matches15) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 16. Match words that have at least 4 characters
            Regex regex16 = new Regex(@"\b\w{4,}\b");
            MatchCollection matches16 = regex16.Matches(input);
            Console.WriteLine("16. Match words that have at least 4 characters:");
            foreach (Match match in matches16) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 17. Replace all vowels with '#'
            Regex regex17 = new Regex(@"[aeiouAEIOU]");
            string replaced17 = regex17.Replace(input, "#");
            Console.WriteLine("17. Replace all vowels with '#':");
            Console.WriteLine(replaced17);
            Console.WriteLine();

            // 18. Match words that are repeated
            Regex regex18 = new Regex(@"\b(\w+)\b.*\b\1\b");
            MatchCollection matches18 = regex18.Matches(input);
            Console.WriteLine("18. Match words that are repeated:");
            foreach (Match match in matches18) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 19. Match words that have exactly 3 characters
            Regex regex19 = new Regex(@"\b\w{3}\b");
            MatchCollection matches19 = regex19.Matches(input);
            Console.WriteLine("19. Match words that have exactly 3 characters:");
            foreach (Match match in matches19) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            // 20. Match words that have alternating vowels and consonants
            Regex regex20 = new Regex(@"\b(?:([aeiouAEIOU][^aeiouAEIOU])|([^aeiouAEIOU][aeiouAEIOU]))+\b");
            MatchCollection matches20 = regex20.Matches(input);
            Console.WriteLine("20. Match words that have alternating vowels and consonants:");
            foreach (Match match in matches20) {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();
        }
    }
    }
using System.Text.RegularExpressions;

namespace __basic1
{
    class Program
    {
        class MathOperations
        {
            public int AddNumbers(int intA, int intB)
            {
                int Addition = intA + intB;
                return Addition;
            }
            public int AddNumbers1(int intA, int intB) => intA * 2 + intB;
        }

        class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Weight { get; set; }

            public Item(int id, string name, double weight)
            {
                Id = id;
                Name = name;
                Weight = weight;
            }
        }

        static void Main()
        {

            int[,] board = {
            { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
            { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
            { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
            { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
            { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
            { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
            { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
            { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
            { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
        };

            Console.WriteLine(IsSudokuSolutionValid(board) ? "Valid" : "Invalid");


            static bool IsSudokuSolutionValid(int[,] board)
            {
                return Enumerable.Range(0, 9).All(i => IsValidRow(board, i) && IsValidColumn(board, i)) && ValidateSubGrids(board);
            }

            static bool IsValidRow(int[,] board, int rowIndex)
            {
                return Enumerable.Range(1, 9).All(n => Enumerable.Range(0, 9).Count(j => board[rowIndex, j] == n) == 1);
            }

            static bool IsValidColumn(int[,] board, int columnIndex)
            {
                return Enumerable.Range(1, 9).All(n => Enumerable.Range(0, 9).Count(j => board[j, columnIndex] == n) == 1);
            }

            static bool ValidateSubGrids(int[,] board)
            {
                return Enumerable.Range(0, 3).All(x => Enumerable.Range(0, 3).All(y => IsValidSubGrid(board, x * 3, y * 3)));
            }

            static bool IsValidSubGrid(int[,] board, int startX, int startY)
            {
                return Enumerable.Range(1, 9).All(n => Enumerable.Range(0, 3).Count(x => Enumerable.Range(0, 3).Count(y => board[startX + x, startY + y] == n) == 1) == 1);
            }


            int[] values = { 1, 2, 3, 4, 5 };
            bool all = values.All(v => v > 0); // Check if all values are greater than 0
            bool any = values.Any(v => v > 3); // Check if any value is greater than 3
            bool contains = values.Contains(2); // Check if values contains 2
            int[] defaultIfEmpty = values.DefaultIfEmpty(-1).ToArray(); // Returns values, or a sequence with -1 if values is empty UNCLEAR 
            IEnumerable<int> empty = Enumerable.Empty<int>(); // Creates an empty sequence UNCLEAR
            IEnumerable<int> range = Enumerable.Range(1, 5); // Creates a sequence from 1 to 5
            IEnumerable<int> repeat = Enumerable.Repeat(1, 3); // Creates a sequence with three 1's
            bool sequenceEqual = values.SequenceEqual(new[] { 1, 2, 3, 4, 5 }); // Check if sequences are equal
            IEnumerable<(int, int)> zip = values.Zip(new[] { 6, 7, 8, 9, 10 }, (a, b) => (a, b)); // Combines values with another sequence UNCLEAR 


            var people = new[] { new { Name = "A", Age = 25 }, new { Name = "B", Age = 25 }, new { Name = "C", Age = 30 } };
            var groupBy = people.GroupBy(p => p.Age); // Group people by age
            var toLookup = people.ToLookup(p => p.Age); // Create a lookup based on age




            int[] numbers4 = { 1, 2, 3, 4, 5 };
            var select = numbers4.Select(n => n * 2); // Multiplies each element by 2
            var selectMany = numbers4.SelectMany(n => new int[] { n, n * 2 }); // Flattens each element and its double into a single sequence


            Console.WriteLine(Regex.Replace("This is an example.", "[^aeiouAEIOU]", "_")); // Matches any character that is not a vowel (upper or lowercase) and replaces it with an underscore

            var financeData = new double[] { 1000, 2000, 3000, 4000, 5000 };
            var userInput = "finance";
            var data = userInput.ToLower() switch
            {
                "finance" => financeData,
                _ => throw new ArgumentException("Invalid input")
            };

            Array.ForEach(data, (value) =>
            {
                Console.WriteLine($"Data point: {value}");
                if (value >= 3000)
                {
                    Console.WriteLine("Exiting loop early");
                    return;
                }
                Console.WriteLine("Continuing loop");
            });

            int[] netIncomes = new int[] { 1000, 2000, 3000 };
            Array.ForEach(netIncomes, i => Console.WriteLine($"Net Income: {i}"));

            Func<double, double, double> calculateNetIncome = (revenue, expenses) => revenue - expenses;
            double netIncome = calculateNetIncome(1000000, 500000);






            List<Item> items = new List<Item>
        {
            new Item(1, "Apple", 1.2),
            new Item(2, "Orange", 1.5),
            new Item(3, "Banana", 1.1)
        };
            Func<List<Item>, Item> getHeaviestItem = itemList => itemList.OrderByDescending(item => item.Weight).FirstOrDefault();
            Item heaviestItem = getHeaviestItem(items);

            //Example of Func<List<int>, Dictionary<string, double>, Tuple<int, string>>
            List<int> numbers3 = new List<int> { 1, 2, 3 };
            Dictionary<string, double> weights = new Dictionary<string, double>
        {
            { "Apple", 1.2 },
            { "Orange", 1.5 },
            { "Banana", 1.1 }
        };

            Func<List<int>, Dictionary<string, double>, Tuple<int, string>> getMaxWeightItem = (nums, wghts) =>
            {
                int maxIndex = nums.IndexOf(nums.Max());
                string maxWeightKey = wghts.OrderByDescending(x => x.Value).ElementAt(maxIndex).Key;
                return Tuple.Create(maxIndex, maxWeightKey);
            };
            Tuple<int, string> result1 = getMaxWeightItem(numbers3, weights);
            //

            List<Dictionary<string, int>> listOfDictionaries = new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> { { "A", 1 }, { "B", 2 } },
            new Dictionary<string, int> { { "C", 3 }, { "D", 4 }, { "E", 5 } },
            new Dictionary<string, int> { { "F", 6 }, { "G", 7 } }
        };
            Predicate<Dictionary<string, int>> hasEvenCount1 = dict => dict.Count % 2 == 0;
            List<Dictionary<string, int>> evenCountDictionaries = listOfDictionaries.FindAll(hasEvenCount1);

            List<List<int>> listOfLists = new List<List<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6, 7 },
            new List<int> { 8, 9 }
        };
            Predicate<List<int>> hasEvenCount = list => list.Count % 2 == 0;
            List<List<int>> evenCountLists = listOfLists.FindAll(hasEvenCount);
            foreach (var list in evenCountLists)
            {
                Console.WriteLine(string.Join(", ", list));
            }

            List<int> numbers2 = new List<int> { 1, 2, 3, 4, 5 };
            Converter<int, string> intToString = num => $"Number {num}";
            List<string> numberStrings = numbers2.ConvertAll(intToString);

            List<string> names = new List<string> { "Zuck", "Yellow", "Alice", "Charlie", "Bob" };
            Comparison<string> compareLength = (str1, str2) => str1.Length.CompareTo(str2.Length);  //Comparison<string> is a delegate and this is how it is defined.
            names.Sort(compareLength);
            names.ForEach(Console.WriteLine);

            List<int> numbers1 = new List<int> { 1, 2, 3, 4, 5 };
            Predicate<int> isEven = num => num % 2 == 0;  //this is like defining a function
            List<int> evenNumbers1 = numbers1.FindAll(isEven);


            Func<int, int, int> add = (x, y) => x + y;
            int result = add(2, 3);

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            List<int> evenNumbers = numbers.FindAll(x => x % 2 == 0);

            Dictionary<string, int> ages = new() { { "Alice", 30 }, { "Bob", 25 } };
            ages["Charlie"] = 22; // Insert
            int ageAlice = ages["Alice"]; // Look-up
            ages["Alice"] = 35; // Modify
            ages.Remove("Bob"); // Delete
            //foreach (var entry in ages) // Iterate
            //{
            //    Console.WriteLine($"{entry.Key}: {entry.Value}");
            //}
            Action<string, int> printDetails = (name, age) => Console.WriteLine($"Name: {name}, Age: {age}");
            printDetails("Charlie", ages["Charlie"]);





            MathOperations mathOperations = new MathOperations();
            Console.WriteLine("Enter two numbers:");
            //int num1 = int.Parse(Console.ReadLine());
            //int num2 = int.Parse(Console.ReadLine());
            string[] input = Console.ReadLine().Split(' ');
            int num1 = int.Parse(input[0]);
            int num2 = int.Parse(input[1]);
            Console.WriteLine($"Sum of two numbers is :{mathOperations.AddNumbers1(num1, num2)}");
        }
    }
}
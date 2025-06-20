namespace _Linq_SocksMatch_HR {
    using System;
    using System.Linq;

    class Program {
        static void Main(string[] args) {
            int[] socks = { 10, 20, 20, 10, 10, 30, 50, 10, 20 };

            int pairs = socks
                 .GroupBy(color => color)
                 .Select(group=>group.Count()/2)
                 .Sum();

            Dictionary<int, int> colorCounts = socks
                                                .GroupBy(color => color)
                                                .ToDictionary(group => group.Key, group => group.Count());
            int pairs1 = colorCounts.Values.Sum(count => count/2);

            Console.WriteLine("Number of pairs: " + pairs);
            /* ALTERNATE
                     int[] distinctIntegers = ar.Distinct().ToArray();
int[] counts = distinctIntegers.Select(x => ar.Count(y => y == x)).ToArray();
int[] counts1 = counts.Select(x => x / 2).ToArray();
int counts3 = counts1.Sum();
return counts3;
             */



        }
    }
}
namespace _HR_RepeatingString {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine(repeatedString("a", 1000));
        }

        public static long repeatedString(string s, long n) {
            int count = s.Count(c=>c=='a');
            int total = (int)(n / s.Length) * count;
            string sRemaining = s.Substring(0, (int)(n % s.Length));
            int countRemaining = sRemaining.Count(c => c == 'a');
            total = total +countRemaining;

            return total;

        }


    }
}
namespace _Linq_Valleys_HR {
    using System;
    using System.Linq;

    class Program {
        static void Main(string[] args) {
            string steps = "UDDUUUUDD"; var stepsChar = new List<char>(steps);
            var stepsInt = new int[stepsChar.Count];
            for (int i = 0; i < stepsChar.Count; i++) {
                if (stepsChar[i] == 'D') { stepsInt[i] = -1; }
                 else if (stepsChar[i] == 'U') { stepsInt[i] = 1; }
                             }

            int[] ps = new int[stepsInt.Length];
            int currentSum = 0;
            for (int i = 0; i < stepsInt.Length; i++) {
                currentSum += stepsInt[i];
                ps[i] = currentSum;
            }

            int count = 0; bool wasNegative=false;
            for (int i = 1; i < ps.Length; i++) {
                if (ps[i - 1] >= 0 && ps[i] < 0) { wasNegative = true; } else if (ps[i - 1] < 0 && ps[i] >= 0 && wasNegative) { count++; wasNegative = false; }
            }   



            Console.WriteLine("Number of pairs: ");

        }
    }
}
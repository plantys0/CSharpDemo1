namespace _Linq_Valleys_HR_2 {
    using System;
    using System.Linq;
    //@@@tbd - to check
    class Program {
        static void Main(string[] args) {
            string steps = "UDDUUUUDD";
            int altitude = 0; int valleys = 0; bool wasNegative = false;
            for (int i = 0; i < steps.Length; i++) {

                if (altitude == 0 && steps[i] == 'D') { wasNegative = true; }
                if (altitude ==-1 && steps[i] == 'U'  && wasNegative) { valleys++; wasNegative = false; }
                if (steps[i] == 'D') { altitude--; } else if (steps[i] == 'U') { altitude++; }

            }


            Console.WriteLine("Number of pairs: ");

        }
    }
}
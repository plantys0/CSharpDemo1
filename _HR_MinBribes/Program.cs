using System.Collections;

namespace _HR_MinBribes {
    internal class Program {
        static void Main(string[] args) {
            List<int> final = new List<int> { 2, 1, 5, 3, 4 };
            List<int> initial = Enumerable.Range(1, final.Count).ToList();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i=1; i<final.Count+1; i++) { dict.Add(i, 0); }

            //Swap(final,0);
            int count = 0;
            bool jobDone;

            for (int j = 0; j < final.Count - 1; j++) {
                for (int i = 0; i < final.Count - 1; i++) {
                    if (final[i] > final[i + 1] && dict[final[i]] < 2) { Swap(final, i); count++; dict[final[i + 1]]++;  }
                }
            }

            jobDone = initial.SequenceEqual(final);
            //if (final.Equals(initial)) { jobDone = true; }
            //Console.WriteLine("Hello, World!");
        }
        public static void minimumBribes(List<int> q) {

        }

static void Swap(List<int> list, int index1) {int temp = list[index1];list[index1] = list[index1+1];list[index1+1] = temp;}
    }
}
namespace _HR_MinBribes_fin {
    using System;

    class Solution {
        static void minimumBribes(int[] q) {
            int bribes = 0;
            bool chaotic = false;
            for (int i = q.Length - 1; i >= 0; i--) {
                if (q[i] - (i + 1) > 2) {
                    chaotic = true;
                    break;
                }
                for (int j = Math.Max(0, q[i] - 2); j < i; j++) {
                    if (q[j] > q[i]) {
                        bribes++;
                    }
                }
            }
            if (chaotic) {
                Console.WriteLine("Too chaotic");
            } else {
                Console.WriteLine(bribes);
            }
        }

        static void Main(string[] args) {
            // Sample input
            int[][] queues = new int[][] {
            new int[] {2, 1, 5, 3, 4},  //3
            new int[] {2, 5, 1, 3, 4},  //too chaotic
            new int[] {5, 1, 2, 3, 7, 8, 6, 4}  //too chaotic
        };

            // Loop through queues and call minimumBribes for each one
            for (int i = 0; i < queues.Length; i++) {
                Console.WriteLine("Queue #" + (i + 1));
                minimumBribes(queues[i]);
                Console.WriteLine();
            }
        }
    }

}
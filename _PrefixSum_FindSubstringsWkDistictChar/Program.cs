namespace _PrefixSum_FindSubstringsWkDistictChar {
    using System;

    class Program {
        static void Main(string[] args) {
            string str = "abbcabb";
            int k = 2;

            int n = str.Length;
            int[] prefixSum = new int[n + 1];

            // Initialize prefix sum array
            for (int i = 1; i <= n; i++) {
                prefixSum[i] = prefixSum[i - 1] + str[i - 1] - 'a';
            }

            // Calculate number of substrings with exactly k distinct characters
            int count = 0;
            int[] freq = new int[26];
            for (int i = 0; i < n; i++) {
                freq[str[i] - 'a']++;
                if (i >= k - 1) {
                    if (i >= k) {
                        freq[str[i - k] - 'a']--;
                    }
                    int distinct = prefixSum[i + 1] - prefixSum[i - k + 1];
                    if (distinct >= 0 && distinct < 26 && freq[distinct] == 1) {
                        count++;
                    }
                }
            }

            Console.WriteLine("Number of substrings with exactly " + k + " distinct characters: " + count);
        }
    }


}
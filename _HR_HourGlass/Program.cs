using System.Diagnostics.CodeAnalysis;

namespace _HR_HourGlass {
    internal class Program {
        static void Main(string[] args) {
            List<List<int>> matrix = new List<List<int>>
{
            new List<int> {11, 12, 13, 14, 15, 16},
            new List<int> {17, 18, 19, 10, 11, 12},
            new List<int> {13, 14, 15, 16, 17, 18},
            new List<int> {19, 20, 21, 22, 23, 24},
            new List<int> {25, 26, 27, 28, 29, 30},
            new List<int> {31, 32, 33, 34, 35, 36}
        };
            int hourGlassSum = 0;
            int maxHourGlassSum = 0;
            for (int l=0; l<4; l++) {
                for (int m = 0; m < 4; m++) {
                    hourGlassSum = matrix[l][m] + matrix[l][m + 1] + matrix[l][m + 2] + matrix[l + 1][m + 1] + matrix[l + 2][m] + matrix[l + 2][m + 1] + matrix[l + 2][m + 2];
                    maxHourGlassSum = Math.Max(hourGlassSum, maxHourGlassSum);
                }
                }

                Console.WriteLine("Hello, World!");
        }
        //public static int hourglassSum(List<List<int>> arr) {


        //}


    }
}
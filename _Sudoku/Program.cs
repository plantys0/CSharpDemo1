namespace _Sudoku
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Sample Sudoku solution
            char[,] board = {
        {'5','3','4','6','7','8','9','1','2'},
        {'6','7','2','1','9','5','3','4','8'},
        {'1','9','8','3','4','2','5','6','7'},
        {'8','5','9','7','6','1','4','2','3'},
        {'4','2','6','8','5','3','7','9','1'},
        {'7','1','3','9','2','4','8','5','6'},
        {'9','6','1','5','3','7','2','8','4'},
        {'2','8','7','4','1','9','6','3','5'},
        {'3','4','5','2','8','6','1','7','9'}
    };

            bool isValid = IsValidSudoku(board);
            Console.WriteLine(isValid ? "Valid Sudoku board." : "Invalid Sudoku board.");
        }

        public static bool IsValidSudoku(char[,] board)
        {
            HashSet<char>[,] rows = new HashSet<char>[9, 9];
            HashSet<char>[,] cols = new HashSet<char>[9, 9];
            HashSet<char>[,] boxes = new HashSet<char>[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    rows[i, j] = new HashSet<char>();
                    cols[i, j] = new HashSet<char>();
                    boxes[i, j] = new HashSet<char>();
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char num = board[i, j];
                    int boxIdx = (i / 3) * 3 + (j / 3);
                    if (rows[i, j].Contains(num) || cols[j, i].Contains(num) || boxes[boxIdx, i / 3 * 3 + j / 3].Contains(num)) return false;
                    rows[i, j].Add(num); cols[j, i].Add(num); boxes[boxIdx, i / 3 * 3 + j / 3].Add(num);
                }
            }
            return true;
        }

    }
}
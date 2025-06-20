namespace _HR_JumpingCloud {
    internal class Program {
        static void Main(string[] args) {

            var cloud = new int[] { 0, 0, 1, 0, 0, 1, 0 };
            int len = cloud.Length - 1;
            int numJumps = 0; int curPos = 0;
            do {
                if (cloud[curPos + 2] == 0 && curPos + 2 <= len) { curPos = curPos + 2; numJumps++; } else if (cloud[curPos + 1] == 0 && curPos + 1 <= len) { curPos = curPos + 1; numJumps++; }
            } while (curPos < len);

        }
    }
}
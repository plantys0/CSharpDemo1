namespace _HR_RotatedArray {
    internal class Program {
        static void Main(string[] args) {
            List<int> arr = new List<int> { 1,2,3,4,5,6,7,8,9};
            List<int> arrResult = rotLeft(arr, 3);
            Console.WriteLine("Hello, World!");
                    }

        public static List<int> rotLeft(List<int> a, int d) {
            Queue<int> q = new Queue<int>(a);
            for (int i=0;i<d; i++) {
                var item = q.Dequeue();
                q.Enqueue(item);
            }
            return q.ToList();
        }
        public static void Swap(ref int a, ref int b) { int temp = a; a = b; b = temp; }
    }
}
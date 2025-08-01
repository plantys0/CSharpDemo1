namespace AAA  // Change to match your project's namespace (e.g., from Project Properties > Application > Default namespace)
{
    public class GarbageCollection
    {
        public static void Main(string[] args)  // Added 'public'
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i < 10000; i++) // Loop to allocate memory
            {
                list.Add(new byte[1000]); // Create small objects (Gen 0)
                if (i % 1000 == 0) Console.WriteLine($"Allocated {i} items"); // Print progress
            }
            Console.WriteLine("Press Enter to exit..."); // Pause to observe
            Console.ReadLine();
        }
    }
}
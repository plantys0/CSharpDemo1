class ArrayMethodsExamples
{
    static void Main()
    {
        // Creation
        Array arr = Array.CreateInstance(typeof(int), 5);
        int[] arrInt = { 1, 3, 5, 7, 9 };
        int target = 5;

        // Searching
        int binarySearchIndex = Array.BinarySearch(arrInt, target);
        int find = Array.Find(arrInt, x => x > 0);
        int[] findAll = Array.FindAll(arrInt, x => x > 0);
        int findIndex = Array.FindIndex(arrInt, x => x > 0);
        int findLast = Array.FindLast(arrInt, x => x > 0);
        int findLastIndex = Array.FindLastIndex(arrInt, x => x > 0);

        // Sorting
        Array.Sort(arrInt);
        Array.Reverse(arrInt);

        // Manipulation
        Array.Resize(ref arrInt, 7);
        int[] destinationArray = new int[arrInt.Length];
        Array.Copy(arrInt, destinationArray, arrInt.Length);
        Array.Clear(destinationArray, 0, destinationArray.Length);

        // Information
        int getLength = arrInt.GetLength(0);
        long getLongLength = arrInt.GetLongLength(0);
        int indexOf = Array.IndexOf(arrInt, target);
        int lastIndexOf = Array.LastIndexOf(arrInt, target);

        // Conversion
        var readOnly = Array.AsReadOnly(arrInt);
        int[] clonedArray = (int[])arrInt.Clone();
        int[] convertAll = Array.ConvertAll(arrInt, x => x * 2);

        // Iteration
        var enumerator = arrInt.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Console.WriteLine($"Iterating: {enumerator.Current}");
        }

        // Typing
        Type type = arrInt.GetType();

        // Comparison
        int[] arrInt2 = { 1, 3, 5, 7, 9 };
        bool areEqual = arrInt.Equals(arrInt2);
        bool areSameInstance = ReferenceEquals(arrInt, arrInt2);

        Console.WriteLine($"BinarySearch: {binarySearchIndex}");
        Console.WriteLine($"Find: {find}");
        Console.WriteLine($"FindAll: {string.Join(", ", findAll)}");
        Console.WriteLine($"FindIndex: {findIndex}");
        Console.WriteLine($"FindLast: {findLast}");
        Console.WriteLine($"FindLastIndex: {findLastIndex}");
        Console.WriteLine($"Sorted Array: {string.Join(", ", arrInt)}");
        Console.WriteLine($"GetLength: {getLength}");
        Console.WriteLine($"GetLongLength: {getLongLength}");
        Console.WriteLine($"IndexOf: {indexOf}");
        Console.WriteLine($"LastIndexOf: {lastIndexOf}");
        Console.WriteLine($"GetType: {type}");
        Console.WriteLine($"Are arrays equal? {areEqual}");
        Console.WriteLine($"Are arrays the same instance? {areSameInstance}");
    }
}

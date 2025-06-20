namespace _QuickSort
{
    using System;

    class QuickSort
    {
        static void Main()
        {
            int[] arr = { 9, 7, 5, 11, 12, 2, 14, 3, 10, 6 };
            QuickSortArray(arr, 0, arr.Length - 1);
            foreach (int i in arr) Console.Write(i + " ");
        }

        static void QuickSortArray(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(arr, low, high);
                QuickSortArray(arr, low, pivot - 1);
                QuickSortArray(arr, pivot + 1, high);
            }
        }

        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high], i = low - 1;
            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] <= pivot) { i++; Swap(ref arr[i], ref arr[j]); }
            }
            Swap(ref arr[i + 1], ref arr[high]);
            return i + 1;
        }

        static void Swap(ref int a, ref int b) { int temp = a; a = b; b = temp; }
    }

}
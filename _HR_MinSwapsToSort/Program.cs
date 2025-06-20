using System.Data;
/*Initialize a variable swaps to 0.
Loop through the array from the beginning to the end:
a. If the current element is in its correct position (i.e. the value at index i is i+1), continue to the next element.
b. If the current element is not in its correct position, swap it with the element at the index that should contain its value.
c. Increment swaps by 1.
d. Repeat step 2a until the current element is in its correct position.
Return the value of swaps.*/
namespace _HR_MinSwapsToSort {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = new int[] { 7, 1, 3, 2, 4, 5, 6 };
            int temp0 = Array.IndexOf(arr, 7);
            int temp1 = Array.IndexOf(arr, 3);
            int temp2 = Array.IndexOf(arr, 2);
            int temp3 = arr[0];


            for (int i=0;i<arr.Length;i++) {
                if (arr[i] != i+1 ) { 
                    swap(arr,i,Array.IndexOf(arr, i+1));
                   }
            }
            
            
            
            Console.WriteLine("Hello, World!");

            static void swap(int[] arr, int int1, int int2) {
                int temp = arr[int1];
                arr[int1] = arr[int2];
                arr[int2] = temp;
                            }



        }
    }
}
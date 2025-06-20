namespace _LinkedList_Reverse {
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main(string[] args) {
            // Input
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4, 5 });

            // Reverse the linked list
            LinkedListNode<int> current = list.First;
            LinkedListNode<int> prev = null;
            while (current != null) {
                var next = current.Next;
                list.Remove(current);  // remove the current node from the list
                current = list.AddFirst(current.Value); // add the current node to the beginning of the list
                prev = current;
                current = next;
            }

            // Output
            Console.WriteLine($"Reversed list: [{string.Join(", ", list)}]");

            /* Expected output:
               Reversed list: [5, 4, 3, 2, 1]
            */
        }
    }

}
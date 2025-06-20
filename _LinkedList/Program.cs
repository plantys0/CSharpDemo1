namespace _LinkedList {
    using System;
    using System.Linq;

    class Node { public int Data; public Node Next; public Node(int d) { Data = d; } } // Node class

    class LinkedList {
        Node head;
        public void Insert(int data) { head = (head == null) ? new Node(data) : new Node(data) { Next = head }; } // Insert method
        public Node Reverse() { Node prev = null; Node curr = head; Node next=null; while (curr != null) { next = curr.Next; curr.Next = prev; prev = curr; curr = next; } head = prev; return head; } // Reverse method
        public int MiddleElement() { return Enumerable.Range(0, Int32.MaxValue).Select(x => (head = head?.Next)).FirstOrDefault(x => (head = head?.Next) == null)?.Data ?? -1; } // MiddleElement method
    }

    class Program {
        static void Main(string[] args) {
            LinkedList list = new LinkedList();
            list.Insert(1); 
            list.Insert(2); 
            list.Insert(3);
            list.Insert(4); list.Insert(5); // Sample input: 5 -> 4 -> 3 -> 2 -> 1
            list.Reverse(); // Reverses the linked list: 1 -> 2 -> 3 -> 4 -> 5
            Console.WriteLine(list.MiddleElement()); // Sample output: 3
        }
    }

}
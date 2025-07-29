using System;

// Define the ListNode class (like a chain link)  
public class ListNode
{
    public int val;  // The number it holds  
    public ListNode next;  // Link to next node  
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}

class Program
{
    // Function to merge two sorted lists  
    public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Create a dummy node to start the merged list  
        ListNode dummy = new ListNode();
        ListNode current = dummy;  // Pointer to build the list  

        // Loop while both lists have nodes  
        while (list1 != null && list2 != null)
        {
            if (list1.val <= list2.val)
            {  // Pick smaller from list1  
                current.next = list1;
                list1 = list1.next;  // Move forward in list1  
            }
            else
            {  // Pick smaller from list2  
                current.next = list2;
                list2 = list2.next;  // Move forward in list2  
            }
            current = current.next;  // Move our builder pointer  
        }

        // If one list is left, attach the rest  
        if (list1 != null)
        {
            current.next = list1;
        }
        else
        {
            current.next = list2;
        }

        return dummy.next;  // Return the real start (skip dummy)  
    }

    static void Main()
    {
        // Hardcoded Input 1: Create list1 = [1, 2, 4]  
        ListNode list1 = new ListNode(1);
        list1.next = new ListNode(2);
        list1.next.next = new ListNode(4);

        // Hardcoded Input 2: Create list2 = [1, 3, 4]  
        ListNode list2 = new ListNode(1);
        list2.next = new ListNode(3);
        list2.next.next = new ListNode(4);

        // Merge them  
        ListNode merged = MergeTwoLists(list1, list2);

        // Print the merged list  
        Console.Write("Merged List: ");
        ListNode current = merged;
        while (current != null)
        {
            Console.Write(current.val + " ");  // Output: 1 1 2 3 4 4  
            current = current.next;
        }
    }
}
using System;

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}

class Program
{
    static void Main()
    {
        var myNode = new ListNode(1);
        myNode.next = new ListNode(2);
        myNode.next.next = new ListNode(3);
        Console.WriteLine("End");

    }
}
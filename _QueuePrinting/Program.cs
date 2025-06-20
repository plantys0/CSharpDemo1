namespace _QueuePrinting {
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main() {
            Queue<string> queue = new Queue<string>();

            while (true) {
                Console.WriteLine("Enter '1' to add a document to the queue, '2' to print the next document, or '3' to exit:");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1) {
                    Console.Write("Enter document name: ");
                    string document = Console.ReadLine();
                    queue.Enqueue(document);
                    Console.WriteLine("{0} has been added to the queue.", document);
                } else if (choice == 2) {
                    if (queue.Count == 0) {
                        Console.WriteLine("Queue is empty.");
                    } else {
                        string nextDocument = queue.Dequeue();
                        Console.WriteLine("Printing {0}...", nextDocument);
                    }
                } else if (choice == 3) {
                    Console.WriteLine("Exiting...");
                    break;
                } else {
                    Console.WriteLine("Invalid choice.");
                }
            }

            Console.WriteLine("Queue contents:");
            foreach (string document in queue) {
                Console.WriteLine(document);
            }

            Console.WriteLine("Copying queue contents to array...");
            string[] queueArray = queue.ToArray();

            Console.WriteLine("Queue count: {0}", queue.Count);
            Console.WriteLine("Queue contains 'document1': {0}", queue.Contains("document1"));

            Console.WriteLine("Clearing queue...");
            queue.Clear();
            Console.WriteLine("Queue count: {0}", queue.Count);
        }
    }

}

namespace AAA
{
    public class a05_expressionBody
    {
        // This file demonstrates examples of each type of expression-bodied members in C# .NET 8,
        // as described in the Microsoft documentation. It defines a 'Person' class that uses these
        // concepts to manage and display information about a person, such as name, age, nicknames,
        // and changes to the name. The console app creates a person, modifies data, and shows outputs.
        // URL: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members

        class Person
        {
            // Private fields
            private string fname;
            private string lname;
            private int birthYear;
            private string[] nicknames = new string[5];
            private EventHandler changedGeneric;

            // Expression-bodied constructor
            public Person(string first, string last, int year) => (fname, lname, birthYear) = (first, last, year);

            // Expression-bodied finalizer
            ~Person() => Console.WriteLine($"Finalizing person: {ToString()}");

            // Expression-bodied method (returns string)
            public override string ToString() => $"{fname} {lname}".Trim();

            // Expression-bodied void method
            public void DisplayInfo() => Console.WriteLine($"Name: {ToString()}, Age: {Age}");

            // Expression-bodied read-only property
            public int Age => DateTime.Now.Year - birthYear;

            // Expression-bodied property with get and set
            public string FullName
            {
                get => $"{fname} {lname}";
                set
                {
                    var parts = value.Split(' ');
                    if (parts.Length >= 2)
                    {
                        fname = parts[0];
                        lname = parts[1];
                    }
                    else
                    {
                        fname = value;
                        lname = string.Empty;
                    }
                    changedGeneric?.Invoke(this, EventArgs.Empty); // Trigger event on change (fixed)
                }
            }

            // Expression-bodied indexer
            public string this[int i]
            {
                get => nicknames[i];
                set => nicknames[i] = value;
            }

            // Expression-bodied event add/remove
            public event EventHandler Changed
            {
                add => changedGeneric += value;
                remove => changedGeneric -= value;
            }
        }

        public static void Main()
        {
            // Top-level statements for console app
            Person person = new Person("John", "Doe", 1990);
            person.DisplayInfo();
            Console.WriteLine($"Age: {person.Age}"); // Read-only property

            person[0] = "Johnny"; // Indexer set
            Console.WriteLine($"Nickname[0]: {person[0]}"); // Indexer get

            person.Changed += (s, e) => Console.WriteLine("Name changed event triggered!"); // Subscribe to event
            person.FullName = "Jane Smith"; // Property set, triggers event
            person.DisplayInfo();

            // Force GC to potentially run finalizer (may not always show in debug)
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
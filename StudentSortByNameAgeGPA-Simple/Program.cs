namespace StudentSortByNameAgeGPA_Simple
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Student> students = new List<Student>
        {
            new Student { Name = "Alice", Age = 20, GPA = 3.5 },
            new Student { Name = "Bob", Age = 23, GPA = 3.8 },
            new Student { Name = "Charlie", Age = 22, GPA = 3.2 },
            new Student { Name = "Alice", Age = 25, GPA = 3.1 }
        };

            // Sort students by name ascending, and age descending
            students.Sort((s1, s2) =>
            {
                int nameComparison = s1.Name.CompareTo(s2.Name);
                if (nameComparison == 0)
                {
                    // If names are equal, compare by age descending
                    return s2.Age.CompareTo(s1.Age);
                }

                return nameComparison;
            });

            Console.WriteLine("Sorted by name (ascending) and age (descending):");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));

            // Sort students by GPA ascending
            students.Sort((s1, s2) => s1.GPA.CompareTo(s2.GPA));
            Console.WriteLine("Sorted by GPA (ascending):");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));

            // Sort students by GPA descending
            students.Sort((s1, s2) => s2.GPA.CompareTo(s1.GPA));
            Console.WriteLine("Sorted by GPA (descending):");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));
        }
    }

}
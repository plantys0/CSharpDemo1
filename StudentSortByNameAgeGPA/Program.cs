namespace StudentSortByNameAgeGPA
{


    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }
    }

    public class Program
    {
        public static void SortStudents(List<Student> students, Comparison<Student> comparer)
        {
            students.Sort(comparer);
        }

        public static void Main(string[] args)
        {
            List<Student> students = new List<Student>
        {
            new Student { Name = "Alice", Age = 20, GPA = 3.5 },
            new Student { Name = "Bob", Age = 23, GPA = 3.8 },
            new Student { Name = "Charlie", Age = 22, GPA = 3.2 }
        };

            // Sort students by name
            SortStudents(students, (s1, s2) => s1.Name.CompareTo(s2.Name));
            Console.WriteLine("Sorted by name:");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));

            Console.WriteLine();

            // Sort students by age
            SortStudents(students, (s1, s2) => s1.Age.CompareTo(s2.Age));
            Console.WriteLine("Sorted by age:");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));

            Console.WriteLine();

            // Sort students by GPA
            SortStudents(students, (s1, s2) => s1.GPA.CompareTo(s2.GPA));
            Console.WriteLine("Sorted by GPA:");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));
        }
    }

}
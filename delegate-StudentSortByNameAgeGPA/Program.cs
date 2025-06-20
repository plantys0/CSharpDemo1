namespace delegate_StudentSortByNameAgeGPA
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
        // Delegate to represent the sorting criteria for students
        public delegate int StudentComparer(Student s1, Student s2);

        // Sorting method that takes a list of students and the sorting criteria delegate
        /*In the given example, the SortStudents method takes two parameters: a List<Student> called students and a delegate called comparer. The comparer is a delegate of type StudentComparer, which is defined to take two Student objects as parameters and return an int. This delegate will be used to compare two students in the sorting algorithm.

    The line students.Sort((s1, s2) => comparer(s1, s2)); calls the Sort method on the List<Student> object. The Sort method takes a Comparison<Student> delegate as a parameter, which is defined as public delegate int Comparison<in T>(T x, T y);.

    In this case, we are providing a lambda expression (s1, s2) => comparer(s1, s2) that takes two Student objects (s1 and s2) as input and calls the comparer delegate with these two students. This lambda expression matches the Comparison<Student> delegate signature, which is why it can be passed as a parameter to the Sort method.

    In summary, the SortStudents method sorts the list of students based on the comparison logic provided by the comparer delegate. The lambda expression (s1, s2) => comparer(s1, s2) is used to match the delegate signature required by the Sort method and delegate the comparison logic to the comparer. */
        public static void SortStudents(List<Student> students, StudentComparer comparer)
        {
            students.Sort((s1, s2) => comparer(s1, s2));
        }

        public static int CompareByName(Student s1, Student s2)
        {
            return s1.Name.CompareTo(s2.Name);
        }

        public static int CompareByAge(Student s1, Student s2)
        {
            return s1.Age.CompareTo(s2.Age);
        }

        public static int CompareByGPA(Student s1, Student s2)
        {
            return s1.GPA.CompareTo(s2.GPA);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            List<Student> students = new List<Student>
        {
            new Student { Name = "Alice", Age = 20, GPA = 3.5 },
            new Student { Name = "Bob", Age = 23, GPA = 3.8 },
            new Student { Name = "Charlie", Age = 22, GPA = 3.2 }
        };

            // Sort students by name
            SortStudents(students, CompareByName);
            Console.WriteLine("Sorted by name:");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));

            Console.WriteLine();

            // Sort students by age
            SortStudents(students, CompareByAge);
            Console.WriteLine("Sorted by age:");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));

            Console.WriteLine();

            // Sort students by GPA
            SortStudents(students, CompareByGPA);
            Console.WriteLine("Sorted by GPA:");
            students.ForEach(s => Console.WriteLine($"{s.Name}, {s.Age}, {s.GPA}"));
        }
    }

}
using System;
using System.Collections.Generic;

namespace LanguageLearningDemo
{
    // Base class
    public class LanguageStudent
    {
        // Protected list to store languages, accessible in derived classes
        protected List<string> Languages { get; private set; }

        // Constructor
        public LanguageStudent()
        {
            Languages = new List<string>();
        }

        // Method to add a language
        public virtual void AddLanguage(string language)
        {
            Languages.Add(language);
            Console.WriteLine($"{GetType().Name} learned {language}");
        }

        // Method to display known languages
        public virtual void DisplayLanguages()
        {
            Console.WriteLine($"{GetType().Name} knows: {string.Join(", ", Languages)}");
        }
    }

    // Derived class, inheriting from LanguageStudent
    public class LanguageTeacher : LanguageStudent
    {
        // Constructor
        public LanguageTeacher() : base()
        {
            // The base() call is implicit, but shown here for clarity
        }

        // Method to teach a language to a student
        public bool Teach(LanguageStudent student, string languageToTeach)
        {
            if (Languages.Contains(languageToTeach))
            {
                student.AddLanguage(languageToTeach);
                Console.WriteLine($"Teacher successfully taught {languageToTeach}");
                return true;
            }
            Console.WriteLine($"Teacher doesn't know {languageToTeach}, can't teach it");
            return false;
        }

        // Override the AddLanguage method
        public override void AddLanguage(string language)
        {
            base.AddLanguage(language);
            Console.WriteLine($"Teacher is now qualified to teach {language}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a teacher
            LanguageTeacher teacher = new LanguageTeacher();
            teacher.AddLanguage("English");
            teacher.AddLanguage("Spanish");

            // Create students
            LanguageStudent student1 = new LanguageStudent();
            LanguageStudent student2 = new LanguageStudent();

            // Display initial languages
            Console.WriteLine("\nInitial state:");
            teacher.DisplayLanguages();
            student1.DisplayLanguages();
            student2.DisplayLanguages();

            // Teacher teaches languages
            Console.WriteLine("\nTeaching attempts:");
            teacher.Teach(student1, "English");
            teacher.Teach(student2, "Spanish");
            teacher.Teach(student1, "French"); // This should fail

            // Display final languages
            Console.WriteLine("\nFinal state:");
            teacher.DisplayLanguages();
            student1.DisplayLanguages();
            student2.DisplayLanguages();

            Console.ReadLine(); // Keep console window open
        }
    }
}
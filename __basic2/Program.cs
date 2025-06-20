namespace __basic2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Person(string name, int age, string email) { Name = name; Age = age; Email = email; }
        public bool IsValidEmail() { return Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"); }
    }

    class Group
    {
        public string Name { get; set; }
        public List<Person> Members { get; set; }
        public Group(string name) { Name = name; Members = new List<Person>(); }
        public void AddMember(Person person) { Members.Add(person); }
        public IEnumerable<Person> GetAdults() { return Members.Where(m => m.Age >= 18); }
        public double AverageAge() { return Members.Average(m => m.Age); }
    }

    class Program
    {
        static async Task LoadDataAsync(Group group)
        {
            await Task.Delay(1000); // Simulate data loading delay
            var people = new List<Person> {
            new Person("Alice", 30, "alice@example.com"),
            new Person("Bob", 15, "bob@example.com"),
            new Person("Charlie", 22, "charlie@example.com"),
            new Person("Diana", 42, "diana@example.com"),
            new Person("Eva", 17, "eva@example.com")
        };
            foreach (var person in people) { group.AddMember(person); }
        }

        static void Main(string[] args)
        {
            try
            {
                var group = new Group("My Group");
                LoadDataAsync(group).Wait();

                Console.WriteLine($"Group Name: {group.Name}");
                Console.WriteLine("Adults in the group:");
                foreach (var adult in group.GetAdults()) { Console.WriteLine(adult.Name); }
                Console.WriteLine($"Average age in the group: {group.AverageAge()}");

                var adultsByName = group.GetAdults().OrderBy(a => a.Name);
                Console.WriteLine("Adults sorted by name:");
                foreach (var adult in adultsByName) { Console.WriteLine(adult.Name); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
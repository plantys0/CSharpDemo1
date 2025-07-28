using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/list-collection#code-try-0

List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30, City = "London" },
            new Person { Name = "Bob", Age = 25, City = "Paris" },
            new Person { Name = "Charlie", Age = 30, City = "London" },
            new Person { Name = "David", Age = 35, City = "New York" },
            new Person { Name = "Eve", Age = 25, City = "Paris" }
        };

// a) IOrderedEnumerable for chaining sorts
IOrderedEnumerable<Person> orderedPeople = people.OrderBy(p => p.Age).ThenBy(p => p.Name); // IOrderedEnumerable<Person> to allow ThenBy chaining

// b) IQueryable for queryable sources (in-memory example)
IQueryable<Person> queryablePeople = people.AsQueryable().Where(p => p.Age > 20); // IQueryable<Person> for expression-tree based queries, usable for DBs too

// c) IDictionary as return type for flexibility - can be Dictionary,  SortedDictionary, ReadOnlyDictionary
//Simple Explanation: Dictionary<TKey, TValue> is like a fast lookup table (hash-based, no order). SortedDictionary<TKey, TValue> keeps keys automatically sorted (tree-based, like a balanced list).
//Structure: Dictionary uses hashing(quick, O(1) access); SortedDictionary uses a binary tree (O(log n) access, but sorted).
//Order: Dictionary: unordered(random - ish); SortedDictionary: always sorted by keys(using IComparable or custom comparer).
IDictionary<string, int> ageDict = people.ToDictionary(p => p.Name, p => p.Age); // IDictionary<string, int> interface for key-value, flexible implementation

// d) ILookup for groupings (best for multi-values per key)
ILookup<int, Person> groupedByAge = people.ToLookup(p => p.Age); // ILookup<int, Person> for one-to-many groupings by age

// Other option: IGrouping from GroupBy (single group example)
IEnumerable<IGrouping<string, Person>> groupedByCity = people.GroupBy(p => p.City); // IGrouping<string, Person> for individual groups

// Other option: IReadOnlyDictionary for read-only
IReadOnlyDictionary<string, Person> readOnlyDict = new Dictionary<string, Person>(people.ToDictionary(p => p.Name)); // IReadOnlyDictionary<string, Person> to prevent modifications

Console.WriteLine("End");













///

int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };
var evenNumQuery = from num in numbers
                   where (num % 2) == 0
                   select num;
List<int> evenNums = evenNumQuery.ToList();
evenNums.Add(8);
Debug.WriteLine(evenNums[2]);

var customers = new List<Customer> { new Customer { Id = 1, Name = "Alice", City = "London" }, new Customer { Id = 2, Name = "Bob", City = "Paris" }, new Customer { Id = 3, Name = "Claire", City = "London" }, new Customer { Id = 4, Name = "David", City = "Paris" } };
var customerQuery = from c in customers
                                   select c;
Dictionary<int, Customer> customerDict = customerQuery.ToDictionary(c => c.Id); 
Customer alice = customerDict[1]; 

ILookup<string, Customer> customerLookup = customers.ToLookup(c => c.City);
foreach (var customer in customerLookup["London"])
{
    Debug.WriteLine(customer.Name);  
}

List<int> fibNum = [1, 1];
for (int i = 1; i < 100; i++) {
    int count  = fibNum.Count();
    fibNum.Add(fibNum[count-1]+ fibNum[count-2]);
};
List<string> names9 = ["<name>", "Ana", "Felipe"];
names9.Add("Bill");
names9.Remove("Ana");
Debug.WriteLine($" index  {names9.IndexOf("Pradeep")} and index is {names9.IndexOf("Felipe")}");
names9.Sort();
foreach (var name in names9)
{
    Debug.WriteLine($"{name}");
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}
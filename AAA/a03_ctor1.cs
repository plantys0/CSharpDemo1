using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{
    public class a03_ctor1
    {

// Book class with primary ctor (params auto-fields)
class Book(string title, string author, decimal price, int stock, string genre)
{
    // Variations (wrap fields or add logic)
    public string Title { get; } = title;  // Read-only: Can't change after create
    public string Author { get; init; } = author;  // Init-only: Set at create only
    public decimal Price { get; set; } = price;  // Full read/write: Change anytime
    public int Stock { get; private set; } = stock;  // Private set: Change inside class only
    public string Genre { get; protected set; } = genre;  // Protected set: Change in subclass only

    // Alternative for InternalCode: Write-only at init (no get)
    private string _internalCode = "CODE-" + title;  // Private field
    public string InternalCode { init { _internalCode = value; } }  // Init setter only

    public string Notes { private get; set; } = "Default note";  // Private get, public set: Read inside only

    // Custom with logic (full form)
    private double _rating;
    public double Rating
    {
        get { return _rating; }
        set
        {
            if (value < 0 || value > 5) throw new ArgumentException("Rating 0-5!");
            _rating = value;
        }
    }

    // Method to update stock (demo private set)
    public void ReduceStock(int amount) { Stock -= amount; }

    // Print info
    public void PrintInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Price: {Price}, Stock: {Stock}, Genre: {Genre}, Rating: {Rating}");
        // Internal read demos (private access)
        Console.WriteLine($"Internal: Notes={Notes}, Code={_internalCode}");
    }
}

    static void Main()
    {
        // Create book with init overrides
        Book book = new("C# Guide", "Doe", 29.99m, 100, "Tech")
        {
            Author = "Jane",  // OK: Init-only
            InternalCode = "NEW-CODE",  // OK: Init setter
            Notes = "Public set note"  // OK: Public set
        };
        book.Rating = 4.5;  // OK: Custom set
        book.PrintInfo();  // Shows all

        // Demo: Full read/write (Price)
        Console.WriteLine("Old Price: " + book.Price);  // Read OK
        book.Price = 19.99m;  // Set OK
        Console.WriteLine("New Price: " + book.Price);

        // Demo: Read-only (Title)
        Console.WriteLine("Title: " + book.Title);  // Read OK
        // book.Title = "New";  // Error: Read-only

        // Demo: Init-only (Author)
        Console.WriteLine("Author: " + book.Author);  // Read OK
        // book.Author = "New";  // Error: Init-only (not at create)

        // Demo: Private set (Stock)
        Console.WriteLine("Old Stock: " + book.Stock);  // Read OK
        book.ReduceStock(10);  // Set OK (inside class)
        Console.WriteLine("New Stock: " + book.Stock);
        // book.Stock = 50;  // Error: Private set

        // Demo: Protected set (Genre)
        Console.WriteLine("Genre: " + book.Genre);  // Read OK
        // book.Genre = "New";  // Error: Protected (subclass only)

        // Demo: Write-only init (InternalCode)
        // Console.WriteLine(book.InternalCode);  // Error: No get
        // But internal read in PrintInfo shows it set

        // Demo: Private get, public set (Notes)
        book.Notes = "New note";  // Set OK (public)
        // Console.WriteLine(book.Notes);  // Error: Private get
        // But PrintInfo reads internally

        // Demo: Custom validation (Rating)
        // book.Rating = 6;  // Throws: 0-5 only!
    }




    }
}

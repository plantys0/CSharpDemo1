using System;

// SOLID Principles - Condensed Explanation in C#
// All explanations are provided as comments.
// Code examples converted from TypeScript to C#.
// For each principle: brief explanation, non-SOLID code, SOLID code.

// Single Responsibility Principle (SRP)
// Explanation: A class should have only one reason to change, meaning it should be impacted by only one source or reason for modification.
// Non-SOLID Example: One class handling seat check, price calculation, payment, and notification.
public class TicketBooking
{
    private int movieId;
    private int userId;
    private string seatNumber;

    public TicketBooking(int movieId, int userId, string seatNumber)
    {
        this.movieId = movieId;
        this.userId = userId;
        this.seatNumber = seatNumber;
    }

    public bool CheckSeatAvailability()
    {
        // Logic to check seat in DB
        return true;
    }

    public int CalculatePrice()
    {
        // Price logic based on seat type, timing
        return 300;
    }

    public bool MakePayment()
    {
        // Call payment gateway API
        return true;
    }

    public void SendConfirmation()
    {
        // Send SMS or Email
    }

    public void Book()
    {
        if (CheckSeatAvailability())
        {
            int price = CalculatePrice();
            bool paid = MakePayment();
            if (paid)
            {
                SendConfirmation();
                Console.WriteLine("Booking successful!");
            }
        }
    }
}

// SOLID Example: Separate classes for each responsibility, injected into TicketBooking.
public class SeatChecker
{
    public bool IsSeatAvailable(int movieId, string seatNumber)
    {
        // Check in DB if seat is free
        return true;
    }
}

public class PriceCalculator
{
    public int GetPrice(int movieId, string seatNumber)
    {
        // Logic based on seat type, timing, etc.
        return 300;
    }
}

public class PaymentService
{
    public bool Pay(int userId, int amount)
    {
        // Payment gateway integration
        Console.WriteLine($"Processing payment of ₹{amount} for user {userId}");
        return true;
    }
}

public class Notifier
{
    public void SendConfirmation(int userId, int movieId)
    {
        // Send email/SMS
        Console.WriteLine($"Confirmation sent to user {userId} for movie {movieId}");
    }
}

public class TicketBookingSolid
{
    private SeatChecker seatChecker;
    private PriceCalculator priceCalculator;
    private PaymentService paymentService;
    private Notifier notifier;

    public TicketBookingSolid(SeatChecker seatChecker, PriceCalculator priceCalculator, PaymentService paymentService, Notifier notifier)
    {
        this.seatChecker = seatChecker;
        this.priceCalculator = priceCalculator;
        this.paymentService = paymentService;
        this.notifier = notifier;
    }

    public void BookTicket(int userId, int movieId, string seatNumber)
    {
        if (seatChecker.IsSeatAvailable(movieId, seatNumber))
        {
            int price = priceCalculator.GetPrice(movieId, seatNumber);
            bool paid = paymentService.Pay(userId, price);
            if (paid)
            {
                notifier.SendConfirmation(userId, movieId);
                Console.WriteLine("Booking successful!");
            }
            else
            {
                Console.WriteLine("Payment failed!");
            }
        }
        else
        {
            Console.WriteLine("Seat not available!");
        }
    }
}

// Open/Closed Principle (OCP)
// Explanation: Software entities should be open for extension (adding new functionality) but closed for modification (avoiding changes to existing code).
// Non-SOLID Example: Adding new payment methods requires modifying the class with new if-else branches.
public class PaymentProcessor
{
    public void Process(string paymentMethod)
    {
        if (paymentMethod == "credit_card")
        {
            Console.WriteLine("Processing credit card payment...");
        }
        else if (paymentMethod == "paypal")
        {
            Console.WriteLine("Processing PayPal payment...");
        }
        else if (paymentMethod == "upi")
        {
            Console.WriteLine("Processing UPI payment...");
        }
        else
        {
            throw new Exception("Unsupported payment method");
        }
    }
}

// SOLID Example: Use strategy pattern with interface; new methods added by new classes implementing the interface, no change to processor.
public interface IPaymentStrategy
{
    void Pay();
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay()
    {
        Console.WriteLine("Credit card payment processed");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay()
    {
        Console.WriteLine("PayPal payment processed");
    }
}

public class UPIPayment : IPaymentStrategy
{
    public void Pay()
    {
        Console.WriteLine("UPI payment processed");
    }
}

public class PaymentProcessorSolid
{
    private IPaymentStrategy strategy;

    public PaymentProcessorSolid(IPaymentStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void Process()
    {
        strategy.Pay();
    }
}

// Liskov Substitution Principle (LSP)
// Explanation: Objects of a subtype should be replaceable with objects of the parent type without breaking the program, ensuring child classes maintain expected behavior.
// Non-SOLID Example: Square extending Rectangle leads to unexpected behavior when setting width/height independently.
public class Rectangle
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void SetWidth(int w)
    {
        Width = w;
    }

    public void SetHeight(int h)
    {
        Height = h;
    }

    public int GetArea()
    {
        return Width * Height;
    }
}

public class Square : Rectangle
{
    public Square(int side) : base(side, side) { }

    public new void SetWidth(int w)
    {
        Width = w;
        Height = w;
    }

    public new void SetHeight(int h)
    {
        Width = h;
        Height = h;
    }
}

/* Example usage that breaks:
public static void PrintArea(Rectangle rect)
{
    rect.SetWidth(5);
    rect.SetHeight(10);
    Console.WriteLine(rect.GetArea()); // Expected 50
}
*/
// Works for Rectangle, but for Square: sets to 10x10, area 100 instead of 50.

// SOLID Example: Separate interfaces/implementations without inheritance that violates behavior.
public interface IShape
{
    int GetArea();
}

public class RectangleSolid : IShape
{
    public int Width { get; }
    public int Height { get; }

    public RectangleSolid(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int GetArea()
    {
        return Width * Height;
    }
}

public class SquareSolid : IShape
{
    public int Side { get; }

    public SquareSolid(int side)
    {
        Side = side;
    }

    public int GetArea()
    {
        return Side * Side;
    }
}

// Usage:
public static void PrintArea(IShape shape)
{
    Console.WriteLine(shape.GetArea());
}

// Interface Segregation Principle (ISP)
// Explanation: Clients should not be forced to depend on interfaces they do not use. Break large interfaces into smaller, focused ones.
// Non-SOLID Example: Classes forced to implement unused methods, throwing exceptions.
public interface IRemoteControl
{
    void TurnOn();
    void TurnOff();
    void Record();
}

// Example classes throwing for unused:
public class CashOnDelivery : IPaymentGateway  // Assuming IPaymentGateway is the fat interface
{
    public void Pay()
    {
        Console.WriteLine("COD payment");
    }

    public void Refund()
    {
        throw new Exception("Refund not supported for COD");
    }

    public void Schedule()
    {
        throw new Exception("Scheduling not supported");
    }
}

// Similar for UserActivityReport and LogFileReader.

// Note: The article uses different examples; here condensed to one, but principle is the same.

// SOLID Example: Split interfaces into specific ones.
public interface IPayable
{
    void Pay();
}

public interface IRefundable
{
    void Refund();
}

public interface ISchedulable
{
    void Schedule();
}

public class CashOnDeliverySolid : IPayable
{
    public void Pay()
    {
        Console.WriteLine("COD payment");
    }
}

public class StripePayment : IPayable, IRefundable, ISchedulable
{
    public void Pay() { /* impl */ }
    public void Refund() { /* impl */ }
    public void Schedule() { /* impl */ }
}

// Similar for report generators:
public interface IPDFGeneratable
{
    void GeneratePDF();
}

public interface IExcelGeneratable
{
    void GenerateExcel();
}

public interface ICSVGeneratable
{
    void GenerateCSV();
}

public class UserActivityReportSolid : ICSVGeneratable
{
    public void GenerateCSV()
    {
        Console.WriteLine("Generating user CSV");
    }
}

// And for files:
public interface IFileOpener
{
    void Open();
    void Close();
}

public interface IFileReader : IFileOpener
{
    string Read();
}

public interface IFileWriter : IFileOpener
{
    void Write(string data);
}

// Dependency Inversion Principle (DIP)
// Explanation: High-level modules should not depend on low-level modules; both should depend on abstractions. Abstractions should not depend on details.
// Non-SOLID Example: Direct dependencies on concrete classes, tight coupling.
public class TicketRepository
{
    public string BookTicket(string userId, string movieId)
    {
        Console.WriteLine($"Saving booking in DB for user {userId} and movie {movieId}");
        return "booking-id-123";
    }
}

public class BookTicketService
{
    private TicketRepository repo = new TicketRepository(); // tightly coupled

    public string Execute(string userId, string movieId)
    {
        // business logic
        return repo.BookTicket(userId, movieId);
    }
}

public class BookTicketController
{
    private BookTicketService service = new BookTicketService(); // tightly coupled

    public object HandleRequest(object req) // Simplified
    {
        // Extract userId, movieId from req
        string userId = "u1";
        string movieId = "m101";
        string bookingId = service.Execute(userId, movieId);
        return new { Success = true, BookingId = bookingId };
    }
}

// SOLID Example: Depend on interfaces, inject dependencies.
public interface ITicketRepository
{
    string BookTicket(string userId, string movieId);
}

public class TicketRepositorySolid : ITicketRepository
{
    public string BookTicket(string userId, string movieId)
    {
        Console.WriteLine($"Saving booking in DB for user {userId} and movie {movieId}");
        return "booking-id-123";
    }
}

public class BookTicketServiceSolid
{
    private ITicketRepository repo;

    public BookTicketServiceSolid(ITicketRepository repo)
    {
        this.repo = repo;
    }

    public string Execute(string userId, string movieId)
    {
        // business logic
        return repo.BookTicket(userId, movieId);
    }
}

public class BookTicketControllerSolid
{
    private BookTicketServiceSolid service;

    public BookTicketControllerSolid(BookTicketServiceSolid service)
    {
        this.service = service;
    }

    public object HandleRequest(object req) // Simplified
    {
        string userId = "u1";
        string movieId = "m101";
        string bookingId = service.Execute(userId, movieId);
        return new { Success = true, BookingId = bookingId };
    }
}

// Usage example:
public class Program
{
    public static void Main()
    {
        ITicketRepository repo = new TicketRepositorySolid();
        BookTicketServiceSolid service = new BookTicketServiceSolid(repo);
        BookTicketControllerSolid controller = new BookTicketControllerSolid(service);
        object response = controller.HandleRequest(null); // fake req
        Console.WriteLine(response); // Would print object, but in practice.
    }
}
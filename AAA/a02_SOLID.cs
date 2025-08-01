

namespace AAA
{
    public class a02_SOLID
    {
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
                Console.WriteLine($"Checking availability for seat {seatNumber} in movie {movieId}");
                return true;
            }

            public int CalculatePrice()
            {
                // Price logic based on seat type, timing
                Console.WriteLine($"Calculating price for seat {seatNumber}");
                return 300;
            }

            public bool MakePayment()
            {
                // Call payment gateway API
                Console.WriteLine($"Making payment of 300 for user {userId}");
                return true;
            }

            public void SendConfirmation()
            {
                // Send SMS or Email
                Console.WriteLine($"Sending confirmation to user {userId} for movie {movieId}");
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
        // To extend: Add new classes for different checkers, calculators, etc., without changing TicketBookingSolid.
        public class SeatChecker
        {
            public bool IsSeatAvailable(int movieId, string seatNumber)
            {
                // Check in DB if seat is free
                Console.WriteLine($"Checking availability for seat {seatNumber} in movie {movieId}");
                return true;
            }
        }

        public class PriceCalculator
        {
            public int GetPrice(int movieId, string seatNumber)
            {
                // Logic based on seat type, timing, etc.
                Console.WriteLine($"Calculating price for seat {seatNumber}");
                return 300;
            }
        }

        // Interface for payment services to allow swapping implementations
        public interface IPaymentService
        {
            bool Pay(int userId, int amount);
        }

        // Initial payment service: Credit Card
        public class CreditCardPaymentService : IPaymentService
        {
            public bool Pay(int userId, int amount)
            {
                Console.WriteLine($"Processing credit card payment of {amount} for user {userId}");
                return true;
            }
        }

        // Added later: Debit Card payment service
        public class DebitCardPaymentService : IPaymentService
        {
            public bool Pay(int userId, int amount)
            {
                Console.WriteLine($"Processing debit card payment of {amount} for user {userId}");
                return true;
            }
        }

        // Interface for notification services to allow swapping implementations
        public interface INotifier
        {
            void SendConfirmation(int userId, int movieId);
        }

        // Initial notification service: Email
        public class EmailNotificationService : INotifier
        {
            public void SendConfirmation(int userId, int movieId)
            {
                Console.WriteLine($"Sending email confirmation to user {userId} for movie {movieId}");
            }
        }

        // Added later: SMS notification service
        public class SMSNotificationService : INotifier
        {
            public void SendConfirmation(int userId, int movieId)
            {
                Console.WriteLine($"Sending SMS confirmation to user {userId} for movie {movieId}");
            }
        }

        public class TicketBookingSolid
        {
            private SeatChecker seatChecker;
            private PriceCalculator priceCalculator;
            private IPaymentService paymentService; // Uses interface for easy swapping
            private INotifier notifier; // Uses interface for easy swapping

            public TicketBookingSolid(SeatChecker seatChecker, PriceCalculator priceCalculator, IPaymentService paymentService, INotifier notifier)
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
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        // SOLID Example: Use strategy pattern with interface; new methods added by new classes implementing the interface, no change to processor.
        // To extend: Create a new class implementing IPaymentStrategy, e.g., BitcoinPayment, and pass it to PaymentProcessorSolid.
        public interface IPaymentStrategy
        {
            void Pay();
        }

        public class CreditCardPayment : IPaymentStrategy
        {
            public void Pay()
            {
                Console.WriteLine("Processing credit card payment...");
            }
        }

        public class PayPalPayment : IPaymentStrategy
        {
            public void Pay()
            {
                Console.WriteLine("Processing PayPal payment...");
            }
        }

        public class UPIPayment : IPaymentStrategy
        {
            public void Pay()
            {
                Console.WriteLine("Processing UPI payment...");
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
                try
                {
                    strategy.Pay();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
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

        // Example usage that breaks (demo in Main):
        public static void PrintArea(Rectangle rect)
        {
            rect.SetWidth(5);
            rect.SetHeight(10);
            Console.WriteLine($"Area: {rect.GetArea()}"); // Expected 50 for Rectangle, but 100 for Square
        }

        // SOLID Example: Separate interfaces/implementations without inheritance that violates behavior.
        // To extend: Add new shapes implementing IShape, without affecting existing ones.
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
        public static void PrintAreaSolid(IShape shape)
        {
            Console.WriteLine($"Area: {shape.GetArea()}");
        }

        // Interface Segregation Principle (ISP)
        // Explanation: Clients should not be forced to depend on interfaces they do not use. Break large interfaces into smaller, focused ones.
        // Non-SOLID Example: Classes forced to implement unused methods, throwing exceptions.
        public interface IPaymentGateway
        {
            void Pay();
            void Refund();
            void Schedule();
        }

        // Example class throwing for unused:
        public class CashOnDelivery : IPaymentGateway
        {
            public void Pay()
            {
                Console.WriteLine("COD payment processed");
            }

            public void Refund()
            {
                throw new Exception("Refund not supported for COD");
            }

            public void Schedule()
            {
                throw new Exception("Scheduling not supported for COD");
            }
        }

        // SOLID Example: Split interfaces into specific ones.
        // To extend: Classes implement only needed interfaces, add new interfaces if new behaviors needed.
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
                Console.WriteLine("COD payment processed");
            }
        }

        public class StripePayment : IPayable, IRefundable, ISchedulable
        {
            public void Pay()
            {
                Console.WriteLine("Stripe payment processed");
            }

            public void Refund()
            {
                Console.WriteLine("Stripe refund processed");
            }

            public void Schedule()
            {
                Console.WriteLine("Stripe schedule processed");
            }
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
                Console.WriteLine($"Booking ID: {bookingId}");
                return new { Success = true, BookingId = bookingId };
            }
        }

        // SOLID Example: Depend on interfaces, inject dependencies.
        // To extend: Implement new repositories, services, etc., and inject them without changing high-level classes.
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
                Console.WriteLine($"Booking ID: {bookingId}");
                return new { Success = true, BookingId = bookingId };
            }
        }

        static void Main(string[] args)
        {
            // SRP Demo
            Console.WriteLine("--- SRP Non-SOLID ---");
            TicketBooking booking = new TicketBooking(101, 1, "A1");
            booking.Book();

            Console.WriteLine("--- SRP SOLID ---");
            SeatChecker seatChecker = new SeatChecker();
            PriceCalculator priceCalculator = new PriceCalculator();
            // Use initial services: CreditCard and Email
            IPaymentService paymentService = new CreditCardPaymentService();
            INotifier notifier = new EmailNotificationService();
            // To swap: Change to DebitCardPaymentService and SMSNotificationService
            // e.g., IPaymentService paymentService = new DebitCardPaymentService();
            // INotifier notifier = new SMSNotificationService();
            TicketBookingSolid bookingSolid = new TicketBookingSolid(seatChecker, priceCalculator, paymentService, notifier);
            bookingSolid.BookTicket(1, 101, "A1");

            // OCP Demo
            Console.WriteLine("--- OCP Non-SOLID ---");
            PaymentProcessor processor = new PaymentProcessor();
            processor.Process("credit_card");
            processor.Process("paypal");
            processor.Process("upi");
            processor.Process("bitcoin"); // Will throw error

            Console.WriteLine("--- OCP SOLID ---");
            PaymentProcessorSolid processorCC = new PaymentProcessorSolid(new CreditCardPayment());
            processorCC.Process();
            PaymentProcessorSolid processorPP = new PaymentProcessorSolid(new PayPalPayment());
            processorPP.Process();
            PaymentProcessorSolid processorUPI = new PaymentProcessorSolid(new UPIPayment());
            processorUPI.Process();
            // For new method, e.g., Bitcoin, just create new strategy class and use: new PaymentProcessorSolid(new BitcoinPayment()).Process();

            // LSP Demo
            Console.WriteLine("--- LSP Non-SOLID ---");
            Rectangle rect = new Rectangle(0, 0);
            PrintArea(rect); // Area: 50
            Square sq = new Square(0);
            PrintArea(sq); // Area: 100 (unexpected)

            Console.WriteLine("--- LSP SOLID ---");
            IShape rectSolid = new RectangleSolid(5, 10);
            PrintAreaSolid(rectSolid); // Area: 50
            IShape sqSolid = new SquareSolid(5);
            PrintAreaSolid(sqSolid); // Area: 25 (but note: we can't set width/height independently on square)

            // ISP Demo
            Console.WriteLine("--- ISP Non-SOLID ---");
            try
            {
                IPaymentGateway cod = new CashOnDelivery();
                cod.Pay();
                cod.Refund(); // Throws
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("--- ISP SOLID ---");
            IPayable codSolid = new CashOnDeliverySolid();
            codSolid.Pay(); // No throw
            // Can't call Refund() on IPayable, so no force to implement unused

            StripePayment stripe = new StripePayment();
            stripe.Pay();
            stripe.Refund();
            stripe.Schedule();

            // DIP Demo
            Console.WriteLine("--- DIP Non-SOLID ---");
            BookTicketController controller = new BookTicketController();
            controller.HandleRequest(null);

            Console.WriteLine("--- DIP SOLID ---");
            ITicketRepository repo = new TicketRepositorySolid();
            BookTicketServiceSolid service = new BookTicketServiceSolid(repo);
            BookTicketControllerSolid controllerSolid = new BookTicketControllerSolid(service);
            controllerSolid.HandleRequest(null);
        }
    }
}
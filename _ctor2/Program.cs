/*Design a .NET 8 console application that models a manufacturing finance entity with comprehensive class features.
Implement a ComprehensiveFinanceManufacturingExample class that demonstrates:
Various property patterns(auto - implemented, private setter, init - only, required, nullable, validation attributes).
Data validation through attributes and custom setter logic (e.g., non-negative balances, future date checks).
Constructor overloads: primary constructor with parameter checks and a copy constructor.
Management of required and init-only members to satisfy C# 11 rules.
Include advanced class mechanisms :
Static and readonly members for company metadata.
Events and delegates for notifications on stock - price changes and transactions.
A backing - field indexer for arbitrary metadata storage.
Operator overloading(+) to apply transactions inline.
Interface and abstract -base -class implementation to support risk calculation and standardized operations.
Record usage for immutable product info.
Demonstrate inheritance with an AdvancedManufacturingExample subclass that extends reporting and adds specialized analysis.
Provide a Program.Main that:
Instantiates and initializes objects (including required LicenseNumber and CompanyCode).
Subscribes to events and exercises methods (risk updates, transactions, operator overload).
Validates object state and generates formatted reports to the console.
Ensure error handling and disposal patterns are correctly implemented for robust, maintainable code.*/

namespace _ctor1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Simple interface to define finance-related operations
    public interface IFinancialEntity
    {
        decimal GetTotalValue();              // Compute total value of assets
        void ProcessTransaction(decimal amount); // Apply a transaction
    }

    // Abstract class for shared risk logic and customization points
    public abstract class BaseFinancialEntity
    {
        public abstract decimal CalculateRisk();    // Must be implemented by subclasses
        public virtual string GetEntityType()       // Can be overridden, provides a default
            => "Base Financial Entity";
        protected virtual void OnEntityChanged()    // Hook method for derived classes //@ what does this mean
        { }
    }

    // Immutable record to hold read-only product metadata
    public record ProductInfo(string Name, string Category, DateTime CreatedDate); //@ why is this declared outside the main class

    // Main class demonstrating many C# features
    public class ComprehensiveFinanceManufacturingExample
        : BaseFinancialEntity, IFinancialEntity, IDisposable  //@ why IDisposable is needed
    {
        // Static constructor runs once before any other member is used
        static ComprehensiveFinanceManufacturingExample()   //@ why is this ctor needed. Is this public static or private static
        {
            CompanyEstablishedYear = 1995;
        }

        // Static readonly field set only in static constructor
        public static readonly int CompanyEstablishedYear; //@why not use {get;} instead

        // Compile-time constant for company name
        public const string CompanyName = "Advanced Manufacturing Corp";  //@ why const is used

        // Event is the C# way to notify subscribers of a change
        public event EventHandler<decimal>? StockPriceChanged; //@ why has subscribed to this. how is this used

        // Delegate type defines the signature for transaction callbacks
        public delegate void TransactionDelegate(decimal amount, string description);  //@explain in detail with example how delegate works

        // Field to hold custom transaction callbacks
        public TransactionDelegate? OnTransaction;

        #region Property Variations

        // Simple auto-implemented property, public get & set
        [Range(0, double.MaxValue, ErrorMessage = "Stock price must be non-negative")]
        public decimal StockPrice { get; set; }

        // Private auto-property only settable inside this class
        [Required(ErrorMessage = "Product serial number is required")]
        private int ProductSerialNumber { get; set; }

        // RiskFactor is private; expose updates via a method for validation
        [Range(0, 1, ErrorMessage = "Risk factor must be between 0 and 1")]
        private decimal RiskFactor { get; set; }

        // Public setter but private getter hides value externally
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string ProductionStatus { private get; set; } = "Pending"; //@what is private get

        // Read-only init-only property: set only in constructor
        public double FixedInterestRate { get; }  //@why not private

        // Private read-only property for internal capacity limit
        private int MaxProductionCapacity { get; } //@why not public

        // Backing field plus custom logic on set
        private decimal _accountBalance;
        public decimal AccountBalance
        {
            get => _accountBalance;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Balance cannot be negative");
                _accountBalance = value;
                OnEntityChanged();        // Notify derived classes//@how are the derived classes notified
            }
        }

        // Read-only field exposed via getter
        private readonly int _producedItemsCount;
        public int ProducedItemsCount => _producedItemsCount;

        // Init-only property must be set in object initializer
        [Required]
        public string CompanyCode { get; init; }

        // Required property enforces assignment in initializer
        public required string LicenseNumber { get; init; }

        // Nullable reference type to allow optional notes
        [StringLength(100)]
        public string? AdditionalNotes { get; set; }//@what happens if stringlength us >100

        // Full property with validation in setter
        private ProductInfo? _productInfo;
        public ProductInfo? ProductInfo
        {
            get => _productInfo;
            set
            {
                if (value != null && value.CreatedDate > DateTime.Now)
                    throw new ArgumentException("CreatedDate cannot be in the future");
                _productInfo = value;
            }
        }

        // Static counter to track how many instances created
        public static int TotalInstancesCreated { get; private set; } //@how does this work

        // Virtual property: can override in subclasses
        public virtual string EntityDescription
            => $"Manufacturing Entity - {CompanyCode}";

        // Protected backing store for metadata; accessible in subclasses
        protected readonly Dictionary<string, object> _metadata = new();

        // Indexer syntax allows object["key"] access
        public object? this[string key]
        {
            get => _metadata.TryGetValue(key, out var v) ? v : null;
            set
            {
                if (value != null)
                    _metadata[key] = value;
                else
                    _metadata.Remove(key);
            }
        }
        #endregion

        #region Constructors

        // Primary constructor initializes read-only and required data
        public ComprehensiveFinanceManufacturingExample(
            int productSerialNumber,
            double fixedInterestRate,
            int maxProductionCapacity,
            decimal accountBalance,
            int producedItemsCount)
        {
            // Parameter validation for safety
            if (productSerialNumber <= 0)
                throw new ArgumentException("Serial must be positive", nameof(productSerialNumber));
            if (fixedInterestRate < 0)
                throw new ArgumentException("Rate cannot be negative", nameof(fixedInterestRate));
            if (maxProductionCapacity <= 0)
                throw new ArgumentException("Capacity must be positive", nameof(maxProductionCapacity));

            ProductSerialNumber = productSerialNumber;
            FixedInterestRate = fixedInterestRate;
            MaxProductionCapacity = maxProductionCapacity;
            AccountBalance = accountBalance;       // Uses setter validation
            _producedItemsCount = producedItemsCount;

            TotalInstancesCreated++;               // Track instance creation
            Console.WriteLine($"Instance #{TotalInstancesCreated} created");
        }

        // Copy constructor reuses primary ctor and copies additional fields
        public ComprehensiveFinanceManufacturingExample(ComprehensiveFinanceManufacturingExample other)
            : this(
                other.ProductSerialNumber,
                other.FixedInterestRate,
                other.MaxProductionCapacity,
                other.AccountBalance,
                other.ProducedItemsCount)
        {
            // Copy optional or public properties
            StockPrice = other.StockPrice;
            RiskFactor = other.RiskFactor;
            ProductionStatus = other.ProductionStatus;
            CompanyCode = other.CompanyCode;
            LicenseNumber = other.LicenseNumber;
            AdditionalNotes = other.AdditionalNotes;
            ProductInfo = other.ProductInfo;
        } //@explain this code
        #endregion

        #region Methods

        // Public method to update private RiskFactor with validation
        public void UpdateRiskFactor(decimal riskFactor)
        {
            if (riskFactor < 0 || riskFactor > 1)
                throw new ArgumentOutOfRangeException(nameof(riskFactor));
            RiskFactor = riskFactor;
            OnEntityChanged();
        }

        // Expose the private ProductionStatus via a method
        public string GetProductionStatus() => ProductionStatus;

        // Internal transaction logic invokes delegate and raises event
        public void ProcessInternalTransaction(decimal amount, string description)
        {
            AccountBalance += amount;
            OnTransaction?.Invoke(amount, description);
            if (Math.Abs(amount) > 1000)
                StockPriceChanged?.Invoke(this, StockPrice);//@ explain this code line
        }

        // Abstract method implementation for risk calculation
        public override decimal CalculateRisk() =>
            RiskFactor * (AccountBalance / 10000m);

        // Override to include serial in type description
        public override string GetEntityType() =>
            $"Manufacturing Entity (Serial: {ProductSerialNumber})";

        // Interface implementation computes total value
        public decimal GetTotalValue() =>
            AccountBalance + (StockPrice * 100);

        // Interface implementation forwards to internal logic
        public void ProcessTransaction(decimal amount) =>
            ProcessInternalTransaction(amount, "External transaction");

        // Virtual report method prints finance summary
        public virtual void GenerateReport()  //@ why is this a virtual method
        {
            Console.WriteLine($"=== Financial Report for {CompanyName} ===");
            Console.WriteLine($"Entity: {EntityDescription}");
            Console.WriteLine($"License: {LicenseNumber}");
            Console.WriteLine($"Stock Price: {StockPrice:C}");
            Console.WriteLine($"Account Balance: {AccountBalance:C}");
            Console.WriteLine($"Total Value: {GetTotalValue():C}");
            Console.WriteLine($"Risk Factor: {RiskFactor:P}");
            Console.WriteLine($"Calculated Risk: {CalculateRisk():C}");
        }

        // Leverage DataAnnotations to validate all attributes at once
        public bool ValidateObject()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);//@explain this code
        }

        // Standard IDisposable pattern for cleanup //@why all the disposable code is needed
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _metadata.Clear();      // Free managed resources
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ComprehensiveFinanceManufacturingExample() => Dispose(false);  //@explain this code
        #endregion

        #region Operators
        // Operator overloading to add funds and return a new instance //@what is operator overloading
        public static ComprehensiveFinanceManufacturingExample operator +(
            ComprehensiveFinanceManufacturingExample left,
            decimal amount)
        {
            // Use copy ctor then init-only initializer for required props
            var result = new ComprehensiveFinanceManufacturingExample(left)  //@explain this code
            {
                LicenseNumber = left.LicenseNumber,
                CompanyCode = left.CompanyCode
            };
            result.ProcessInternalTransaction(amount, "Operator addition");
            return result;
        }

        // Equality overrides so instances compare by serial
        public override bool Equals(object? obj) =>
            obj is ComprehensiveFinanceManufacturingExample other &&
            other.ProductSerialNumber == ProductSerialNumber;
        public override int GetHashCode() =>
            ProductSerialNumber.GetHashCode();
        public static bool operator ==(
            ComprehensiveFinanceManufacturingExample? left,
            ComprehensiveFinanceManufacturingExample? right) =>
            EqualityComparer<ComprehensiveFinanceManufacturingExample>.Default.Equals(left, right);
        public static bool operator !=(
            ComprehensiveFinanceManufacturingExample? left,
            ComprehensiveFinanceManufacturingExample? right) =>
            !(left == right);
        #endregion
    }

    // Subclass shows how to extend and override behavior
    public class AdvancedManufacturingExample
        : ComprehensiveFinanceManufacturingExample
    {
        public AdvancedManufacturingExample(
            int serialNumber,
            double interestRate,
            int capacity,
            decimal balance,
            int itemCount)
            : base(serialNumber, interestRate, capacity, balance, itemCount)
        { }

        public override string EntityDescription =>
            $"Advanced {base.EntityDescription}";

        public override void GenerateReport()
        {
            base.GenerateReport();  // Reuse base report
            Console.WriteLine($"=== Advanced Features - Metadata entries: {_metadata.Count} ===");
        }

        public void PerformAdvancedAnalysis() =>
            Console.WriteLine("Performing advanced financial analysis...");
    }

    // Console program to demonstrate usage
    public class Program
    {
        public static void Main()
        {
            try
            {
                // Object initializer sets all required and init-only props
                var example = new ComprehensiveFinanceManufacturingExample(
                    productSerialNumber: 12345,
                    fixedInterestRate: 0.025,
                    maxProductionCapacity: 1000,
                    accountBalance: 2000m,
                    producedItemsCount: 500)
                {
                    CompanyCode = "AMC",
                    LicenseNumber = "MFG-2024-001",
                    StockPrice = 150.5m,
                    ProductionStatus = "In Progress",
                    AdditionalNotes = "Premium manufacturing line",
                    ProductInfo = new ProductInfo("Widget Pro", "Electronics", DateTime.Now.AddDays(-30))
                };

                // Subscribe to events using lambda syntax
                example.StockPriceChanged += (s, price) =>
                    Console.WriteLine($"Stock price alert: {price:C}");
                example.OnTransaction += (amt, desc) =>
                    Console.WriteLine($"Transaction: {amt:C} - {desc}");

                // Use indexer to tag arbitrary metadata
                example["Location"] = "Factory A";
                Console.WriteLine($"Location: {example["Location"]}");

                // Use methods and operators
                example.UpdateRiskFactor(0.25m);
                example.ProcessTransaction(1500m);
                Console.WriteLine($"Valid: {example.ValidateObject()}");
                example.GenerateReport();

                var modified = example + 500m;
                Console.WriteLine($"After + operator: {modified.GetTotalValue():C}");

                // Demonstrate subclass behavior
                var advanced = new AdvancedManufacturingExample(
                    serialNumber: 67890,
                    interestRate: 0.03,
                    capacity: 2000,
                    balance: 5000m,
                    itemCount: 1000)
                {
                    CompanyCode = "AMC-ADV",
                    LicenseNumber = "ADV-2024-001",
                    StockPrice = 200.5m,
                    ProductionStatus = "Complete"
                };
                advanced.GenerateReport();
                advanced.PerformAdvancedAnalysis();

                Console.WriteLine($"\nTotal instances: {ComprehensiveFinanceManufacturingExample.TotalInstancesCreated}");
                Console.WriteLine($"Company established: {ComprehensiveFinanceManufacturingExample.CompanyEstablishedYear}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

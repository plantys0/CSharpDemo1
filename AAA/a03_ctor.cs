


namespace AAA
{
    public class a03_ctor
    {

        // Simple record demonstrating implicit primary constructor for immutable data
        public record ProductInfo(string Name, string Category, DateTime CreatedDate);

        // Interface for financial operations (used in comprehensive class)
        public interface IFinancialEntity
        {
            decimal GetTotalValue();
            void ProcessTransaction(decimal amount);
        }

        // Abstract base class with constructor and virtual methods
        public abstract class BaseFinancialEntity
        {
            protected string EntityType { get; }

            // Parameterized constructor in abstract class
            protected BaseFinancialEntity(string entityType)
            {
                EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            }

            public abstract decimal CalculateRisk();
            public virtual string GetEntityType() => EntityType;
            protected virtual void OnEntityChanged() { }
        }

        // Simple class without constructor (default parameterless)
        public class IntegerHolderNoCtor
        {
            public int Value { get; set; }
        }

        // Simple class with parameterized constructor
        public class IntegerHolder
        {
            public int Value { get; set; }

            // Parameterized constructor with validation
            public IntegerHolder(int value)
            {
                if (value < 0) throw new ArgumentException("Value must be non-negative");
                Value = value;
            }
        }

        // Class demonstrating private constructor for singleton pattern
        public class SingletonExample
        {
            private static readonly Lazy<SingletonExample> instance = new(() => new SingletonExample());

            public static SingletonExample Instance => instance.Value;

            public string Message { get; } = "Singleton instance created!";

            // Private constructor prevents external instantiation
            private SingletonExample() { }
        }

        // Class with constructor overloads and chaining
        public class OverloadExample
        {
            public string Name { get; }
            public int Age { get; }

            // Parameterless constructor
            public OverloadExample() : this("Default", 0) { }

            // Parameterized constructor chaining to another
            public OverloadExample(string name) : this(name, 0) { }

            // Base constructor with expression body
            public OverloadExample(string name, int age) => (Name, Age) = (name, age);
        }

        // Class with static constructor and factory method
        public class FactoryExample
        {
            private static int factoryCount;

            // Static constructor initializes static fields
            static FactoryExample()
            {
                factoryCount = 0;
                Console.WriteLine("Static constructor called once.");
            }

            public int Id { get; }

            // Private constructor used by factory
            private FactoryExample(int id) => Id = id;

            // Factory method instead of public constructor
            public static FactoryExample Create()
            {
                factoryCount++;
                return new FactoryExample(factoryCount);
            }

            public static int GetFactoryCount() => factoryCount;
        }

        // Comprehensive class combining all features
        public class ComprehensiveFinanceManufacturingExample : BaseFinancialEntity, IFinancialEntity, IDisposable
        {
            // Static constructor
            static ComprehensiveFinanceManufacturingExample()
            {
                CompanyEstablishedYear = 1995;
                Console.WriteLine("Static constructor for Comprehensive class called.");
            }

            // Static readonly and const fields
            public static readonly int CompanyEstablishedYear;
            public const string CompanyName = "Advanced Manufacturing Corp";

            // Event and delegate
            public event EventHandler<decimal>? StockPriceChanged;
            public delegate void TransactionDelegate(decimal amount, string description);
            public TransactionDelegate? OnTransaction;

            // Various properties
            [Range(0, double.MaxValue)]
            public decimal StockPrice { get; set; }

            [Required]
            private int ProductSerialNumber { get; set; }

            [Range(0, 1)]
            private decimal RiskFactor { get; set; }

            [StringLength(50)]
            public string ProductionStatus { private get; set; } = "Pending";

            public double FixedInterestRate { get; }

            private int MaxProductionCapacity { get; }

            private decimal _accountBalance;
            public decimal AccountBalance
            {
                get => _accountBalance;
                private set
                {
                    if (value < 0) throw new ArgumentException("Balance cannot be negative");
                    _accountBalance = value;
                    OnEntityChanged();
                }
            }

            private readonly int _producedItemsCount;
            public int ProducedItemsCount => _producedItemsCount;

            [Required]
            public string CompanyCode { get; init; }

            public required string LicenseNumber { get; init; }

            [StringLength(100)]
            public string? AdditionalNotes { get; set; }

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

            public static int TotalInstancesCreated { get; private set; }

            public virtual string EntityDescription => $"Manufacturing Entity - {CompanyCode}";

            protected readonly Dictionary<string, object> _metadata = new();

            public object? this[string key]
            {
                get => _metadata.TryGetValue(key, out var v) ? v : null;
                set => _metadata[value != null ? key : null!] = value!;
            }

            // Primary parameterized constructor with validation
            public ComprehensiveFinanceManufacturingExample(
                int productSerialNumber,
                double fixedInterestRate,
                int maxProductionCapacity,
                decimal accountBalance,
                int producedItemsCount)
                : base("Comprehensive Entity")
            {
                if (productSerialNumber <= 0) throw new ArgumentException("Serial must be positive");
                if (fixedInterestRate < 0) throw new ArgumentException("Rate cannot be negative");
                if (maxProductionCapacity <= 0) throw new ArgumentException("Capacity must be positive");

                ProductSerialNumber = productSerialNumber;
                FixedInterestRate = fixedInterestRate;
                MaxProductionCapacity = maxProductionCapacity;
                AccountBalance = accountBalance;
                _producedItemsCount = producedItemsCount;

                TotalInstancesCreated++;
                Console.WriteLine($"Comprehensive instance #{TotalInstancesCreated} created.");
            }

            // Copy constructor chaining to primary
            [SetsRequiredMembers]
            public ComprehensiveFinanceManufacturingExample(ComprehensiveFinanceManufacturingExample other)
                : this(other.ProductSerialNumber, other.FixedInterestRate, other.MaxProductionCapacity, other.AccountBalance, other.ProducedItemsCount)
            {
                StockPrice = other.StockPrice;
                RiskFactor = other.RiskFactor;
                ProductionStatus = other.ProductionStatus;
                CompanyCode = other.CompanyCode;
                LicenseNumber = other.LicenseNumber;
                AdditionalNotes = other.AdditionalNotes;
                ProductInfo = other.ProductInfo;
                Console.WriteLine("Copy constructor used.");
            }

            // Additional constructor with out parameter (rare, for demo)
            [SetsRequiredMembers]
            public ComprehensiveFinanceManufacturingExample(out string initMessage) : this(1, 0.01, 100, 1000m, 10)
            {
                initMessage = "Constructor with out parameter called.";
            }

            // Methods (abbreviated for brevity)
            public void UpdateRiskFactor(decimal riskFactor)
            {
                if (riskFactor < 0 || riskFactor > 1) throw new ArgumentOutOfRangeException();
                RiskFactor = riskFactor;
                OnEntityChanged();
            }

            public string GetProductionStatus() => ProductionStatus;

            public void ProcessInternalTransaction(decimal amount, string description)
            {
                AccountBalance += amount;
                OnTransaction?.Invoke(amount, description);
                if (Math.Abs(amount) > 1000) StockPriceChanged?.Invoke(this, StockPrice);
            }

            public override decimal CalculateRisk() => RiskFactor * (AccountBalance / 10000m);

            public decimal GetTotalValue() => AccountBalance + (StockPrice * 100);

            public void ProcessTransaction(decimal amount) => ProcessInternalTransaction(amount, "External transaction");

            public virtual void GenerateReport()
            {
                Console.WriteLine($"=== Report for {CompanyName} ===");
                Console.WriteLine($"Stock Price: {StockPrice:C} | Balance: {AccountBalance:C} | Risk: {CalculateRisk():C}");
            }

            public bool ValidateObject()
            {
                var context = new ValidationContext(this);
                var results = new List<ValidationResult>();
                return Validator.TryValidateObject(this, context, results, true);
            }

            // IDisposable pattern
            private bool _disposed;
            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing) _metadata.Clear();
                    _disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~ComprehensiveFinanceManufacturingExample() => Dispose(false);

            // Operator overload (uses copy ctor internally)
            public static ComprehensiveFinanceManufacturingExample operator +(ComprehensiveFinanceManufacturingExample left, decimal amount)
            {
                var result = new ComprehensiveFinanceManufacturingExample(left)
                {
                    CompanyCode = left.CompanyCode,
                    LicenseNumber = left.LicenseNumber
                };
                result.ProcessInternalTransaction(amount, "Operator addition");
                return result;
            }
        }

        // Subclass demonstrating inheritance and base constructor call
        public class AdvancedManufacturingExample : ComprehensiveFinanceManufacturingExample
        {
            // Constructor chaining to base
            public AdvancedManufacturingExample(int serialNumber, double interestRate, int capacity, decimal balance, int itemCount)
                : base(serialNumber, interestRate, capacity, balance, itemCount) { }

            public override void GenerateReport()
            {
                base.GenerateReport();
                Console.WriteLine("Advanced report: Additional analysis performed.");
            }
        }

        // Program class to test all features
            public static void Main()
            {
                Console.WriteLine("=== Demonstrating Constructors in C# .NET 8 ===");

                // Simple without ctor
                var noCtor = new IntegerHolderNoCtor { Value = 5 };
                Console.WriteLine($"No ctor: {noCtor.Value + 2}");

                // Simple with ctor
                var withCtor = new IntegerHolder(5);
                Console.WriteLine($"With ctor: {withCtor.Value + 2}");

                // Singleton with private ctor
                var singleton = SingletonExample.Instance;
                Console.WriteLine(singleton.Message);

                // Overloads and chaining
                var overload1 = new OverloadExample();
                var overload2 = new OverloadExample("John");
                var overload3 = new OverloadExample("Jane", 30);
                Console.WriteLine($"Overloads: {overload1.Name}/{overload1.Age}, {overload2.Name}/{overload2.Age}, {overload3.Name}/{overload3.Age}");

                // Factory with static ctor
                var factory1 = FactoryExample.Create();
                var factory2 = FactoryExample.Create();
                Console.WriteLine($"Factory IDs: {factory1.Id}, {factory2.Id} | Total: {FactoryExample.GetFactoryCount()}");

                // Comprehensive with primary ctor and initializer
                var comprehensive = new ComprehensiveFinanceManufacturingExample(12345, 0.025, 1000, 2000m, 500)
                {
                    CompanyCode = "AMC",
                    LicenseNumber = "MFG-001",
                    StockPrice = 150.5m,
                    ProductionStatus = "In Progress",
                    ProductInfo = new ProductInfo("Widget", "Electronics", DateTime.Now)
                };
                comprehensive.UpdateRiskFactor(0.25m);
                comprehensive.ProcessTransaction(1500m);
                comprehensive.GenerateReport();
                Console.WriteLine($"Validation: {comprehensive.ValidateObject()}");

                // Copy ctor
                var copy = new ComprehensiveFinanceManufacturingExample(comprehensive);
                copy.GenerateReport();

                // Ctor with out param
                var outCtor = new ComprehensiveFinanceManufacturingExample(out string message);
                Console.WriteLine(message);
                outCtor.GenerateReport();

                // Operator overload
                var modified = comprehensive + 500m;
                modified.GenerateReport();

                // Subclass
                var advanced = new AdvancedManufacturingExample(67890, 0.03, 2000, 5000m, 1000)
                {
                    CompanyCode = "ADV",
                    LicenseNumber = "ADV-001"
                };
                advanced.GenerateReport();

                // Dispose
                comprehensive.Dispose();

                Console.WriteLine($"\nTotal comprehensive instances: {ComprehensiveFinanceManufacturingExample.TotalInstancesCreated}");
                Console.WriteLine($"Company established: {ComprehensiveFinanceManufacturingExample.CompanyEstablishedYear}");
            }
    }
}

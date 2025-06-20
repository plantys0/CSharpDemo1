namespace _Interface1
{
    // Define a simple interface for tax calculation
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal amount);
    }

    // Implement the interface in a class and add extra properties and methods
    public class SalaryTaxCalculator : ITaxCalculator
    {
        // Additional property not in the interface
        public decimal TaxRate { get; set; }

        public SalaryTaxCalculator(decimal taxRate)
        {
            TaxRate = taxRate;
        }

        // Implement the method from the interface
        public decimal CalculateTax(decimal amount)
        {
            return amount * TaxRate;
        }

        // Additional method not in the interface
        public decimal CalculateAnnualTax(decimal monthlySalary)
        {
            return CalculateTax(monthlySalary * 12);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            decimal taxRate = 0.25m;
            decimal monthlySalary = 5000;

            // Directly creating an instance of SalaryTaxCalculator
            SalaryTaxCalculator taxCalculator = new SalaryTaxCalculator(taxRate);

            decimal monthlyTax = taxCalculator.CalculateTax(monthlySalary);
            Console.WriteLine($"Monthly salary tax: {monthlyTax}");

            // Accessing the additional method without casting
            decimal annualTax = taxCalculator.CalculateAnnualTax(monthlySalary);
            Console.WriteLine($"Annual salary tax: {annualTax}");
        }
    }

}
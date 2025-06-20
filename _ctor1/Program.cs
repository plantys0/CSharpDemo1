namespace _ctor1 {
    using System;
    /*
| # |       Name       | Access | Ext. Read | Int. Read | Ext. Modify | Int. Modify | Get | Set |
|---|------------------|--------|-----------|-----------|-------------|-------------|-----|-----|
| 1 |    StockPrice    | public |   Yes     |    Yes    |     Yes     |     Yes     | Yes | Yes |
| 2 | Prod. Serial #   |private |    No     |    Yes    |      No     |     Yes     | Yes | Yes |
| 3 |    RiskFactor    |private |    No     |    Yes    |      No     |     Yes     | Yes | Yes |
| 4 |   Prod. Status   | public |   Yes     |    Yes    |     Yes     |     Yes     | Yes | Yes |
| 5 |   FixedIntRate   | public |   Yes     |    Yes    |      No     |      No     | Yes | No  |
| 6 | MaxProdCapacity  |private |    No     |    Yes    |      No     |      No     | Yes | No  |
| 7 |  AccountBalance  |private |    No     |    Yes    |     Yes     |     Yes     | Yes | No  |
| 8 |Prod. Item Cnt.   |private readonly|Yes|    Yes    |      No     |     Yes     | Yes | No  |
     */
    public class FinanceManufacturingExample {
        public decimal StockPrice { get; set; }  //SetAnyTime
        private int ProductSerialNumber { get; set; } //Set@Instantiation
        private decimal RiskFactor { get; set; } //SetViaPublicMethod
        public string ProductionStatus { private get; set; }  //SetAnyTime_DisplayViaPublicMethod
        public double FixedInterestRate { get; }  //Set@Instantiation
        private int MaxProductionCapacity { get; } //Set@Instantiation
        private decimal _accountBalance; //Set@Instantiation and can change internally
        public decimal AccountBalance {
            get { return _accountBalance; }
        }
        private readonly int _producedItemsCount; //Set@Instantiation and can't change internally
        public int ProducedItemsCount {
            get { return _producedItemsCount; }
        }

        public FinanceManufacturingExample(int productSerialNumber, double fixedInterestRate, int maxProductionCapacity, decimal accountBalance, int producedItemsCount) {  //ForObjectInstantiation
            ProductSerialNumber = productSerialNumber;
            FixedInterestRate = fixedInterestRate;
            MaxProductionCapacity = maxProductionCapacity;
            _accountBalance = accountBalance;
            _producedItemsCount = producedItemsCount;
        }

        public void UpdateRiskFactor(decimal riskFactor) {
            RiskFactor = riskFactor;
        }

        public void DisplayInfo() {
            Console.WriteLine($"StockPrice: {StockPrice}  |  ProductSerialNumber: {ProductSerialNumber}  |  RiskFactor: {RiskFactor}  |  ProductionStatus: {ProductionStatus}  |  FixedInterestRate: {FixedInterestRate}  |  AccountBalance: {AccountBalance}  |  ProducedItemsCount: {ProducedItemsCount}");
        }
    }
    public class Program {
        public static void Main() {
            FinanceManufacturingExample example = new FinanceManufacturingExample(12345, 0.025, 1000, 2000m, 500); //1stObjectCreated

            example.StockPrice = 150.5m;
            // example.ProductSerialNumber = 54321; // This line will not work as ProductSerialNumber is private
            // example.RiskFactor = 0.15m; // This line will not work as RiskFactor is private
            example.ProductionStatus = "In Progress";

            // example.AccountBalance = 3000m; // This line will not work as AccountBalance has no set method
            // example.ProducedItemsCount = 1000; // This line will not work as ProducedItemsCount is readonly

            Console.WriteLine("Internal updates:");
            example.UpdateRiskFactor(0.25m);

            Console.WriteLine("External updates:");
            FinanceManufacturingExample externalExample = new FinanceManufacturingExample(67890, 0.03, 2000, 5000m, 1000); //2ndObjectCreated
            externalExample.StockPrice = 200.5m;
            // externalExample.ProductSerialNumber = 98765; // This line will not work as ProductSerialNumber is private
            // externalExample.RiskFactor = 0.20m; // This line will not work as RiskFactor is private
            externalExample.ProductionStatus = "Complete";

            // externalExample.AccountBalance = 6000m; // This line will not work as AccountBalance has no set method
            // externalExample.ProducedItemsCount = 2000; // This line will not work as ProducedItemsCount is readonly

            example.DisplayInfo();
            externalExample.DisplayInfo();
        }
    }



    /* OUTPUT Internal updates:
External updates:
StockPrice: 150.5  |  ProductSerialNumber: 12345  |  RiskFactor: 0.25  |  ProductionStatus: In Progress  |  FixedInterestRate: 0.025  |  AccountBalance: 2000  |  ProducedItemsCount: 500
StockPrice: 200.5  |  ProductSerialNumber: 67890  |  RiskFactor: 0  |  ProductionStatus: Complete  |  FixedInterestRate: 0.03  |  AccountBalance: 5000  |  ProducedItemsCount: 1000 */

}
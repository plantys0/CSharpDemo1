namespace _Icomparable
{
    using System;
    using System.Collections.Generic;

    // Implementing IComparable for a simple BankAccount class
    public class BankAccount : IComparable<BankAccount>
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public BankAccount(int accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        // Implement the CompareTo method from IComparable<BankAccount> interface
        public int CompareTo(BankAccount other)
        {
            // Comparing based on the account balance
            return Balance.CompareTo(other.Balance);
        }

        public override string ToString()
        {
            return $"Account {AccountNumber}: {Balance:C2}";// The purpose of the ToString() method override in the above code is to provide a custom string representation of the BankAccount object. This makes it easier to display the object's information in a human-readable format.
        }
    }

    public class Program
    {
        public static void Main()
        {
            List<BankAccount> accounts = new List<BankAccount>
        {
            new BankAccount(1, 1500m),
            new BankAccount(2, 3500m),
            new BankAccount(3, 500m)
        };

            // Sort the list of bank accounts by their balance
            accounts.Sort();

            // Output the sorted list of bank accounts
            foreach (var account in accounts)
            {
                Console.WriteLine(account);
            }
        }
    }

}

// DTOs/AuthDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string TaxIdentifier { get; set; }
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

// DTOs/AccountDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class CreateAccountDto
    {
        [Required]
        [StringLength(20)]
        public string AccountType { get; set; } // "Savings" or "Checking"

        [Range(0, double.MaxValue)]
        public decimal InitialDeposit { get; set; } = 0;
    }

    public class AccountDto
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class AccountBalanceDto
    {
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

// DTOs/TransactionDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class DepositDto
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }

    public class WithdrawDto
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }

    public class TransferDto
    {
        [Required]
        public int FromAccountId { get; set; }

        [Required]
        public int ToAccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }

    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionReference { get; set; }
        public string Status { get; set; }
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
    }

    public class TransactionHistoryDto
    {
        public List<TransactionDto> Transactions { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}

// Services/IAccountService.cs
using BankingAPI.DTOs;

namespace BankingAPI.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetUserAccountsAsync(int userId);
        Task<AccountDto> GetAccountAsync(int accountId, int userId);
        Task<AccountDto> CreateAccountAsync(int userId, CreateAccountDto createAccountDto);
        Task<AccountBalanceDto> GetAccountBalanceAsync(int accountId, int userId);
    }
}

// Services/AccountService.cs
using Microsoft.EntityFrameworkCore;
using BankingAPI.Data;
using BankingAPI.Models;
using BankingAPI.DTOs;

namespace BankingAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankingDbContext _context;

        public AccountService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDto>> GetUserAccountsAsync(int userId)
        {
            return await _context.Accounts
                .Where(a => a.UserId == userId)
                .Select(a => new AccountDto
                {
                    AccountId = a.AccountId,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    Balance = a.Balance,
                    AvailableBalance = a.AvailableBalance,
                    Status = a.Status,
                    CreatedDate = a.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<AccountDto> GetAccountAsync(int accountId, int userId)
        {
            var account = await _context.Accounts
                .Where(a => a.AccountId == accountId && a.UserId == userId)
                .Select(a => new AccountDto
                {
                    AccountId = a.AccountId,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    Balance = a.Balance,
                    AvailableBalance = a.AvailableBalance,
                    Status = a.Status,
                    CreatedDate = a.CreatedDate
                })
                .FirstOrDefaultAsync();

            if (account == null)
                throw new Exception("Account not found");

            return account;
        }

        public async Task<AccountDto> CreateAccountAsync(int userId, CreateAccountDto createAccountDto)
        {
            // Validate account type
            if (createAccountDto.AccountType != "Savings" && createAccountDto.AccountType != "Checking")
                throw new Exception("Invalid account type");

            // Generate account number
            var accountNumber = GenerateAccountNumber();

            var account = new Account
            {
                UserId = userId,
                AccountNumber = accountNumber,
                AccountType = createAccountDto.AccountType,
                Balance = createAccountDto.InitialDeposit,
                AvailableBalance = createAccountDto.InitialDeposit,
                Status = "Active",
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return new AccountDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                Balance = account.Balance,
                AvailableBalance = account.AvailableBalance,
                Status = account.Status,
                CreatedDate = account.CreatedDate
            };
        }

        public async Task<AccountBalanceDto> GetAccountBalanceAsync(int accountId, int userId)
        {
            var account = await _context.Accounts
                .Where(a => a.AccountId == accountId && a.UserId == userId)
                .FirstOrDefaultAsync();

            if (account == null)
                throw new Exception("Account not found");

            return new AccountBalanceDto
            {
                Balance = account.Balance,
                AvailableBalance = account.AvailableBalance,
                LastUpdated = account.LastModified
            };
        }

        private string GenerateAccountNumber()
        {
            // Generate a 10-digit account number
            var random = new Random();
            var accountNumber = "";
            for (int i = 0; i < 10; i++)
            {
                accountNumber += random.Next(0, 9).ToString();
            }
            return accountNumber;
        }
    }
}

// Services/ITransactionService.cs
using BankingAPI.DTOs;

namespace BankingAPI.Services
{
    public interface ITransactionService
    {
        Task<TransactionDto> DepositAsync(int userId, DepositDto depositDto);
        Task<TransactionDto> WithdrawAsync(int userId, WithdrawDto withdrawDto);
        Task<TransactionDto> TransferAsync(int userId, TransferDto transferDto);
        Task<TransactionHistoryDto> GetTransactionHistoryAsync(int accountId, int userId, int page, int pageSize);
        Task<TransactionDto> GetTransactionAsync(int transactionId, int userId);
    }
}

// Services/TransactionService.cs
using Microsoft.EntityFrameworkCore;
using BankingAPI.Data;
using BankingAPI.Models;
using BankingAPI.DTOs;

namespace BankingAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankingDbContext _context;

        public TransactionService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionDto> DepositAsync(int userId, DepositDto depositDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var account = await _context.Accounts
                    .Where(a => a.AccountId == depositDto.AccountId && a.UserId == userId)
                    .FirstOrDefaultAsync();

                if (account == null)
                    throw new Exception("Account not found");

                if (account.Status != "Active")
                    throw new Exception("Account is not active");

                // Create transaction
                var txn = new Transaction
                {
                    TransactionType = "Deposit",
                    Amount = depositDto.Amount,
                    FromAccountId = depositDto.AccountId,
                    Description = depositDto.Description ?? "Deposit",
                    TransactionDate = DateTime.UtcNow,
                    TransactionReference = GenerateTransactionReference(),
                    Status = "Completed"
                };

                _context.Transactions.Add(txn);

                // Update account balance
                account.Balance += depositDto.Amount;
                account.AvailableBalance += depositDto.Amount;
                account.LastModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TransactionDto
                {
                    TransactionId = txn.TransactionId,
                    TransactionType = txn.TransactionType,
                    Amount = txn.Amount,
                    FromAccountId = txn.FromAccountId,
                    Description = txn.Description,
                    TransactionDate = txn.TransactionDate,
                    TransactionReference = txn.TransactionReference,
                    Status = txn.Status,
                    FromAccountNumber = account.AccountNumber
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TransactionDto> WithdrawAsync(int userId, WithdrawDto withdrawDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var account = await _context.Accounts
                    .Where(a => a.AccountId == withdrawDto.AccountId && a.UserId == userId)
                    .FirstOrDefaultAsync();

                if (account == null)
                    throw new Exception("Account not found");

                if (account.Status != "Active")
                    throw new Exception("Account is not active");

                if (account.AvailableBalance < withdrawDto.Amount)
                    throw new Exception("Insufficient funds");

                // Create transaction
                var txn = new Transaction
                {
                    TransactionType = "Withdrawal",
                    Amount = withdrawDto.Amount,
                    FromAccountId = withdrawDto.AccountId,
                    Description = withdrawDto.Description ?? "Withdrawal",
                    TransactionDate = DateTime.UtcNow,
                    TransactionReference = GenerateTransactionReference(),
                    Status = "Completed"
                };

                _context.Transactions.Add(txn);

                // Update account balance
                account.Balance -= withdrawDto.Amount;
                account.AvailableBalance -= withdrawDto.Amount;
                account.LastModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TransactionDto
                {
                    TransactionId = txn.TransactionId,
                    TransactionType = txn.TransactionType,
                    Amount = txn.Amount,
                    FromAccountId = txn.FromAccountId,
                    Description = txn.Description,
                    TransactionDate = txn.TransactionDate,
                    TransactionReference = txn.TransactionReference,
                    Status = txn.Status,
                    FromAccountNumber = account.AccountNumber
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TransactionDto> TransferAsync(int userId, TransferDto transferDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var fromAccount = await _context.Accounts
                    .Where(a => a.AccountId == transferDto.FromAccountId && a.UserId == userId)
                    .FirstOrDefaultAsync();

                if (fromAccount == null)
                    throw new Exception("Source account not found");

                var toAccount = await _context.Accounts
                    .Where(a => a.AccountId == transferDto.ToAccountId)
                    .FirstOrDefaultAsync();

                if (toAccount == null)
                    throw new Exception("Destination account not found");

                if (fromAccount.Status != "Active" || toAccount.Status != "Active")
                    throw new Exception("One or both accounts are not active");

                if (fromAccount.AvailableBalance < transferDto.Amount)
                    throw new Exception("Insufficient funds");

                // Create transaction
                var txn = new Transaction
                {
                    TransactionType = "Transfer",
                    Amount = transferDto.Amount,
                    FromAccountId = transferDto.FromAccountId,
                    ToAccountId = transferDto.ToAccountId,
                    Description = transferDto.Description ?? "Transfer",
                    TransactionDate = DateTime.UtcNow,
                    TransactionReference = GenerateTransactionReference(),
                    Status = "Completed"
                };

                _context.Transactions.Add(txn);

                // Update account balances
                fromAccount.Balance -= transferDto.Amount;
                fromAccount.AvailableBalance -= transferDto.Amount;
                fromAccount.LastModified = DateTime.UtcNow;

                toAccount.Balance += transferDto.Amount;
                toAccount.AvailableBalance += transferDto.Amount;
                toAccount.LastModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TransactionDto
                {
                    TransactionId = txn.TransactionId,
                    TransactionType = txn.TransactionType,
                    Amount = txn.Amount,
                    FromAccountId = txn.FromAccountId,
                    ToAccountId = txn.ToAccountId,
                    Description = txn.Description,
                    TransactionDate = txn.TransactionDate,
                    TransactionReference = txn.TransactionReference,
                    Status = txn.Status,
                    FromAccountNumber = fromAccount.AccountNumber,
                    ToAccountNumber = toAccount.AccountNumber
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TransactionHistoryDto> GetTransactionHistoryAsync(int accountId, int userId, int page, int pageSize)
        {
            // Verify account ownership
            var account = await _context.Accounts
                .Where(a => a.AccountId == accountId && a.UserId == userId)
                .FirstOrDefaultAsync();

            if (account == null)
                throw new Exception("Account not found");

            var query = _context.Transactions
                .Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId)
                .OrderByDescending(t => t.TransactionDate);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var transactions = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    FromAccountId = t.FromAccountId,
                    ToAccountId = t.ToAccountId,
                    Description = t.Description,
                    TransactionDate = t.TransactionDate,
                    TransactionReference = t.TransactionReference,
                    Status = t.Status,
                    FromAccountNumber = t.FromAccount.AccountNumber,
                    ToAccountNumber = t.ToAccount != null ? t.ToAccount.AccountNumber : null
                })
                .ToListAsync();

            return new TransactionHistoryDto
            {
                Transactions = transactions,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<TransactionDto> GetTransactionAsync(int transactionId, int userId)
        {
            var transaction = await _context.Transactions
                .Include(t => t.FromAccount)
                .Include(t => t.ToAccount)
                .Where(t => t.TransactionId == transactionId && 
                           (t.FromAccount.UserId == userId || 
                            (t.ToAccount != null && t.ToAccount.UserId == userId)))
                .FirstOrDefaultAsync();

            if (transaction == null)
                throw new Exception("Transaction not found");

            return new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                FromAccountId = transaction.FromAccountId,
                ToAccountId = transaction.ToAccountId,
                Description = transaction.Description,
                TransactionDate = transaction.TransactionDate,
                TransactionReference = transaction.TransactionReference,
                Status = transaction.Status,
                FromAccountNumber = transaction.FromAccount.AccountNumber,
                ToAccountNumber = transaction.ToAccount?.AccountNumber
            };
        }

        private string GenerateTransactionReference()
        {
            return $"TXN{DateTime.UtcNow:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }
    }
}

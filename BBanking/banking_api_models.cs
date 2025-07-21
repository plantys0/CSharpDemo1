// Models/User.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string TaxIdentifier { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

// Models/Account.cs


namespace BankingAPI.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountType { get; set; } // "Savings", "Checking"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AvailableBalance { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(20)]
        public string Status { get; set; } // "Active", "Suspended", "Closed"

        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Transaction> DebitTransactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Transaction> CreditTransactions { get; set; } = new List<Transaction>();
    }
}

// Models/Transaction.cs


namespace BankingAPI.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; } // "Deposit", "Withdrawal", "Transfer"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int FromAccountId { get; set; }

        public int? ToAccountId { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [StringLength(50)]
        public string TransactionReference { get; set; }

        [StringLength(20)]
        public string Status { get; set; } // "Pending", "Completed", "Failed"

        // Navigation properties
        [ForeignKey("FromAccountId")]
        public virtual Account FromAccount { get; set; }

        [ForeignKey("ToAccountId")]
        public virtual Account ToAccount { get; set; }
    }
}

// Models/Role.cs


namespace BankingAPI.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

// Models/UserRole.cs


namespace BankingAPI.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public DateTime AssignedDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}

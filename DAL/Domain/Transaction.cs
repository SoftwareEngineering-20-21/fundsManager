using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain
{
    /// <summary>
    /// Transaction class
    /// contains fields Bank account, amount, transaction date, description, user id, user
    /// </summary>
    
    public class Transaction:BaseEntity
    {
        public virtual BankAccount BankAccountFrom { get; set; }
        public virtual BankAccount BankAccountTo { get; set; }
        [Required]
        public decimal AmountTo { get; set; }
        [Required]
        public decimal AmountFrom { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

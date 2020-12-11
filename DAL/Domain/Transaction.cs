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

        /// <summary>
        /// bank account from Transaction
        /// </summary>
        public virtual BankAccount BankAccountFrom { get; set; }

        /// <summary>
        /// bank account to Transaction
        /// </summary>
        public virtual BankAccount BankAccountTo { get; set; }

        /// <summary>
        /// Amount to Transaction
        /// </summary>
        [Required]
        public decimal AmountTo { get; set; }

        /// <summary>
        /// Amount From Transaction
        /// </summary>
        [Required]
        public decimal AmountFrom { get; set; }

        /// <summary>
        /// Transaction Date
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Transaction Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Transaction UserId
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Transaction User
        /// </summary>
        public virtual User User { get; set; }
    }
}

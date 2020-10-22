using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain
{
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
    }
}

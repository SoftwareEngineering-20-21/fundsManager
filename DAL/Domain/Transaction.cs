using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class Transaction:BaseEntity
    {
        public BankAccount BankAccountFrom { get; set; }
        public BankAccount BankAccountTo { get; set; }
        [Required]
        public decimal AmountTo { get; set; }
        [Required]
        public decimal AmountFrom { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

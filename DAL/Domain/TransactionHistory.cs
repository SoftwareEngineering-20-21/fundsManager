using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public BankAccount BankAccountFrom { get; set; }
        [Required]
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

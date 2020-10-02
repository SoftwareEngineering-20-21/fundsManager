using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Domain
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
        public BankAccount BankAccountFrom { get; set; }
        public BankAccount BankAccountTo { get; set; }
        public decimal AmountTo { get; set; }
        public decimal AmountFrom { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

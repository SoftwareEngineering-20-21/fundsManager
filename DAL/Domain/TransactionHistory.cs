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
        public BankAccount FromBankAccount { get; set; }
        public BankAccount ToBankAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

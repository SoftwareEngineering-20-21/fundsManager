using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    class TransactionHistory : BaseEntity
    {
        public BankAccount FromBankAccount { get; set; }
        public BankAccount ToBankAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

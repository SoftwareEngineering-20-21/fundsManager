using System;

namespace BLL.DTO
{
    public class TransactionDTO
    {
        public int BankAccountFromId { get; set; }
        public virtual BankAccountDTO BankAccountFrom { get; set; }
        public int BankAccountToId { get; set; }
        public virtual BankAccountDTO BankAccountTo { get; set; }
        public decimal AmountTo { get; set; }
        public decimal AmountFrom { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}
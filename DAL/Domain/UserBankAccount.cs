namespace DAL.Domain
{
    /// <summary>
    /// User Bank Account class
    /// Contains fields user Id, user, bank account Id, bank account
    /// </summary>
    
    public class UserBankAccount
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }

    }
} 

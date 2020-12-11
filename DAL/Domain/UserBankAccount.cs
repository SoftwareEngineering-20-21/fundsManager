namespace DAL.Domain
{
    /// <summary>
    /// User Bank Account class
    /// Contains fields user Id, user, bank account Id, bank account
    /// </summary>
    
    public class UserBankAccount
    {
        /// <summary>
        /// User Bank Account user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User Bank Account user
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// User Bank Account Bank Account id
        /// </summary>
        public int BankAccountId { get; set; }

        /// <summary>
        /// User Bank Account bank account
        /// </summary>
        public virtual BankAccount BankAccount { get; set; }

    }
} 

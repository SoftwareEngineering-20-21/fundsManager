namespace DAL.Domain
{
    public class UserBankAccount
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

    }
}

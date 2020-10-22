namespace BLL.DTO
{
    public class UserBankAccountDTO
    {
        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public int BankAccountId { get; set; }
        public virtual BankAccountDTO BankAccount { get; set; }
    }
}
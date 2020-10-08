using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class User:BaseEntity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public UserDetails? Details { get; set; }
        public IEnumerable<UserBankAccount> BankAccounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserDetails Details { get; set; }
        ICollection<BankAccount> bankAccounts { get; set; }
    }
}

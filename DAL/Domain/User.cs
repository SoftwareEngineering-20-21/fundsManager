using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Domain
{
    public class User:BaseEntity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public IEnumerable<UserBankAccount> BankAccounts { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public int Phone { get; set; }
    }
}

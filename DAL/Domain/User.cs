using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Domain
{
    /// <summary>
    /// User class
    /// Contains fields of user mail, user password, user name, user surname
    /// user phone
    /// </summary>
    
    public class User:BaseEntity
    {
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual List<UserBankAccount> BankAccounts { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}

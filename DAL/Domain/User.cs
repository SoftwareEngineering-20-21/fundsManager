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
        
        /// <summary>
        /// User mail
        /// </summary>
        [Required]
        public string Mail { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        /// <summary>
        /// User list of bank accounts
        /// </summary>
        public virtual List<UserBankAccount> BankAccounts { get; set; }
        
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// User surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// User phone
        /// </summary>
        [Required]
        public string Phone { get; set; }
    }
}

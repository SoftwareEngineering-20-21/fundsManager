using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;

namespace DAL.Domain
{
    /// <summary>
    /// Bank Account class
    /// Contains fields name, account type, currency type, currency id, list of users
    /// </summary>
    
    public class BankAccount:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public AccountType Type { get; set; }
        
        [Required]
        [ForeignKey("CurrencyType")]
        public int CurrencyTypeId { get; set; }

        [Required]
        public virtual Currency CurrencyType { get; set; }
        public virtual List<UserBankAccount> Users { get; set; }
    }
}

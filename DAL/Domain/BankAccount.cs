using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;

namespace DAL.Domain
{
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
        public Currency CurrencyType { get; set; }
        public IEnumerable<UserBankAccount> Users { get; set; }

    }
}

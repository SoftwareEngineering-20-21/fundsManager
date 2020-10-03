using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Domain
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AccountType Type { get; set; }
        [Required]
        public Currency CurrencyType { get; set; }
        public IEnumerable<UserBankAccount> Users { get; set; }

    }
}

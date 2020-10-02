using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Enums;

namespace DAL.Domain
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public Currency CurrencyType { get; set; }
        public IEnumerable<UserBankAccount> Users { get; set; }

    }
}

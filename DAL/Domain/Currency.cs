using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class Currency:BaseEntity
    {
        [Required]
        public string Code { get; set; }
        public IEnumerable<BankAccount> BankAccounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    class BankAccount:BaseEntity
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        ICollection<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Domain
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public ICollection<User> Users { get; set; } 
    }
}

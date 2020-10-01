using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserDetails Details { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}

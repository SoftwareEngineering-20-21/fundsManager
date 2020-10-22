using System.Collections.Generic;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public List<BankAccountDTO> BankAccounts { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
    }
}
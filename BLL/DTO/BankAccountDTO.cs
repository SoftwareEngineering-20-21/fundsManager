using System.Collections.Generic;
using BLL.Enums;

namespace BLL.DTO
{
    public class BankAccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public int CurrencyTypeId { get; set; }
        public CurrencyDTO CurrencyType { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
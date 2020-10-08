using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class UserDetails:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public int Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

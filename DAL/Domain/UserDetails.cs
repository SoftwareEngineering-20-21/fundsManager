using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public int Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

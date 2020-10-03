using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
    }
}

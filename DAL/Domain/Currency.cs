using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    public class Currency:BaseEntity
    {
        [Required]
        public string Code { get; set; }
    }
}

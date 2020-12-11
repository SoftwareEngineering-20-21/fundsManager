using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    /// <summary>
    /// Currency class
    /// Contains field currency code
    /// </summary>
    
    public class Currency:BaseEntity
    {
        /// <summary>
        /// Currency code 
        /// </summary>
        
        [Required]
        public string Code { get; set; }
    }
}

﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    /// <summary>
    /// Currency class
    /// Contains field currency code
    /// </summary>
    
    public class Currency:BaseEntity
    { 
        [Required]
        public string Code { get; set; }
    }
}

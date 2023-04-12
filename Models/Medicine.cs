using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginApplication.Models
{
    public class Medicine
    {
        [Key]
        public int sr_id { get; set; }
        [Required]
        [Display(Name = "Medicine Name")]
        public string Medicine_name { get; set;}
        [Required]
        public decimal Price { get;set; }
        [Required]
        [Display(Name = "Total Stock")]
        public int Total_stock { get; set; }
    }
}
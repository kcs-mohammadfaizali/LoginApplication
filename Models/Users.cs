using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginApplication.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string First_name { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string Last_name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

}
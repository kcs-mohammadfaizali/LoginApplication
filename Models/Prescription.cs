using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace LoginApplication.Models
{
    public class Prescription
    {
        [Key]
        [Display(Name = "Prescription Id")]
        public int Prescription_id { get; set; }
        [Display(Name = "Patient Id")]
        public int patient_id { get; set; }
        [Display(Name = "Create Date")]
        public DateTime Date { get; set; }
        public string name { get; set; }
        public int user_id { get; set; }

    }
}
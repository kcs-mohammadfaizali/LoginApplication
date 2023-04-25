using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginApplication.Models
{
    public class Report
    {
        public DateTime Date { get; set; }
        public int totalprescription { get; set; }
        public decimal price { get; set; }
        public string Medicine_name { get; set; }
        public int totalquantity { get; set; }
    }
}
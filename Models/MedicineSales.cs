using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoginApplication.Models
{
    public class MedicineSales
    {
        public int id { get; set; }
        public int Prescription_id { get; set; }
        public int sr_id { get; set; }
        public string Medicine_name { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal totalprice { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

    }
}
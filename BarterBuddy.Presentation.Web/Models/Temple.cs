using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterBuddy.Presentation.Web.Models
{
    public class Temple
    {

        public int  TempleId { get; set; }
        public int ReligionId { get; set; }
        public string TempleName { get; set; }
        public string TempleOfficeNo { get; set; }
        public string TempleOfficeAddress { get; set; }
        public string TempleSynopsis { get; set; }
        public decimal TempleLat { get; set; }
        public decimal TempleLng { get; set; }
        

    }
}
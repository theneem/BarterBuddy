using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterBuddy.Presentation.Web.Models
{
    public class Saint
    {

        public int SaintId { get; set; }
        public int ReligionId { get; set; }
        public string SaintName { get; set; }
        public string SaintContactNo { get; set; }
        public string SaintCurrentLocation { get; set; }
        public string SaintSynopsis { get; set; }
        public decimal SaintLat { get; set; }
        public decimal SaintLag { get; set; }



    }
}
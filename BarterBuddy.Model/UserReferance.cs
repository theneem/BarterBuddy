using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Model
{
    public class UserReferance
    {
        public long UserDetailID { get; set; }

        public long UserID { get; set; }

        public string WhatsAppNumber { get; set; }

        public string Name { get; set; }
    }
}

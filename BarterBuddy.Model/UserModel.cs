using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Model
{
    public class UserModel
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LoginType { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifidDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarterBuddy.Common.Helper;
using BarterBuddy.Model;
using BarterBuddy.Data.BarterBuddyEDMX;

namespace BarterBuddy.Data.IData
{
    public interface ILoginDataRepository
    {
        Task<ResponseHelper> RegisterUser(BBUser userModel);
    }
}

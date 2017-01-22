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
        Task<ResponseHelper> RegisterUserModel(UserRegisterModel userModel);

        Task<ResponseHelper> ValidateUser(UserModel userModel);

        Task<ResponseHelper> UpdateProfile(UserModel userModel);

        Task<ResponseHelper> GetUserDetail(UserModel userModel);
    }
}

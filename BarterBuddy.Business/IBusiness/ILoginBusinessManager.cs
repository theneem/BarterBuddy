using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarterBuddy.Common.Helper;
using BarterBuddy.Model;

namespace BarterBuddy.Business.IBusiness
{
    public interface ILoginBusinessManager
    {
        Task<ResponseHelper> RegisterUser(UserModel userModel);

        Task<ResponseHelper> ValidateUser(UserModel userModel);

        Task<ResponseHelper> UpdateProfile(UserModel userModel);

        Task<ResponseHelper> GetUserDetail(UserModel userModel);
    }
}

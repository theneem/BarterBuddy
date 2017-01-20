using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarterBuddy.Business.IBusiness;
using BarterBuddy.Model;
using BarterBuddy.Data.IData;
using BarterBuddy.Data;
using AutoMapper;
using BarterBuddy.Common.IOC;
using BarterBuddy.Data.BarterBuddyEDMX;
using BarterBuddy.Common.Helper;

namespace BarterBuddy.Business
{
    public class LoginBusinessManager : ILoginBusinessManager
    {

        private readonly ILoginDataRepository loginRepo;

        public LoginBusinessManager()
        {
            loginRepo = IOCHelper.Resolve<LoginDataRepository>();
        }

        public async Task<ResponseHelper> GetUserDetail(UserModel userModel)
        {
            return await loginRepo.GetUserDetail(userModel);
        }

        public async Task<ResponseHelper> RegisterUser(UserModel userModel)
        {
            return await loginRepo.RegisterUserModel(userModel);
        }

        public async Task<ResponseHelper> UpdateProfile(UserModel userModel)
        {
           return await  loginRepo.UpdateProfile(userModel);
        }

        public async Task<ResponseHelper> ValidateUser(UserModel userModel)
        {
            return await loginRepo.ValidateUser(userModel);
        }
    }
}

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

namespace BarterBuddy.Business
{
    public class LoginBusinessManager : ILoginBusinessManager
    {

        private readonly ILoginDataRepository loginRepo;
        public LoginBusinessManager()
        {
            loginRepo = IOCHelper.Resolve<LoginDataRepository>();
        }
        public void RegisterUser(UserModel user)
        {

            loginRepo.RegisterUser(Mapper.Map<BBUser>(user));
        }
    }
}

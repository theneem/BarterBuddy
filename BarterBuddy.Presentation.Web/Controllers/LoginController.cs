using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BarterBuddy.Common.Helper;
using BarterBuddy.Common.Encryption;
using BarterBuddy.Model;
using BarterBuddy.Presentation.Web.Models;
using log4net;
using log4net.Config;
using BarterBuddy.Common.IOC;
using BarterBuddy.Business;
using BarterBuddy.Business.IBusiness;
using System.Configuration;
using BarterBuddy.Common.Rest;

namespace BarterBuddy.Presentation.Web.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILoginBusinessManager loginmanager;

        /// <summary>
        /// The aladdin rest client
        /// </summary>
        public readonly RestClient aladdinRestClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>

        public LoginController()
        {
            var aladdinUrl = ConfigurationManager.AppSettings[Constant.GETURL];
            aladdinRestClient = new RestClient(aladdinUrl);
            loginmanager = IOCHelper.Resolve<LoginBusinessManager>();

        }

        // GET: /Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            TempData[Constant.ERROR] = "";
            return View(new UserModel());
        }


        [HttpPost]
        public async Task<ActionResult> ValidateUserLogin(UserModel user)
        {
            user.Password = CryptorHelper.Encrypt(user.Password, true);
            var result = await aladdinRestClient.PostAsync<UserModel, ResponseHelper>(Constant.VALIDATEUSER, user, false);

            if (result.StatusCode == Enums.ResponseCode.Error)
            {
                TempData[Constant.ERROR] = result.Message;
            }

            return View("Login",new UserModel());
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserModel userModel)
        {
            userModel.Password = CryptorHelper.Encrypt(userModel.Password, true);
            var result = await aladdinRestClient.PostAsync<UserModel, ResponseHelper>(Constant.REGISTERUSER, userModel, false);

            if (result.StatusCode == Enums.ResponseCode.Error)
            {
                TempData[Constant.ERROR] = result.Message;
            }

            return View("Login", new UserModel());
        }
    }
}
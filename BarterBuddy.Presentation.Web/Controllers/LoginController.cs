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
using Newtonsoft.Json;
using BarterBuddy.Presentation.Web.Common;
using System.Web.Security;

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
        public ActionResult Login()
        {
            return View(new LoginModel());
        }


        [HttpPost]
        public async Task<ActionResult> ValidateUserLogin(LoginModel user)
        {
            user.LoginUser.Password = CryptorHelper.Encrypt(user.LoginUser.Password, true);
            var responseHelper = await aladdinRestClient.PostAsync<UserModel, ResponseHelper>(Constant.VALIDATEUSER, user.LoginUser, false);

            if (responseHelper.StatusCode == Enums.ResponseCode.Success)
            {

                var userModel = JsonConvert.DeserializeObject<UserModel>(responseHelper.Payload.ToString());
                SiteSession.CurrentSession.UserId = userModel.UserID;
                SiteSession.CurrentSession.UserName = userModel.UserName;
                SiteSession.CurrentSession.UserRoleId = userModel.LoginType;
                FormsAuthentication.SetAuthCookie(SiteSession.CurrentSession.Authorization, true);
            }

            return Json(responseHelper, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(LoginModel userModel)
        {
            userModel.RegisterUser.Password = CryptorHelper.Encrypt(userModel.RegisterUser.Password, true);
            var responseHelper = await aladdinRestClient.PostAsync<UserRegisterModel, ResponseHelper>(Constant.REGISTERUSER, userModel.RegisterUser, false);

            if (responseHelper.StatusCode == Enums.ResponseCode.Error)
            {
                return Json(responseHelper, JsonRequestBehavior.AllowGet);
            }


            var userDetail = JsonConvert.DeserializeObject<UserModel>(responseHelper.Payload.ToString());

            SiteSession.CurrentSession.UserId = userDetail.UserID;
            SiteSession.CurrentSession.UserName = userDetail.UserName;
            SiteSession.CurrentSession.UserRoleId = userDetail.LoginType;
            FormsAuthentication.SetAuthCookie(SiteSession.CurrentSession.Authorization, true);
            return RedirectToAction("Index", "Home", userDetail);
        }
    }
}
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
        public readonly RestClient barterBuddyRestClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>

        public LoginController()
        {
            var aladdinUrl = ConfigurationManager.AppSettings[Constant.GETURL];
            barterBuddyRestClient = new RestClient(aladdinUrl);
            loginmanager = IOCHelper.Resolve<LoginBusinessManager>();

        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            //MailHelper objHelper = new MailHelper("Parimal.loliyaniya@gmail.com", Constant.NEWPASSWORDNOTIFICATION, GetHTMLForUpdatePassword("ABVDEFDDDF"));

            //objHelper.Send();
            return View(new LoginModel());
        }


        [HttpPost]
        public async Task<ActionResult> ValidateUserLogin(LoginModel user)
        {
            user.LoginUser.Password = CryptorHelper.Encrypt(user.LoginUser.Password, true);
            var responseHelper = await barterBuddyRestClient.PostAsync<UserModel, ResponseHelper>(Constant.VALIDATEUSER, user.LoginUser, false);

            if (responseHelper.StatusCode == Enums.ResponseCode.Success)
            {
                var userModel = JsonConvert.DeserializeObject<UserModel>(responseHelper.Payload.ToString());
                if (!SiteSession.IsAlreadyExist(user.LoginUser.UserName))
                {
                    SiteSession.InitiateSession(userModel.UserName);
                }
                SiteSession.LoggedUserId = userModel.UserID;
                SiteSession.LoggedUsername = userModel.UserName.Trim();
                SiteSession.LoggedUserType = userModel.LoginType;
                FormsAuthentication.SetAuthCookie(SiteSession.LoggedUsername, true);
            }

            return Json(responseHelper, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(LoginModel userModel)
        {
            userModel.RegisterUser.Password = CryptorHelper.Encrypt(userModel.RegisterUser.Password, true);
            var responseHelper = await barterBuddyRestClient.PostAsync<UserRegisterModel, ResponseHelper>(Constant.REGISTERUSER, userModel.RegisterUser, false);

            if (responseHelper.StatusCode == Enums.ResponseCode.Error)
            {
                return Json(responseHelper, JsonRequestBehavior.AllowGet);
            }


            var userDetail = JsonConvert.DeserializeObject<UserModel>(responseHelper.Payload.ToString());

            SiteSession.InitiateSession(userDetail.UserName);
            SiteSession.LoggedUserId = userDetail.UserID;
            SiteSession.LoggedUsername = userDetail.UserName.Trim();
            SiteSession.LoggedUserType = userDetail.LoginType;
            FormsAuthentication.SetAuthCookie(SiteSession.LoggedUsername, true);
            return RedirectToAction("Index", "Home", userDetail);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordUser userModel)
        {
            ResponseHelper savedSuccessfully = new ResponseHelper();
            if (!string.IsNullOrEmpty(userModel.userName))
            {
                string userName = userModel.userName;
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$";
                var random = new Random();
                var password = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
                var stringToSend = Helper.ConvertToHashString(password);
                userModel.password = stringToSend;

                savedSuccessfully = await barterBuddyRestClient.PostAsync<ResetPasswordUser, ResponseHelper>(Constant.RESETPASSOWRD, userModel, false);
                if (savedSuccessfully.StatusCode == Enums.ResponseCode.Success)
                {
                    MailHelper objHelper = new MailHelper("Parimal.loliyaniya@gmail.com", Constant.NEWPASSWORDNOTIFICATION, GetHTMLForUpdatePassword(password));
                    objHelper.Send();
                    // write code for mail sent here.
                }
                else
                {
                    return Json(savedSuccessfully, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(savedSuccessfully, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            SiteSession.ClearCache();
            return RedirectToAction("Login", "Login");
        }

        private string GetHTMLForUpdatePassword(string newPassword)
        {
            string htmlText = string.Empty;
            htmlText = "<br/></br><b>Your New Password is</b> - " + newPassword;
            htmlText += "<br/><br/>BarterBuddy Admin Team !";
            return htmlText;
        }
    }
}
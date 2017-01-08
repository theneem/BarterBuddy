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
namespace BarterBuddy.Presentation.Web.Controllers
{

    public class LoginController : BaseController
    {
        private readonly ILoginBusinessManager loginmanager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>

        public LoginController()
        {
            loginmanager = IOCHelper.Resolve<LoginBusinessManager>();

        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
             ViewBag.ReturnUrl = returnUrl;
            return View(new UserModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> ValidateUserLogin(UserModel user)
        {
            var data = new LoginViewModel { Email = "parimal.loliyaniya@gmail.com", Password = "12345" };
            var result = await aladdinRestClient.PostAsync<LoginViewModel, ResponseHelper>(Constant.VALIDATEUSERLOGIN, data, false);
            return new JsonResult();
        }

        public async Task<JsonResult> RegisterUser(UserModel userModel)
        {
            userModel.Password = CryptorHelper.Encrypt(userModel.Password, true);
            return Json(new ResponseHelper(), JsonRequestBehavior.AllowGet);
        }
    }
}
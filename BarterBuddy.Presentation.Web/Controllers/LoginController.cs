using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BarterBuddy.Common.Helper;
using BarterBuddy.Model;
using BarterBuddy.Presentation.Web.Models;
using log4net;
using log4net.Config;

namespace BarterBuddy.Presentation.Web.Controllers
{

    public class LoginController : BaseController
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> ValidateUserLogin()
        {
            var data = new LoginViewModel { Email = "parimal.loliyaniya@gmail.com" };
            var result = await aladdinRestClient.PostAsync<LoginViewModel, ResponseHelper>(Constant.VALIDATEUSERLOGIN, data, data);
            return new JsonResult();
        }
    }
}
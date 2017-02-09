using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using BarterBuddy.Business;
using BarterBuddy.Business.IBusiness;
using BarterBuddy.Common.IOC;
using BarterBuddy.Model;

namespace BarterBuddy.Presentation.Web.Area.api
{
    public class LoginController : ApiController
    {
        private ILoginBusinessManager loginManager;

        public LoginController()
        {
            loginManager = IOCHelper.Resolve<LoginBusinessManager>();
        }

        [HttpPost]
        [Route("~/api/Login/ValidateUser/{user}")]
        public async Task<IHttpActionResult> ValidateUser(UserModel user)
        {
            try
            {
                var result = await loginManager.ValidateUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("~/api/Login/GetUserDetail/{user}")]
        public async Task<IHttpActionResult> GetUserDetail(UserModel user)
        {
            try
            {
                var result = await loginManager.GetUserDetail(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("~/api/Login/RegisterUser/{user}")]
        public async Task<IHttpActionResult> RegisterUser(UserRegisterModel user)
        {
            try
            {
                var result = await loginManager.RegisterUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("~/api/Login/UpdateProfile/{user}")]
        public async Task<IHttpActionResult> UpdateProfile(UserModel user)
        {
            try
            {
                var result = await loginManager.UpdateProfile(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("~/api/Login/ResetPassword/{user}")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordUser user)
        {
            try
            {
                var result = await loginManager.ResetPassword(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }
    }
}

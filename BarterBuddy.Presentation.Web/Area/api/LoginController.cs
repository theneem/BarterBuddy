using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarterBuddy.Business.IBusness;
using BarterBuddy.Common.Helper;
using BarterBuddy.Common.IOC;
using BarterBuddy.Presentation.Web.Models;

namespace BarterBuddy.Presentation.Web.Area.api
{
    public class LoginController : ApiController
    {
        private ILoginBusinessManager loginManager;


        public LoginController()
        {
            loginManager = IOCHelper.Resolve<ILoginBusinessManager>();
        }

        [HttpGet]
        [Route("~/api/Login/ValidateUserLogin/{data}")]
        public async Task<IHttpActionResult> ValidateUserLogin(LoginViewModel data)
        {
            try
            {
                //var asset = await loginManager.GetAssetCustomFieldDetails(assetId, companyId, allowEdit, categoryId);
                return Ok(new ResponseHelper { StatusCode = Enums.ResponseCode.Success });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("~/api/Login/GetDetail/{data}")]
        public async Task<IHttpActionResult> GetDetail(int data)
        {
            try
            {
                //var asset = await loginManager.GetAssetCustomFieldDetails(assetId, companyId, allowEdit, categoryId);
                return Ok(new ResponseHelper { StatusCode = Enums.ResponseCode.Success });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return InternalServerError(ex);
            }
        }
    }
}

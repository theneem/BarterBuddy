using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BarterBuddy.Business;
using BarterBuddy.Business.IBusiness;
using BarterBuddy.Common.IOC;
namespace BarterBuddy.Presentation.Web.Area.api
{
    public class SettingsController : ApiController
    {
        private ISettingsBusinessManager SettingsManager;

        public SettingsController()
        {
            SettingsManager = IOCHelper.Resolve<SettingsBusinessManager>();
        }

    }
}

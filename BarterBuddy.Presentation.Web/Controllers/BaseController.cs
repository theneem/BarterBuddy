﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BarterBuddy.Common.Helper;
using BarterBuddy.Common.Rest;
using BarterBuddy.Presentation.Web.Common;

namespace BarterBuddy.Presentation.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// The aladdin rest client
        /// </summary>
        public readonly RestClient aladdinRestClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        public BaseController()
        {
            var aladdinUrl = ConfigurationManager.AppSettings[Constant.GETURL];
            aladdinRestClient = new RestClient(aladdinUrl);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        /// <exception cref="System.ArgumentNullException">filterContext</exception>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var ctx = System.Web.HttpContext.Current;

            // If the browser session or authentication session has expired...
            if (ctx.Session.IsNewSession &&
                  !((filterContext.RouteData.Values["Controller"].ToString() == "Login" &&
                    filterContext.RouteData.Values["Action"].ToString() == "Index")))
            {
                if (SiteSession.CurrentSession.UserName == null)
                {
                    Session.RemoveAll();
                    Session.Abandon();
                    if (Request.Cookies["Culture"] != null)
                    {
                        SiteSession.CurrentSession.Culture = Request.Cookies["Culture"].Value;
                    }
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // For AJAX requests, we're overriding the returned JSON result with a simple string,
                        // indicating to the calling JavaScript code that a redirect should be performed.

                        //filterContext.Result = new ContentResult { Content = "_Logon_" };
                    }
                    else
                    {
                        // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                        // simply displays a temporary 5 second notification that they have timed out, and
                        // will, in turn, redirect to the logon page.

                        if (ctx.Request.Url.ToString().Contains("/WorkOrder/NewWorkRequest?id="))
                        {
                            // string companyId = HttpUtility.ParseQueryString(ctx.Request.Url.ToString())["id"];
                            // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "WorkOrder" }, { "Action", "NewWorkRequest" }, { "id", companyId } });
                            return;
                        }
                        else
                        {
                            var url = this.Request.Url;
                            var returnUrl = string.Empty;
                            if (url != null)
                            {
                                returnUrl = url.ToString();
                            }

                            filterContext.Result =
                                    new RedirectToRouteResult(
                                        new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" }, { "returnUrl", returnUrl } });
                        }
                    }
                }
            }
            //base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string[] suppportedlanguages = { "en-us", "ar-AE" };
            if (SiteSession.CurrentSession != null)
            {
                if (string.IsNullOrEmpty(SiteSession.CurrentSession.Culture))
                {


                    //Detect Request Culture and set in session if valid culture.
                    //if (Request.UserLanguages == null)
                    //    SiteSession.CurrentSession.Culture = "en-us";
                    //else
                    //{
                    string culture = Request.UserLanguages[0].ToLower();
                    if (Request.Cookies["Culture"] != null)
                    {
                        culture = Request.Cookies["Culture"].Value;
                    }
                    if (suppportedlanguages.Any(x => x.ToLower() == culture.ToLower()))
                    {
                        SiteSession.CurrentSession.Culture = culture;
                    }
                    else
                    {
                        SiteSession.CurrentSession.Culture = "en-us";
                    }
                }

                if (!string.IsNullOrEmpty(SiteSession.CurrentSession.Culture))
                {
                    SetCulture(SiteSession.CurrentSession.Culture);
                }
            }
            else
            {
                string culture = string.Empty;
                if (Request.Cookies["Culture"] != null)
                {
                    culture = Request.Cookies["Culture"].Value;
                }
                if (suppportedlanguages.Any(x => x.ToLower() == culture.ToLower()))
                {
                    SiteSession.CurrentSession.Culture = culture;
                    SetCulture(SiteSession.CurrentSession.Culture);
                }
            }
        }

        public void SetCulture(string langCode)
        {
            var ci = new System.Globalization.CultureInfo(langCode);
            if (ci.IsNeutralCulture)
            {
                ci = CultureInfo.CreateSpecificCulture(langCode);
            }
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = ci;
            SiteSession.CurrentSession.Culture = langCode;
        }
    }
}
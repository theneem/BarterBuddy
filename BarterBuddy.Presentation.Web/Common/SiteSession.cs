using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using BarterBuddy.Common.Helper;

namespace BarterBuddy.Presentation.Web.Common
{
    //[Serializable]
    public class SiteSession
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        //public UserRoles UserRole { get; set; }

        /// <summary>
        /// Gets or sets the current UI culture.
        /// </summary>
        /// <remarks>
        /// Values meaning: 0 = InvariantCulture (en-US), 1 = ro-RO, 2 = de-DE.
        /// </remarks>
        public static int CurrentUICulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "ar-AE")
                    return 1;
                else
                    return 0;
            }
            set
            {
                //
                // Set the thread's CurrentUICulture.
                //
                if (value == 1)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-AE");
                else
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                //
                // Set the thread's CurrentCulture the same as CurrentUICulture.
                //
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }

        private readonly static SessionProp sessionWrapper = new SessionProp();

        /// <summary>
        /// Gets or sets the current session.
        /// </summary>
        /// <value>The current session.</value>
        public static SessionProp CurrentSession
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                {
                    return sessionWrapper;
                }
                if (System.Web.HttpContext.Current.Session == null)
                {
                    return null;
                }
                if (System.Web.HttpContext.Current.Session["SessionInfo"] == null)
                {
                    System.Web.HttpContext.Current.Session["SessionInfo"] = new SessionProp();
                }
                return System.Web.HttpContext.Current.Session["SessionInfo"] as SessionProp;
            }

            set
            {
                System.Web.HttpContext.Current.Session["SessionInfo"] = value;
            }
        }
    }


    /// <summary>
    /// Session Prop
    /// </summary>
    [Serializable]
    public class SessionProp
    {
        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the login.
        /// </summary>
        /// <value>The name of the login.</value>
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets the is user blocked.
        /// </summary>
        /// <value>The is user blocked.</value>
        public bool IsUserBlocked { get; set; }

        /// <summary>
        /// Gets or sets the email id.
        /// </summary>
        /// <value>The email id.</value>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the user type id.
        /// </summary>
        /// <value>The user type id.</value>
        public Enums.UserType UserTypeId { get; set; }

        /// <summary>
        ///Gets or set the companyid 
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// Gets or set the companyname
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        /// <value>The country id.</value>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the authorization.
        /// </summary>
        /// <value>The authorization.</value>
        public string Authorization { get; set; }

        /// <summary>
        /// Gets or sets the module operations list.
        /// </summary>
        /// <value>The module operations list.</value>
        public string moduleOperationsList { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>The currency.</value>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the user role id.
        /// </summary>
        /// <value>The user role id.</value>
        public long UserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the user address.
        /// </summary>
        /// <value>
        /// The user address.
        /// </value>
        public string UserAddress { get; set; }

        /// <summary>
        /// Gets or sets the user phone.
        /// </summary>
        /// <value>
        /// The user phone.
        /// </value>
        public string UserPhone { get; set; }
    }
}
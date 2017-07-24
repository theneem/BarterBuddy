using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.SessionState;
using BarterBuddy.Common.Helper;

namespace BarterBuddy.Presentation.Web.Common
{
    //[Serializable]
    public class SiteSession
    {
        /// <summary>
        /// Gets or sets Properties for User name
        /// </summary>
        /// <value>The User name</value>
        public static string LoggedUsername
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(GlobalCacheProvider<string>.Instance.GetUserSpecificDetail(Constant.USERNAME)))
                {
                    return GlobalCacheProvider<string>.Instance.GetUserSpecificDetail(Constant.USERNAME);
                }

                return HttpContext.Current.Session[Constant.USERNAME] == null ? string.Empty : HttpContext.Current.Session[Constant.USERNAME].ToString();
            }

            set
            {
                GlobalCacheProvider<string>.Instance.SetUserSpecificDetail(Constant.USERNAME, value);
                HttpContext.Current.Session[Constant.USERNAME] = value;
            }
        }

        /// <summary>
        /// Gets or sets the logged user Id.
        /// </summary>
        /// <value>
        /// The logged user identifier.
        /// </value>
        public static long LoggedUserId
        {
            get
            {
                return HttpContext.Current.Session[Constant.USERID] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session[Constant.USERID]);
            }

            set
            {
                HttpContext.Current.Session[Constant.USERID] = value;
            }
        }

        /// <summary>
        /// Gets or sets the logged user Id.
        /// </summary>
        /// <value>
        /// The logged user identifier.
        /// </value>
        public static long LoggedUserType
        {
            get
            {
                return HttpContext.Current.Session[Constant.LOGGEDUSERTYPE] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session[Constant.LOGGEDUSERTYPE]);
            }

            set
            {
                HttpContext.Current.Session[Constant.LOGGEDUSERTYPE] = value;
            }
        }

        /////// <summary>
        /////// Gets or sets the valid module.
        /////// </summary>
        /////// <value>
        /////// The valid module.
        /////// </value>
        ////public static List<AccessRights> AccessModule
        ////{
        ////    get
        ////    {
        ////        return (List<AccessRights>)HttpContext.Current.Session[Constant.AccessModule];
        ////    }

        ////    set
        ////    {
        ////        HttpContext.Current.Session[Constant.AccessModule] = value;
        ////    }
        ////}

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public static string ReturnUrl
        {
            get
            {
                return HttpContext.Current.Session[Constant.RETURNURL] == null ? string.Empty : Convert.ToString(HttpContext.Current.Session[Constant.RETURNURL]);
            }

            set
            {
                HttpContext.Current.Session[Constant.RETURNURL] = value;
            }
        }

        /// <summary>
        /// Gets the name of the user host.
        /// </summary>
        /// <value>
        /// The name of the user host.
        /// </value>
        public static string UserHostName => HttpContext.Current.Session[Constant.USERHOST] == null ? string.Empty : Convert.ToString(HttpContext.Current.Session[Constant.USERHOST]);

        /// <summary>
        /// Gets or sets the user operating system.
        /// </summary>
        /// <value>
        /// The user operating system.
        /// </value>
        public static string UserOS
        {
            get
            {
                return HttpContext.Current.Session[Constant.USEROPERATINGSYSTEM] == null ? string.Empty : Convert.ToString(HttpContext.Current.Session[Constant.USEROPERATINGSYSTEM]);
            }

            set
            {
                HttpContext.Current.Session[Constant.USEROPERATINGSYSTEM] = value;
            }
        }

        /////// <summary>
        /////// Gets or sets a value indicating whether [web version access].
        /////// </summary>
        /////// <value>
        ///////   <c>true</c> if [web version access]; otherwise, <c>false</c>.
        /////// </value>
        ////public static bool WebVersionAccess
        ////{
        ////    get
        ////    {
        ////        return Convert.ToBoolean(HttpContext.Current.Session[Constant.UserOperatingSystem]);
        ////    }

        ////    set
        ////    {
        ////        HttpContext.Current.Session[Constant.UserOperatingSystem] = value;
        ////    }
        ////}

        /////// <summary>
        /////// Gets or sets the web interface_ access error.
        /////// </summary>
        /////// <value>
        /////// The web interface_ access error.
        /////// </value>
        ////public static string WebInterfaceAccessError
        ////{
        ////    get
        ////    {
        ////        return HttpContext.Current.Session[Constant.WebInterfaceAccessError].ToString();
        ////    }

        ////    set
        ////    {
        ////        HttpContext.Current.Session[Constant.WebInterfaceAccessError] = value;
        ////    }
        ////}

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public static string Password
        {
            get
            {
                return !string.IsNullOrWhiteSpace(GlobalCacheProvider<string>.Instance.GetUserSpecificDetail(Constant.PASSWORD))
                    ?
                    GlobalCacheProvider<string>.Instance.GetUserSpecificDetail(Constant.PASSWORD)
                    :
                    HttpContext.Current.Session[Constant.PASSWORD].ToString();
            }

            set
            {
                GlobalCacheProvider<string>.Instance.SetUserSpecificDetail(Constant.PASSWORD, value);
                HttpContext.Current.Session[Constant.PASSWORD] = value;
            }
        }

        /// <summary>
        /// Removes the session.
        /// </summary>
        public static void ClearCache()
        {
            var loggedInUsers = GlobalCacheProvider<Dictionary<string, string>>.Instance.GetItem("LoggedInUsers", true);
            if (loggedInUsers != null && HttpContext.Current != null && loggedInUsers.ContainsKey(HttpContext.Current.Session.SessionID))
                loggedInUsers.Remove(HttpContext.Current.Session.SessionID);
            if (loggedInUsers != null)
                GlobalCacheProvider<Dictionary<string, string>>.Instance.AddItem("LoggedInUsers", loggedInUsers);
            GlobalCacheProvider<string>.Instance.RemoveUserSpecificDetail(Constant.USERNAME);
            GlobalCacheProvider<string>.Instance.RemoveUserSpecificDetail(Constant.PASSWORD);
            if (HttpContext.Current != null)
                HttpContext.Current.Session.Abandon();
        }

        /// <summary>
        /// Removes the session.
        /// </summary>
        /// <param name="sessionState">State of the session.</param>
        public static void ClearCache(HttpSessionState sessionState)
        {
            if (sessionState == null) return;
            var loggedInUsers = GlobalCacheProvider<Dictionary<string, string>>.Instance.GetItem("LoggedInUsers", true);
            if (loggedInUsers != null)
            {
                if (loggedInUsers.ContainsKey(sessionState.SessionID))
                {
                    loggedInUsers.Remove(sessionState.SessionID);
                }

                GlobalCacheProvider<Dictionary<string, string>>.Instance.AddItem("LoggedInUsers", loggedInUsers);
            }

            GlobalCacheProvider<string>.Instance.RemoveUserSpecificDetail(Constant.USERNAME, sessionState.SessionID);
            GlobalCacheProvider<string>.Instance.RemoveUserSpecificDetail(Constant.PASSWORD, sessionState.SessionID);
            sessionState.Abandon();
        }

        /// <summary>
        /// Determines whether [is already exist] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>If user already exists or not</returns>
        public static bool IsAlreadyExist(string userName)
        {
            var loggedInUsers = GlobalCacheProvider<Dictionary<string, string>>.Instance.GetItem("LoggedInUsers");
            return loggedInUsers != null && loggedInUsers.Any(c => c.Value.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Initiates the session.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public static void InitiateSession(string userName)
        {
            var loggedInUsers = GlobalCacheProvider<Dictionary<string, string>>.Instance.GetItem("LoggedInUsers", true) ??
                                new Dictionary<string, string>();

            loggedInUsers.Add(HttpContext.Current.Session.SessionID, userName);
            GlobalCacheProvider<Dictionary<string, string>>.Instance.AddItem("LoggedInUsers", loggedInUsers);
        }

        /// <summary>
        /// Gets all cache items.
        /// </summary>
        /// <returns>Get all cache items.</returns>
        public static string GetAllCacheItems()
        {
            var test = new List<dynamic>();
            foreach (var item in GlobalCacheProvider<MemoryCache>.Instance.GetAllCacheItems())
            {
                test.Add(new
                {
                    item.Key,
                    item.Value
                });
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(test);
        }
    }

}
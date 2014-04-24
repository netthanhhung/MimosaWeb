using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;


namespace MVCCore.Framework.Web
{
    public class FormsAuthenticationHelper
    {
        public static void SetAuthCookie(string userIdentifier, string[] roles, TimeSpan timeOut, bool createPersistentCookie)
        {
            SetAuthCookie(userIdentifier, roles, null, timeOut, createPersistentCookie);
        }

        static string BuildUserData(string[] roles, string[] permissions)
        {
            return string.Format("{0}|{1}",
                roles != null ? string.Join(",", roles) : string.Empty,
                permissions != null ? string.Join(",", permissions) : string.Empty
                ); // User-data, in this case the roles + permission list
        }

        public static List<string[]> ParseUserData(string userData)
        {
            var result = new List<string[]>(2);
            // parse user data from ticket to get role and permission
            var parts = userData.Split('|');
            result.Add(parts[0].Split(','));
            
            if (parts.Length > 1)
            {
                result.Add(parts[1].Split(','));
            }

            return result;

        }

        public static void SetAuthCookie(string userIdentifier, string[] roles, string[] permissions, TimeSpan timeOut, bool createPersistentCookie) 
        {
        
            var userData = BuildUserData(roles, permissions);

            var ticket = new FormsAuthenticationTicket(
                1, // Ticket version
                userIdentifier, // identifier (id or username) associated with ticket
                DateTime.Now, // Date/time issued
                DateTime.Now.Add(timeOut), // Date/time to expire
                createPersistentCookie, // "true" for a persistent user cookie
                userData,
                FormsAuthentication.FormsCookiePath);// Path cookie valid for

            // Encrypt the cookie using the machine key for secure transport
            var hash = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, // Name of auth cookie
                hash); // Hashed ticket

            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            // Set the cookie's expiration time to the tickets expiration time
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            // Add the cookie to the list for outgoing response
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        /// <summary>
        /// Redirect from Login page: timeOut = 30 minutes, defaultReturnUrl = ~/Default.aspx
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="roles"></param>
        /// <param name="permissions"></param>
        /// <param name="createPersistentCookie"></param>
        public static void SetAuthCookie(string userIdentifier, string[] roles, string[] permissions, bool createPersistentCookie = false)
        {
            SetAuthCookie(userIdentifier, roles, permissions, FormsAuthentication.Timeout, createPersistentCookie);
        }

        public static void SetAuthCookie(string userIdentifier, string[] roles, bool createPersistentCookie = false)
        {
            SetAuthCookie(userIdentifier, roles, null, FormsAuthentication.Timeout, createPersistentCookie);
        }

    }
}

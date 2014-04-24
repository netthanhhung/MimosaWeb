using System;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using MVCCore.Framework.Web;


namespace MVCCore.Framework.Security.Principal
{
    //http://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
    public class ApocadoPrincipal<TUser, TEnumPermission> : IPrincipal, IPermission where TEnumPermission : struct, IConvertible

    {
        protected virtual TEnumPermission[] Permisisons { get; set; }
        private string[] roles = new string[0];
        
        protected void Initialize(IIdentity identity, string[] userRoles, TEnumPermission[] permissions = null)
        {
            Identity = identity;
            roles = userRoles;
            Permisisons = permissions;
        }

        protected ApocadoPrincipal(IIdentity identity, string[] roles, TEnumPermission[] permissions = null)
        {
            Initialize(identity, roles, permissions);
        }

        public ApocadoPrincipal(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var data = FormsAuthenticationHelper.ParseUserData(ticket.UserData);
            
            Initialize(new FormsIdentity(ticket), data[0]);
        }

        protected ApocadoPrincipal()
        {
            
        }

        public virtual bool HasPermission(string permission)
        {
            if (ConfigurationManager.AppSettings["enablePermission"] == null)
                return true;

            if (Permisisons == null)
                return false;

            return Array.IndexOf(Permisisons, permission) >= 0;
        }

        public bool HasPermission(TEnumPermission permission)
        {
            return HasPermission(permission.ToString());
        }


        public bool IsInRole(string role)
        {
            return Array.IndexOf(roles, role) >= 0;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        /// <summary>
        /// get current user info from database using Func and cache this info for later access
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="getEntitiesMethod"></param>
        /// <returns></returns>
        protected TUser GetUserInfo(Func<long, TUser> getEntitiesMethod)
        {
            // check null
            if (HttpContext.Current.Cache[Identity.Name] == null)
            {
                // acquire lock
                // do not need to lock because of cache is threadsafe
                // lock (this)
                {
                    // recheck null after acquire lock
                    if (HttpContext.Current.Cache[Identity.Name] == null)
                    {
                        var userInfo = getEntitiesMethod.Invoke(int.Parse(Identity.Name));                        
                        if (userInfo != null)
                            HttpContext.Current.Cache.Insert(Identity.Name, userInfo, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
                    }
                        
                }
            }

            // if admin still null, sign out 
            if (HttpContext.Current.Cache[Identity.Name] == null)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Response.Redirect("~");
            }

            return (TUser)HttpContext.Current.Cache[Identity.Name];

        }

        protected void SetUserInfo(Func<long, TUser> getEntitiesMethod)
        {
            var userInfo = getEntitiesMethod.Invoke(int.Parse(Identity.Name));
            HttpContext.Current.Cache.Insert(Identity.Name, userInfo, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }
        

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            ClearPrincipalCache();
        }

        public void ClearPrincipalCache()
        {
            if (string.IsNullOrWhiteSpace(Identity.Name) == false)
                HttpContext.Current.Cache.Remove(Identity.Name);               
        }
        
    }
}

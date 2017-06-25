using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MEAS
{
    

    public static class AccountManager
    {
        public static Dictionary<string, string[]> roles;

       static AccountManager()
        {
            roles = new Dictionary<string, string[]>();
            roles["admin"] = new string[] { "1","2","3"};
            roles["user"] = new string[] { "4" ,"5"};
        }

        public static string[]  GetRoles(string name)
        {
            if (!roles.ContainsKey(name))
                return new string[] { };
            return roles[name];
        }
    }

    public class PermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Authorize(filterContext))
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.HeaderEncoding = filterContext.HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                filterContext.HttpContext.Response.Write("<h2>OoO，权限不足，请登陆</h2><script type=\"text/javascript\">setTimeout(function () { location.href = \"/account/login\" }, 2000);</script>");
                filterContext.HttpContext.Response.Flush();
                filterContext.HttpContext.Response.End();
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        public string Roles { get; set; }

        protected virtual bool Authorize(ActionExecutingContext context)
        {
          
           var cookies=  System.Web.HttpContext.Current.Request.Cookies;
            if (!cookies.AllKeys.Contains(CookieKeys.USERID))
                return false;
            var name = cookies[CookieKeys.USERID].Value;
            
            if (string.IsNullOrEmpty(name))
                return false;
            var roles = AccountManager.GetRoles(name) ;
            if (roles.Count() == 0)
                return false;
            string[] configRoles = string.IsNullOrEmpty(Roles)? new string[] { "1"}: Roles.Trim().Split(',');
            if (!configRoles.All(t => roles.Any(b => b == t)))
                return false;

            return true;
        }
 
    }

}
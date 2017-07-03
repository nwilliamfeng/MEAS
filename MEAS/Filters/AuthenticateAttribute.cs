using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace MEAS
{
    
    public class AuthenticateAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var identity = filterContext.Principal.Identity;
            if (!identity.IsAuthenticated)             
                filterContext.Result = new HttpUnauthorizedResult(); //注意此处必须赋值（任何actionresult类型），不能让result为null，否则OnAuthenticationChallenge不会执行
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                       { "action", "Unauthenticated" },
                                       { "controller", "Error" },
                                       { "returnUrl",filterContext.HttpContext.Request.RawUrl}
                                  });
                //filterContext.HttpContext.Response.Redirect(@"\Error\Unauthenticated");
            }
        
        }
    }
}
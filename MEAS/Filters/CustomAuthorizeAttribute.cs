using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MEAS
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            //不推荐抛出异常。https://stackoverflow.com/questions/17148554/asp-net-mvc-4-throw-httpexception-vs-return-httpstatuscoderesult
            if (context.HttpContext.Request.IsAuthenticated)
            {
                //throw new HttpException((int)System.Net.HttpStatusCode.Forbidden, "此操作没有权限！");
                //context.HttpContext.Response.Redirect(@"\Error\Unauthorized");

                //context.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary {
                //                       { "action", "ActionName" },
                //                       { "controller", "ControllerName" }
                //                  });
                //var ex = new HttpException((int)System.Net.HttpStatusCode.Forbidden, "此操作没有权限！");                           
                //context.Result = this.GetErrorPage(ex, context);

                context.Result = new RedirectToRouteResult(
                     new RouteValueDictionary {
                                       { "action", "Unauthorized" },
                                       { "controller", "Error" },
                                       { "returnUrl",context.HttpContext.Request.UrlReferrer}
                                   });

            }
            else
            {
                base.HandleUnauthorizedRequest(context);
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                       { "action", "Unauthenticated" },
                                       { "controller", "Error" },
                                       { "returnUrl",context.HttpContext.Request.RawUrl}
                                  });

                //context.HttpContext.Response.Redirect(@"\Error\Unauthenticated");
                //var exception = new HttpException((int)System.Net.HttpStatusCode.Unauthorized, "请重新登录。");
                //context.Result = this.GetErrorPage(exception, context);
                //throw new HttpException((int)System.Net.HttpStatusCode.Unauthorized, "未登录。");
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //to do... store sessiondata，log etc. see https://stackoverflow.com/questions/6860686/extend-authorizeattribute-override-authorizecore-or-onauthorization
            base.OnAuthorization(filterContext);
        }


        private ActionResult  GetErrorPage(Exception ex,AuthorizationContext context)
        {            
            HandleErrorInfo errorinfo = new HandleErrorInfo(ex, context.ActionDescriptor.ControllerDescriptor.ControllerName, context.ActionDescriptor.ActionName);
            return new ViewResult { ViewName = "Error", ViewData = new ViewDataDictionary(errorinfo) };
        }
    }
}
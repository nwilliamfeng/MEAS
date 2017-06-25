using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MEAS
{
    internal class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            // Set the response code to 403.
            context.HttpContext.Response.StatusCode = 403;
        }
    }
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAuthenticated)
            {
                var ex = new HttpException((int)System.Net.HttpStatusCode.Forbidden, "此操作没有权限！");
                context.Result = this.GetErrorPage(ex, context);
            }
            else
            {
                base.HandleUnauthorizedRequest(context);

                var exception = new HttpException((int)System.Net.HttpStatusCode.Unauthorized, "请重新登录。");
                context.Result = this.GetErrorPage(exception ,context );
            }
        }

        private ActionResult  GetErrorPage(Exception ex,AuthorizationContext context)
        {            
            HandleErrorInfo errorinfo = new HandleErrorInfo(ex, context.ActionDescriptor.ControllerDescriptor.ControllerName, context.ActionDescriptor.ActionName);
            return new ViewResult { ViewName = "Error", ViewData = new ViewDataDictionary(errorinfo) };
        }
    }
}
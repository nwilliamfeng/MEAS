﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MEAS.Controllers;

namespace MEAS
{
   
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            //不推荐抛出异常。https://stackoverflow.com/questions/17148554/asp-net-mvc-4-throw-httpexception-vs-return-httpstatuscoderesult
            if (context.HttpContext.Request.IsAuthenticated)
            {
               context.HttpContext.Response.Redirect(@"\Error\Unauthorized");

     
                //context.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary {
                //                       { "action", "ActionName" },
                //                       { "controller", "ControllerName" }
                //                  });


                //var ex = new HttpException((int)System.Net.HttpStatusCode.Forbidden, "此操作没有权限！");                           
                //context.Result = this.GetErrorPage(ex, context);

            }
            else
            {
                base.HandleUnauthorizedRequest(context);
                context.HttpContext.Response.Redirect(@"\Error\Unauthenticated");
                //var exception = new HttpException((int)System.Net.HttpStatusCode.Unauthorized, "请重新登录。");
                //context.Result = this.GetErrorPage(exception ,context );  
            }
        }

        private ActionResult  GetErrorPage(Exception ex,AuthorizationContext context)
        {            
            HandleErrorInfo errorinfo = new HandleErrorInfo(ex, context.ActionDescriptor.ControllerDescriptor.ControllerName, context.ActionDescriptor.ActionName);
            return new ViewResult { ViewName = "Error", ViewData = new ViewDataDictionary(errorinfo) };
        }
    }
}
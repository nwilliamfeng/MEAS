﻿using System.Web;
using System.Web.Mvc;
using Elmah;

namespace MEAS
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        //public override void OnException(ExceptionContext context)
        //{
        //    base.OnException(context);
        //    if (!context.ExceptionHandled       // if unhandled, will be logged anyhow
        //        || TryRaiseErrorSignal(context) // prefer signaling, if possible
        //        || IsFiltered(context))         // filtered?
        //        return;

        //    LogException(context);
        //}

        //private static bool TryRaiseErrorSignal(ExceptionContext context)
        //{
        //    var httpContext = GetHttpContextImpl(context.HttpContext);
        //    if (httpContext == null)
        //        return false;
        //    var signal = ErrorSignal.FromContext(httpContext);
        //    if (signal == null)
        //        return false;
        //    signal.Raise(context.Exception, httpContext);
        //    return true;
        //}

        //private static bool IsFiltered(ExceptionContext context)
        //{
        //    var config = context.HttpContext.GetSection("elmah/errorFilter")
        //                    as ErrorFilterConfiguration;

        //    if (config == null)
        //        return false;

        //    var testContext = new ErrorFilterModule.AssertionHelperContext(
        //                          context.Exception,
        //                          GetHttpContextImpl(context.HttpContext));
        //    return config.Assertion.Test(testContext);
        //}

        //private static void LogException(ExceptionContext context)
        //{
        //    var httpContext = GetHttpContextImpl(context.HttpContext);
        //    var error = new Error(context.Exception, httpContext);
        //    ErrorLog.GetDefault(httpContext).Log(error);
        //}

        //private static HttpContext GetHttpContextImpl(HttpContextBase context)
        //{
        //    return context.ApplicationInstance.Context;
        //}

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (!context.ExceptionHandled)
                return;
            var httpContext = context.HttpContext.ApplicationInstance.Context;
            var signal = ErrorSignal.FromContext(httpContext);
            signal.Raise(context.Exception, httpContext);
            
        }
    }
}
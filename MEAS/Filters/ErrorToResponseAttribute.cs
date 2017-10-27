using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace MEAS
{
    public class ErrorToResponseAttribute:FilterAttribute,IExceptionFilter
    {
        public  void OnException(ExceptionContext filterContext)
        {
            
            var response = filterContext.RequestContext.HttpContext.Response;
           
            response.Write(filterContext.Exception.Message);
            response.ContentType = MediaTypeNames.Text.Plain;
            filterContext.ExceptionHandled = true;

        }
    }
}
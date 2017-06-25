using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;


namespace MEAS
{
    public class MyAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
           var authorizor=  DependencyResolver.Current.GetService<IAuthorizeService>();

            return base.AuthorizeCore(httpContext);
        }
    }
}
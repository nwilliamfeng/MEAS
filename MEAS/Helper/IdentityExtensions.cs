using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using MEAS.Service;

namespace MEAS
{
    public static class IdentityExtensions
    {
        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
                return null;
           return DependencyResolver.Current.GetService<IAccountService>().GetUserName(identity.Name).Result;
        }
    }
}
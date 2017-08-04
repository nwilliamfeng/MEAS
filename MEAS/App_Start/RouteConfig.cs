using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MEAS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            http://www.cnblogs.com/weixing/p/3326188.html
            routes.MapRoute(
               "Measure", // 路由名称，这个只要保证在路由集合中唯一即可
               "Measure/{controller}/{action}/{id}", //路由规则,匹配以Admin开头的url
               new { controller = "TorqueWrench", action = "Index", id = UrlParameter.Optional } // 
           );
        }
    }
}

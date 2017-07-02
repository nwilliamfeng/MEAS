using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Models;
using System.Web.Mvc.Html;

namespace MEAS
{
    public static class MyHtmlHelper
    {
        public static MvcHtmlString PageLink(this HtmlHelper html, IPagingInfo pagingInfo,Func<int,string> pageUrl)
        {
            return null;
        }


        public static MvcForm BeginFormWithHorizontal(this HtmlHelper html, string actionName=null,string controllerName=null)
        {
            return html.BeginForm(actionName, controllerName, FormMethod.Post, new { @class = "form-horizontal" });
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEAS
{
    public static class ControllerExtension
    {
        const string RefferUrl = "userProfileReturnUrl";
        private static string GetJumpUrlFromTempData(this Controller  controller)
        {
            if (!controller.TempData.ContainsKey(RefferUrl))
                return null;
            return controller.TempData[RefferUrl]?.ToString();
        }

     
       

        public static void SaveUrlRefferUrlToTempData(this Controller controller)
        {
            if (controller.Request.UrlReferrer!=null)
                controller.TempData[RefferUrl] = controller.Request.UrlReferrer;
        }
    }
}
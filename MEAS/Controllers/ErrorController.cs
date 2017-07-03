using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEAS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  
            return View();
        }

        public ViewResult Unauthenticated(string returnUrl)
        {            
            Response.StatusCode = 401;
            if (!string.IsNullOrEmpty(returnUrl))
                ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ViewResult Unauthorized( )
        {
            Response.StatusCode = 403;
      
            return View() ;
        }
    }
}
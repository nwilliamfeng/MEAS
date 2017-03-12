using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class NavController : Controller
    {
        

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Menu()
        {
            return PartialView(new string[] { "aa","bb"});
        }

     
    }
}
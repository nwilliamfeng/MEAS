using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class HomeController : Controller
    {
       

        public HomeController( )
        {
         
            Console.WriteLine(System.Configuration.ConfigurationManager.ConnectionStrings);
        }

        public ActionResult Demo()
        {
            return Content(""); 
       
        }

        public ActionResult Index()
        {
            // ViewBag.Text = @"<H><H2>abcded</H2></H>"; 
       
            ViewBag.Text = @"This text contains<br />a line break";
            return View();
        }

        [Throttle(Name = "Temp", Message = "You must wait {n} seconds before accessing this url again.", Seconds = 5)]
        public ActionResult Temp()
        {
            return Content("temp executed "+DateTime.Now .ToLongTimeString());
        }
       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(); 
        }

       
    }
}
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

        
        public ActionResult Index()
        {               
            ViewBag.Text = @"This text contains<br />a line break";
        
            return View();
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
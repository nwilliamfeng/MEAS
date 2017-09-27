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
        
       
       

        public PartialViewResult Menu(string category)
        {
            ViewBag.SelectedCategory = category;
            return PartialView(new string[] { "Chess","Soccer","Waterports"});
        }

     
    }
}
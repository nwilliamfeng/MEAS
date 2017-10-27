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
  

        public ActionResult Index()
        {
            return View();
        }

      
        [ErrorToResponse]
       [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]       
        public ActionResult GetJsonResults(string name,int age)
        {
           
            System.Diagnostics.Debug.WriteLine("dd"+this.Request.IsAjaxRequest());
            for (int i=0;i< this.Request.Headers.Count;i++)
                System.Diagnostics.Debug.WriteLine("the key :"+this.Request.Headers.GetKey(i)+", vlaue :"+  this.Request.Headers[i]); 
            return this.Json(new string[] { "tom","jack","mary"},JsonRequestBehavior.AllowGet);
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
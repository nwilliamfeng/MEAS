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

        public ActionResult GetKeywords(string content)
        {
            var data = new object[] { new { id = 1, name = content + "1" }, new { id = 2, name = content + "2" }, new { id = 3, name = content + "3" }};
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult OutputTable()
        {
            
            var data = new object[] { new {id=1, fileName = "abc", size = "260k" }, new { id = 2, fileName = "cdef", size = "1200k" }, new { id = 3, fileName = "kghf", size = "4500k" } };
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
     
 
        public ActionResult About()
        {
         
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(); 
        }

       
    }
}
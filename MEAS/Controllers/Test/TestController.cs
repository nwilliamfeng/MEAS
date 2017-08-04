using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MEAS.Models;
using System.Web;
using System.Web.Security;
using MEAS.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MEAS.Controllers
{
    public class TestController : Controller
    {
 
        public TestController( )
        {
        
        }

       

        public ActionResult TestJson()
        {
         
            LoginViewModel obj = new LoginViewModel { UserName = "fw", Password = "aaa" };
      
            return this.Json(obj,JsonRequestBehavior.AllowGet);

        }

       
  
       
    }
}
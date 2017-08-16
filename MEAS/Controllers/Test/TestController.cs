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
using System.Web.Routing;

namespace MEAS.Controllers
{
   
    public class TestController : Controller
    {
 
        public TestController( )
        {
           
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
    
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        protected override IActionInvoker CreateActionInvoker()
        {
            return base.CreateActionInvoker();
        }





     //   [AllowAnonymous]
        [CustomAuthorize(Roles ="7,8,9")]
        public ActionResult Test()
        {

            
            Console.WriteLine(this.Request); 
            return Content("finish " );
        }

        public ActionResult TestJson()
        {

            var obj = new { id = 123, name = "tom cluse", time = DateTime.Now };
            JsonResult result = new Controllers.JsonResult { Data = obj, Result = true };
            return new JsonNetResult { Data = result };

        }

       
  
       
    }

    public class JsonResult
    {
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
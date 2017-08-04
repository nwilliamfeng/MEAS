using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PagedList;
using MEAS.Service;


namespace MEAS.Controllers.Torque
{
   
    
    public class  TorqueWrenchController : Controller
    {
        private ITorqueWrenchMeasureTestService _testService;

        public TorqueWrenchController(ITorqueWrenchMeasureTestService testService)
        {
            this._testService = testService;
        }
 
       public async Task<ActionResult> Index()
        {
            return Content("abc");
        }

       

 
    }
}
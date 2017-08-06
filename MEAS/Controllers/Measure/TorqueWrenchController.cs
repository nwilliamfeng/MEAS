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

 //   [RoutePrefix("measure")]
    
    public class  TorqueWrenchController : Controller
    {
        private ITorqueWrenchMeasureTestService _testService;

        public TorqueWrenchController(ITorqueWrenchMeasureTestService testService)
        {
            this._testService = testService;
        }
 
        [Route("measure/torquewrench")]
       public async Task<ActionResult> Index()
        {
            return Content("abc");
        }

        [Route("torque/wrench/test/")]
        public async Task<ActionResult> Test(int id)
        {
            return Content("abc");
        }


    }
}
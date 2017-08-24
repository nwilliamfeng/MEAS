using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PagedList;
using MEAS.Service;


namespace MEAS.Controllers
{

    [RoutePrefix("torque/wrench")]
    public class  WrenchController : Controller
    {
        private ITorqueWrenchMeasureService _measureService;

        public WrenchController(ITorqueWrenchMeasureService measureService)
        {
            this._measureService = measureService;
        }
 
       

         [Route("test")]
        public async Task<ActionResult> Test(int id)
        {
            return Content("abc");
        }

    
        [Route()]
        public async Task<ActionResult> Index(int? page)
        {
            var pageNum = page ?? 1;
            var start = new DateTime(2016, 1, 1);
            var end = new DateTime(2017,1, 1);
            var sr = await this._measureService.Find(start,end,3,pageNum-1);
            ViewBag.OnePageResult = new StaticPagedList<TorqueWrenchMeasure>(sr.Data, pageNum, 3, sr.TotalCount);
            return View();

        }

        public async Task<ActionResult> Delete(int id)
        {
            var wrench = await this._measureService.FindWithId(id);
            return PartialView("_Delete", wrench);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var result = await this._measureService.Delete(id);
            return Json(new { success = true });
        }

        //public async Task<ActionResult> Edit(int id)
        //{
        //    var product = await this._productService.FindWithId(id);
        //    return View(product);
        //}


        //[HttpPost]
        //public async Task<ActionResult> Edit(Product product)
        //{
        //    if (this.ModelState.IsValid)
        //    {

        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}

    }
}
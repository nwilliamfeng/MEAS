using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEAS.Controllers
{
    public class OutputCacheController : Controller
    {
        // GET: OutputCache
        public ActionResult Index()
        {
            return this.RedirectToAction("Time");  
        }

        //测试缓存
        [OutputCache(Duration = 30, VaryByParam = "None", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Time()
        {
            return Content(DateTime.Now.ToLongTimeString());
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoveCache()
        {
            var url = Url.Action("Time", "OutputCache");
            HttpResponse.RemoveOutputCacheItem(url);
            return Content(string.Format("Clear Output Cache by Url {0} Success!", url));
        }

        /// <summary>
        /// 带参数的缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult TimeWithParam(int id)
        {
            return Content(DateTime.Now.ToLongTimeString() + "   and id is " + id.ToString());
        }

        /// <summary>
        /// 移除带参数的缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveCacheByParam(int id)
        {
            var url = Url.Action("TimeWithParam", "OutputCache", new { id = id });
            HttpResponse.RemoveOutputCacheItem(url);
            return Content(string.Format("Clear Output Cache by Url {0} Success!", url));
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult DonutHolecaching()
        {
            // Get categories list from the database and // pass it to the child view ViewBag.Categories = GetCategories(); 
            return View(); 
        }
    }
}
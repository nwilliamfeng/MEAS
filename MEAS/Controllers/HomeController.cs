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
            return View(GetPageCategories());
        }



        public ActionResult Jump(int pageId = 0)
        {
            var page = this.GetPageCategories().SelectMany(x => x.Pages).FirstOrDefault(x => x.Id == pageId);
            if (page == null)
            {
                if (this.Request.UrlReferrer != null)
                    return Redirect(this.Request.UrlReferrer.ToString());
                else
                    return RedirectToAction("Index");
            }     
            var url = "http://" + this.Request.Url.Authority + "/" + page.RelativeUrl;
            return Redirect(url);
        }


        private IEnumerable<PageCategory> GetPageCategories()
        {
            PageCategory category1 = new PageCategory { Id = 1, Title = "Category1" };
            Page page1 = new Page { Id = 1, Title = "page1", Category = category1, RelativeUrl = "Product" };
            Page page2 = new Page { Id = 2, Title = "page2", Category = category1, RelativeUrl = @"Product/page2" };
            category1.Pages.Add(page1);
            category1.Pages.Add(page2);
            yield return category1;
            PageCategory category2 = new PageCategory { Id = 2, Title = "Category2" };
            Page page3 = new Page { Id = 3, Title = "page3", Category = category2, RelativeUrl = @"Product/page3" };
            Page page4 = new Page { Id = 4, Title = "page4", Category = category2, RelativeUrl = @"Product/page4" };
            category2.Pages.Add(page3);
            category2.Pages.Add(page4);
            yield return category2;
            PageCategory category3 = new PageCategory { Id = 3, Title = "Category3" };
            Page page5 = new Page { Id = 5, Title = "page5", Category = category3, RelativeUrl = @"Product/page5" };

            category3.Pages.Add(page5);
            yield return category3;
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
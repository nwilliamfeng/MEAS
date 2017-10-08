using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEAS.Controllers
{
 
    public class NavigationController:Controller
    {     
        private const string SELECT_PAGE_ID = "selectPageId";

        public ActionResult Index()
        {
            var rd = this.HttpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.Values["area"] as string;

            ViewBag.SelectPageId = this.Request.Cookies[SELECT_PAGE_ID] == null ? 0 : int.Parse(this.Request.Cookies[SELECT_PAGE_ID].Value.ToString());
            return PartialView("_NavigationPartial", GetPageCategories());
        }


        public ActionResult Jump(int pageId = 0)
        {
            var page = this.GetPageCategories().SelectMany(x => x.Pages).FirstOrDefault(x => x.Id == pageId);
            if (page == null)
            {
                this.Response.Cookies[SELECT_PAGE_ID].Value = null;
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            if (!this.Request.Cookies.AllKeys.Contains(SELECT_PAGE_ID))
                this.Response.Cookies.Set(new HttpCookie(SELECT_PAGE_ID) { Expires = DateTime.Now.AddDays(1) });
            this.Response.Cookies[SELECT_PAGE_ID].Value = pageId.ToString();
            return RedirectToAction(page.ActionName, page.ControllerName);
        }



        private IEnumerable<PageCategory> GetPageCategories()
        {
            PageCategory category1 = new PageCategory { Id = 1, Title = "Category1" };
            Page page1 = new Page { Id = 1, Title = "page1", Category = category1, ControllerName = "Product", ActionName = "Index" };
            Page page2 = new Page { Id = 2, Title = "page2", Category = category1, ControllerName = "Product", ActionName = "Page2" };
            category1.Pages.Add(page1);
            category1.Pages.Add(page2);
            yield return category1;
            PageCategory category2 = new PageCategory { Id = 2, Title = "Category2" };
            Page page3 = new Page { Id = 3, Title = "page3", Category = category2, ControllerName = "Product", ActionName = "Page3" };
            Page page4 = new Page { Id = 4, Title = "page4", Category = category2, ControllerName = "Product", ActionName = "Page4" };
            category2.Pages.Add(page3);
            category2.Pages.Add(page4);
            yield return category2;
            PageCategory category3 = new PageCategory { Id = 3, Title = "Category3" };
            Page page5 = new Page { Id = 5, Title = "page5", Category = category3, ControllerName = "Product", ActionName = "Page5" };

            category3.Pages.Add(page5);
            yield return category3;
        }

        //private IEnumerable<PageCategory> GetPageCategories()
        //{
        //    PageCategory category1 = new PageCategory {Id=1, Title="Category1"};
        //    Page page1 = new Page { Id=1, Title = "page1", Category = category1, RelativeUrl = "Product" };
        //    Page page2 = new Page {Id=2, Title = "page2", Category = category1, RelativeUrl = @"Product/page2" };           
        //    category1.Pages.Add(page1);
        //    category1.Pages.Add(page2);
        //    yield return category1;
        //    PageCategory category2 = new PageCategory { Id = 2, Title = "Category2" };
        //    Page page3 = new Page {Id=3, Title = "page3", Category = category2, RelativeUrl = @"Product/page3" };
        //    Page page4 = new Page {Id=4, Title = "page4", Category = category2, RelativeUrl = @"Product/page4" };
        //    category2.Pages.Add(page3);
        //    category2.Pages.Add(page4);
        //    yield return category2;
        //    PageCategory category3 = new PageCategory { Id = 3, Title = "Category3" };
        //    Page page5 = new Page {Id=5, Title = "page5", Category = category3, RelativeUrl = @"Product/page5" };

        //    category3.Pages.Add(page5);
        //    yield return category3;
        //}
    }
}
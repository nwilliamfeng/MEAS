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
            string currentAction = rd.GetRequiredString("action")?.ToLower();
            string currentController = rd.GetRequiredString("controller")?.ToLower();
            string currentArea = rd.Values["area"] as string;
            
           var page= this.GetPageCategories()
                .SelectMany(x => x.Pages)
                .FirstOrDefault(x =>( x.Action==currentAction || x.RedirectActionName==currentAction)  && x.Controller==currentController );

            ViewBag.SelectPageId = page == null ? 0 : page.Id;
          //  ViewBag.SelectPageId = this.Request.Cookies[SELECT_PAGE_ID] == null ? 0 : int.Parse(this.Request.Cookies[SELECT_PAGE_ID].Value.ToString());
            return PartialView("_NavigationPartial", GetPageCategories());
        }


     

        private IEnumerable<PageCategory> GetPageCategories()
        {
            PageCategory category1 = new PageCategory { Id = 1, Title = "Category1" };
            Page page1 = new Page { Id = 1, Title = "prodcut", Category = category1, Controller = "product", Action = "index",RedirectActionName="list" };
            Page page2 = new Page { Id = 2, Title = "page2", Category = category1, Controller = "product", Action = "page2" };
            category1.Pages.Add(page1);
            category1.Pages.Add(page2);
            yield return category1;
            PageCategory category2 = new PageCategory { Id = 2, Title = "Category2" };
            Page page3 = new Page { Id = 3, Title = "page3", Category = category2, Controller = "product", Action = "page3" };
            Page page4 = new Page { Id = 4, Title = "page4", Category = category2, Controller = "product", Action = "page4" };
            category2.Pages.Add(page3);
            category2.Pages.Add(page4);
            yield return category2;
            PageCategory category3 = new PageCategory { Id = 3, Title = "Category3" };
            Page page5 = new Page { Id = 5, Title = "page5", Category = category3, Controller = "product", Action = "page5" };
            Page page6 = new Page { Id = 6, Title = "wrench", Category = category3, Controller = "wrench", Action = "index" };
            category3.Pages.Add(page5);
            category3.Pages.Add(page6);
            yield return category3;
            for (int i = 4; i < 50; i++)
            {
                PageCategory d = new PageCategory { Id = 1, Title = "Category" + i.ToString() };
                Page a = new Page { Id = 1, Title = "prodcut", Category = d, Controller = "product", Action = "index", RedirectActionName = "list" };
                Page b = new Page { Id = 2, Title = "page2", Category = d, Controller = "product", Action = "page2" };
                d.Pages.Add(a);
                d.Pages.Add(b);
                yield return d;
            }
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
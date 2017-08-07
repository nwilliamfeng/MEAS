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
   
    [Authenticate]
    public class AdminController : Controller
    {
        private IProductService _productService;

        public AdminController(IProductService productService)
        {
            this._productService = productService;
        }
    

        [CustomAuthorize(Roles ="1,2,3")]
        public async Task<ActionResult> Index(int? page)
        {
            var pageNum = page ?? 1;
            var products = await this._productService.FindWithCategory(null);
         //   ViewBag.OnePageResult = products.ToPagedList(pageNum,10);
            return View(products.ToPagedList(pageNum, 10));
        
        }

        public async Task<ActionResult> Delete(int id)
        {

            //return View(products);
            //   var result= await this._productService.Delete(id);

            //      return this.Redirect(this.Request.UrlReferrer.ToString());
           var product=  await this._productService.FindWithId(id);
            return PartialView("_DeleteProduct",product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var result = await this._productService.Delete(id);
            return Json(new { success = true });
        }

        public async Task<ActionResult> Edit(int id)
        {
            var product = await this._productService.FindWithId(id);
            return View(product);
        }

      
        [HttpPost]
        public async Task<ActionResult> Edit(Product product)
        {
            if (this.ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }
         
            return View(product);
        }

 
    }
}
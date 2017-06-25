using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MEAS.Data;
using PagedList;


namespace MEAS.Controllers
{
  
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;

        public AdminController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
    

       [CustomAuthorize(Roles ="1,2,3")]
        public async Task<ActionResult> Index(int? page)
        {
            Console.WriteLine(this.Request); 
            var pageNum = page ?? 1;
            var products = await this._productRepository.LoadAll();
         //   ViewBag.OnePageResult = products.ToPagedList(pageNum,10);
            return View(products.ToPagedList(pageNum, 10));
        }


        [Permission(Roles = "1,2,3,4")]
        public async Task<ActionResult> Index2(int? page)
        {
            var pageNum = page ?? 1;
            var products = await this._productRepository.LoadAll();
            //   ViewBag.OnePageResult = products.ToPagedList(pageNum,10);
            return View(products.ToPagedList(pageNum, 10));
        }

        public async Task<ActionResult> Delete(int id)
        {
            var products = await this._productRepository.LoadAll();
            return View(products);
        }

        public async Task<ActionResult> Edit(int productId)
        {
            var product = await this._productRepository.FindWithId(productId);
            return View(product);
        }

        public ActionResult Cols()
        {
            return View();  
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


        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
            
        }
    }
}
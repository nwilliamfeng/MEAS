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
   
    [Authenticate]
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

        public ActionResult DownLoad(long fileId)
        {
            //https://stackoverflow.com/questions/5826649/returning-a-file-to-view-download-in-asp-net-mvc
            string[] extensions = new string[] { "pdf","png","jpg"};
            return new FileStreamResult(new System.IO.MemoryStream(document.Data), document.ContentType);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = document.FileName,
                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(document.Data, document.ContentType);
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
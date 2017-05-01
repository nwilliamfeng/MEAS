using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MEAS.Data;

namespace MEAS.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;

        public AdminController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
    
        public async Task<ActionResult> Index()
        {
            var products = await this._productRepository.LoadAll();
            return View(products);
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
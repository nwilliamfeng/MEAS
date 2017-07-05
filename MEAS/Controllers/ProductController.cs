using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MEAS.Service;
using MEAS.Models;

namespace MEAS.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _service;
        private int PageSize { get; set; } = 3;

        public ProductController(IProductService service)
        {
            this._service = service;
        }

      
        public async Task<ActionResult> Index()
        {
            this.TempData["key"] = "111";
            var category = "Chess";
            
            return this.RedirectToAction("List", new {  category }); 
        }

        public async Task<ActionResult> List(string category,int page=1 )
        {
     
            if (category == null)
                return this.RedirectToAction("Index", "Home");
            var value =TempData["key"];
            Console.WriteLine(value);
            var results = await this._service.FindWithCategory(category);
            var vm = new PagingResult<Product> { Values = results.Skip(PageSize * (page - 1)).Take(PageSize), CurrentPage = page, ItemsPerPage = PageSize, TotalItems = results.Count() };
         
          
            return View(vm);
        }

     
    }
}
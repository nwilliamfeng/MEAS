using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class CartController : Controller
    {
        private IProductService _productService;
        public CartController(IProductService productService)
        {
            this._productService = productService;
        }
       
        public ActionResult AddToCart(int productId,string returnUrl)
        {
            var product =this._productService.
            return View();
        }
    }
}
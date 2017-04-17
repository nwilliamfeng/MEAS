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
    public class CartController : Controller
    {
        private IProductService _productService;
        public CartController(IProductService productService)
        {
            this._productService = productService;
        }

        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }
     
       
        public async Task<ActionResult> AddToCart(Cart cart,int id,string returnUrl)
        {
            var product = await this._productService.FindWithId(id);
            cart.AddLine(product, 1);
            return this.RedirectToAction("Index",new { returnUrl});
        }

      
        public async Task<ActionResult> RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            var product = await this._productService.FindWithId(productId);
            cart.RemoveLine(product);
            return this.RedirectToAction("Index", new { returnUrl});
        }


        public  ActionResult  Summary(Cart cart)
        {
            return PartialView(cart);
        }
    
    }
}
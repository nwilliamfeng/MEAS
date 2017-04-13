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

        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = this.GetCart(), ReturnUrl = returnUrl });
        }
     
       
        public async Task<ActionResult> AddToCart(int id,string returnUrl)
        {
            var product = await this._productService.FindWithId(id);
            this.GetCart().AddLine(product, 1);
            return this.RedirectToAction("Index",new { returnUrl});
        }

        public async Task<ActionResult> RemoveFromCart(int productId, string returnUrl)
        {
            var product = await this._productService.FindWithId(productId);
            this.GetCart().RemoveLine(product);
            return this.RedirectToAction("Index", new { returnUrl});
        }

        private Cart GetCart()
        {
            var cart = this.Session["Cart"] as Cart;
            if (cart == null)
                cart = new Cart();
            this.Session["Cart"] = cart;
            return cart;
        }
    }
}
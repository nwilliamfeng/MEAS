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
        private IOrderProcessor _orderProcessor;

        public CartController(IProductService productService,IOrderProcessor orderProcessor)
        {
            this._productService = productService;
            this._orderProcessor = orderProcessor;
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


        public ActionResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ActionResult CheckOut()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult CheckOut(Cart cart ,ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
                this.ModelState.AddModelError("","Sorry ,your cart is empty");
            if (this.ModelState.IsValid)
            {
                this._orderProcessor.ProcessOrder(cart, shippingDetails);
                return View("");
            }
            else
                return View(shippingDetails);
        }
    
    }
}
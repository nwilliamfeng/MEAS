using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MEAS.Binder
{
    /// <summary>
    /// 模型绑定器，Page190
    /// </summary>
    public class CartBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string sessionkey = "Cart";
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
                cart = controllerContext.HttpContext.Session[sessionkey] as Cart; //第一次调用为空，必须new一个cart
            if(cart==null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionkey] = cart;
            }
            return cart;
               
        }
    }
}
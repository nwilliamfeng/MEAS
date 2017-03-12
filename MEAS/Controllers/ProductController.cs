using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _service;

        public ProductController(IProductService service)
        {
            this._service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List( )
        {
            return View(this._service.Find("model1"));
        }

     
    }
}
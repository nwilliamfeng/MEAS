using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class HomeController : Controller
    {
        private IManufacturerService _service;

        public HomeController(IManufacturerService service)
        {
            this._service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page." + this._service.Find("snap-on").Name;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
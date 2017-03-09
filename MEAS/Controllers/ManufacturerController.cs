using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class ManufacturerController : Controller
    {
        private IManufacturerService _service;

        public ManufacturerController(IManufacturerService service)
        {
            this._service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page,string name)
        {
            //  ViewBag.Message = "sdadsfasdf" + this._service.Find("snap-on " +page.ToString()+"   "+name).Name ;
            Manufacturer cp = this._service.Find(string.Format("the company:{0}{1}", page, name));
            return View(cp);
        }

     
    }
}
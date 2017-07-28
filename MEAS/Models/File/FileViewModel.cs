using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEAS
{
    public class FileViewModel
    {
        [HttpPostedFileExtensions(Extensions = "txt,png,gif,jpg")]
        public HttpPostedFileBase File { get; set; }

    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace MEAS.Controllers
{
    public class FileController : Controller
    {
        private static List<FileDocument> files;

        public FileController()
        {
            if (files == null)
            {
                files = new List<FileDocument>();
                long idx = 1;
                var dir = @"e:\files";
                if (Directory.Exists(dir))
                    foreach (var fileName in Directory.GetFiles(dir))
                    {
                        var fi = new FileInfo(fileName);
                        var fd = new FileDocument { Id = idx, Data = fi.ToBytes(), FileName = fi.Name, ContentType = fi.GetMime() };
                        idx++;
                        files.Add(fd);
                    }
            }
        }
   
     

        public ActionResult Download(long id)
        {
            //https://stackoverflow.com/questions/5826649/returning-a-file-to-view-download-in-asp-net-mvc

            var document = files.FirstOrDefault(x => x.Id == id);
            if (document == null)
                return Content("不存在指定id的文件");
            //通过浏览器打开或下载，通过FileStreamResult返回的话如果contenttype浏览器不支持打开，则转成下载，并且文件名称变成“下载”
            //    return new FileStreamResult(new System.IO.MemoryStream(document.Data), document.ContentType);

            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = document.FileName,
                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(document.Data, document.ContentType); 
        }


        //[HttpPost]  
        //public ActionResult Upload()
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        var file = Request.Files[0];
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(file.FileName);
        //            //var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
        //            var dir = @"e:\files";
        //            if (Directory.Exists(dir))
        //                Directory.CreateDirectory(dir);
        //            var path = Path.Combine(dir, fileName);
        //            file.SaveAs(path);
        //            return Content("文件上传成功!");
        //        }
        //    }
        //    return Content("文件上传失败!");
        //}

        [HttpPost]
        public ActionResult Upload(FileViewModel vm,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(vm.File.FileName);
                var dir = @"e:\files";
                if (Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var path = Path.Combine(dir, fileName);
                vm.File.SaveAs(path);
                return Content("文件上传成功!");
            }
            return Redirect(returnUrl);

            
        }

        [ChildActionOnly]
        public ActionResult UploadFile()
        {
            ViewBag.ReturnUrl = this.Request.FilePath;
            return View();
        }

    }
}
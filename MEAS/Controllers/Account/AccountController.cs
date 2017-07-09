using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MEAS.Models;
using System.Web;
using System.Web.Security;
using System.Security.Claims;
using MEAS.Service;

namespace MEAS.Controllers
{
    public class AccountController : Controller
    {

        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();        
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{

        //    Console.WriteLine(this.User);
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var result = this.Authorize(model.UserName,model.Password);
        //    if (result)
        //    {   
        //        HttpCookie cookie = new HttpCookie(CookieKeys.USERID);
        //        cookie.Value = model.UserName;
        //        cookie.Expires = DateTime.Now.AddMinutes(3);

        //        this.Response.Cookies.Add(cookie);
        //        //  this.HttpContext.Response.Cookies[CookieKeys.USERID].Value = model.UserName;
        //        //    this.HttpContext.Response.Cookies[CookieKeys.TOKEN].Value = model.Password;
        //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddDays(1), false, model.Password);

        //        //string encTicket = FormsAuthentication.Encrypt(authTicket);
        //       // var logincookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

        //     //   logincookie.Expires = DateTime.Now.AddDays(1);

        //        return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        //    }

        //    this.AddError("错误的用户名或密码！");

        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {            
            if (!ModelState.IsValid)
                return View();
            var user =await  this._accountService.Find(model.UserName, model.Password);
            if (user!=null)
            {
                var rolestr = string.Join(",", user.Roles);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddSeconds(10), true, rolestr);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = ticket.Expiration;
                cookie.HttpOnly = true;
                this.Response.Cookies.Add(cookie);
                await this._accountService.UpdateLogin(user);

         
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            this.AddError("错误的用户名或密码！");
            return View();
        }


        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            //不能用for，全部清空会报outofmemory异常
            //for (int i = 0; i < Request.Cookies.Count; i++)
            //{
            //    HttpCookie cookie = new HttpCookie(Request.Cookies[i].Name);
            //    cookie.Expires = DateTime.Now;//cookie将马上过期
            //    this.Response.Cookies.Add(cookie);
            //}
            //HttpCookie cookie = new HttpCookie(CookieKeys.USERID); 
            //cookie.Expires = DateTime.Now.AddDays(-1);//cookie将马上过期
            //this.Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
             
        }

    
        private void AddError(string error)
        {
            ModelState.AddModelError("", error);
        }


       
    }
}
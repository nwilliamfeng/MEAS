using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MEAS.Models;
using System.Web;
using System.Web.Security;
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

        public ActionResult Login(string returnUrl)
        {
            if (this.User.Identity.IsAuthenticated) //如果已经登录成功，则返回到指定的页面
                return this.RedirectToAction("Index","Home"); //之后会移到个人页面
            
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
                var mins = FormsAuthentication.Timeout.TotalMinutes;
                //if (mins < 5)
                //    mins = 60;
                mins = 1;
                var timeout = DateTime.Now.AddMinutes(mins); //从webconfig配置文件获取
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, timeout, true, rolestr);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = ticket.Expiration;
                cookie.HttpOnly = true;
                this.Response.Cookies.Add(cookie);
                await this._accountService.UpdateLogin(user);
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            this.ModelState.AddModelError(string.Empty ,"错误的用户名或密码！");
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

        [Authenticate]
        [CustomAuthorize(Roles = "1,2,3")]
        public ActionResult ResetPassword()
        {       
            return View(new ResetPasswordViewModel {  LoginName= User.Identity.Name, UserName=User.Identity.GetUserName()});
            
        }

        [Authenticate]
        [CustomAuthorize(Roles ="1,2,3")]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            var user =await this._accountService.Find(resetPassword.LoginName ,resetPassword.OldPassword);
             if(user==null )
                this.ModelState.AddModelError(string.Empty, "输入的旧密码错误。");
            if (!this.ModelState.IsValid)
                return RedirectToAction("ResetPassword");
            return RedirectToAction("LogOut"); 
        }

        [RequireRequestValue("idx")]
        public ActionResult GetItem(int idx)
        {
            return Content(idx.ToString());
        }

        [RequireRequestValue("name")]
        public ActionResult GetItem(string name)
        {
            return Content(name);
        }
    
  
       
    }
}
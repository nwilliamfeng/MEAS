using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MEAS.Models;
using System.Web;
using System.Web.Security;
using AutoMapper;
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
            if (this.HttpContext.Request.UrlReferrer == null)
            {
                return this.RedirectToAction("Index","Home");
            }
            if (this.User.Identity.IsAuthenticated) //如果已经登录成功，则返回到指定的页面
                return this.RedirectToAction("Index","Home"); //之后会移到个人页面
            
            ViewBag.ReturnUrl = returnUrl;
            
     
            return this.PartialView("_Login");
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
                return PartialView("_Login");
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
                return Json(new { success = true });
                //  return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                
            }
            this.ModelState.AddModelError(string.Empty ,"错误的用户名或密码！");
            return PartialView("_Login");
    
        }

  
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Authenticate]
        public ActionResult LogOut()
        {       
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
            
        }

        [Authenticate]
        public async Task<ActionResult> UserProfile()
        {
       
            this.SaveUrlRefferUrlToTempData();
           var user=await this._accountService.GetCurrentUser();
            if (user == null)
                throw new InvalidOperationException("无法找到当前用户。");
            var vm = Mapper.Map<UserInfoViewModel>(user);
            return View(vm);
        }

        [Authenticate]
        [HttpPost]
        public ActionResult UserProfile(UserInfoViewModel user)
        {

            if (!this.ModelState.IsValid)
                return View();
            //if (!this.TempData.ContainsKey("userProfileReturnUrl"))
            //    return RedirectToAction("Index","Home");     //跳转逻辑暂时不用，之后由js完成

            //return this.Redirect(this.TempData["userProfileReturnUrl"].ToString());
            return Content("保存成功");
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
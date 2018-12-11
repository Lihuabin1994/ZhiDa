using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhiDaCore.Appliaction;
using ZhiDaCore.Application;
using ZhiDaCore.Entity.Identity;
using ZhiDaCore.IService;

namespace ZhiDaCore.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private IHttpContextAccessor _accessor;
        public UserController(IUserService userService, IHttpContextAccessor accessor)
        {
            _userService = userService;
            _accessor = accessor;

        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        { //验证模型是否正确
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (await _userService.GetUser(user.Account, user.Password))
            { 
                //加密用户名写入cookie中，AdminAuthorizeAttribute特性标记取出cookie并解码除用户名
                var encryptValue = _userService.LoginEncrypt(user.Account, ApplicationKeys.User_Cookie_Encryption_Key);
                HttpContext.Response.Cookies.Append(ApplicationKeys.User_Cookie_Key, encryptValue, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60) });
                Logger.Info("Access ip " + HttpContext.Connection.RemoteIpAddress.ToString());
                return Redirect("/Trade/Index");
            }
            else
            {
                return View(user);
            }
        }
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(ApplicationKeys.User_Cookie_Key);
            return Redirect(CookiesAdmin.LoginUrl);
        }
    }
}
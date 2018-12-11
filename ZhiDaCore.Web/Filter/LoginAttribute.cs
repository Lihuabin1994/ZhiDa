using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ZhiDaCore.Appliaction;
using ZhiDaCore.Application;

namespace ZhiDaCore.Web.Filter
{
    public class LoginAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            DateTime st;
            Logger.Info("login Authorization start:" + DateTime.Now);
            st = DateTime.Now;
            var ad = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (ad.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
            {
                return;
            }
            if (string.IsNullOrEmpty(CookiesAdmin.AdminName))
            {
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    filterContext.Result = new JsonResult("Not logged in");
                }
                else
                {
                    filterContext.Result = new RedirectResult(CookiesAdmin.LoginUrl);
                }
            }
            Logger.Info("login Authorization waste time:" + DateTime.Now.Subtract(st).TotalSeconds);
        }
    }
}

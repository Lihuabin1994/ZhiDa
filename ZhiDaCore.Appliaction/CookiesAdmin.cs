using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ZhiDaCore.IService;

namespace ZhiDaCore.Application
{
    public static class ServiceContainer
    {
        public static IServiceProvider Instance { get; set; }
        public static T GetService<T>() where T : class
        {
            return Instance.GetService<T>();
        }

    }
    public static class CookiesAdmin
    {
       
        public static string AdminName
        {
            get
            {
                //获取cookie
                var hasCookie = ServiceContainer.GetService<IHttpContextAccessor>()
                    .HttpContext
                    .Request.Cookies
                    .TryGetValue(ApplicationKeys.User_Cookie_Key, out string encryptValue);
                if (!hasCookie || string.IsNullOrEmpty(encryptValue))
                    return null;
                var adminName = ServiceContainer.GetService<IUserService>().LoginDecrypt(encryptValue, ApplicationKeys.User_Cookie_Encryption_Key);
                return adminName;
            }
        }
        public const string LoginUrl = "/User/Login";
    }
}

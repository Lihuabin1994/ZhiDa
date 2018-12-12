using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZhiDaCore.Application;
using ZhiDaCore.EFCore;
using ZhiDaCore.IService;
using ZhiDaCore.Service;
using ZhiDaCore.Web.Filter;

namespace ZhiDaCore.Web
{
    public class Startup
    {//log4net日志
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IContainer AutofacContainer;
        // This method gets called by the runtime. Use this method to add services to the container.

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.Configure<CookiePolicyOptions>(options =>
        //    {
        //        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        //        options.CheckConsentNeeded = context => true;
        //        options.MinimumSameSitePolicy = SameSiteMode.None;
        //    });
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        //    //注册服务进 IServiceCollection
        //    services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        //    services.AddMvc();
        //    ContainerBuilder builder = new ContainerBuilder();
        //    //将services中的服务填充到Autofac中.
        //    builder.Populate(services);
        //    //新模块组件注册
        //    builder.RegisterModule<DefaultModuleRegister>();
        //    //创建容器.
        //    AutofacContainer = builder.Build();
        //    //使用容器创建 AutofacServiceProvider 
        //    return new AutofacServiceProvider(AutofacContainer);
        //}

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//用于获取请求上下文
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(x=> { x.Filters.Add(new LoginAttribute()); });   //全局验证
            return RegisterAutofac(services);//注册Autofac
        }
        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            //实例化Autofac容器
            var builder = new ContainerBuilder();
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //新模块组件注册    
            builder.RegisterModule<DefaultModuleRegister>();
            //创建容器
            var Container = builder.Build();
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(Container);
        }

        public class DefaultModuleRegister : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                try
                {
                    //注册当前程序集中以“Service”结尾的类,暴漏类实现的所有接口，生命周期为PerLifetimeScope
                    //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
                    builder.RegisterAssemblyTypes(GetAssembly("ZhiDaCore.IService")).Where(a => a.Name.EndsWith("IService")).AsImplementedInterfaces();
                    builder.RegisterAssemblyTypes(GetAssembly("ZhiDaCore.Service")).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
                    //单独注册
                    // builder.RegisterType<TradeService>().Named<ITradeService>(typeof(TradeService).Name);
                    //注册所有"MyApp.Repository"程序集中的类
                    //builder.RegisterAssemblyTypes(GetAssembly("MyApp.Repository")).AsImplementedInterfaces();
                }
                catch (Exception ex)
                {

                    throw;
                }
             
            }

            public static Assembly GetAssembly(string assemblyName)
            {
                //return Assembly.Load(assemblyName);
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
                return assembly;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=Login}/{id?}");
            });
            ServiceContainer.Instance = app.ApplicationServices;
            //程序停止调用函数
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}

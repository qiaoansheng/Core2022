using Autofac;
using Core2022.Framework.Commons.Autofac;
using Core2022.Framework.Commons.Injections;
using Core2022.Framework.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2022.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // 初始化配置信息
            AppSettings.InitAppSettings(configuration);

        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Autofac 依赖注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Autofac 注入Orm对象
            builder.AutofacInjectionOrmModel();
            // Autofac 注入各层之间的依赖
            builder.AutofacInjectionServices();
            builder.RegisterBuildCallback(scope =>
            {
                AppSettings.AppAutofacContainer((IContainer)scope);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //注册控制器和视图
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceScopeFactory serviceScopeFactory)
        {
            // 依赖注入容器
            AppSettings.AppServiceScopeFactory(serviceScopeFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });


        }
    }
}

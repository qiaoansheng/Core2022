using Autofac;
using Core2022.Framework;
using Core2022.Framework.Commons.Autofac;
using Core2022.Framework.Commons.AutoMapper;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using System;
using System.Security.Principal;
using System.Threading;

namespace Core2022.Application.Services.Test
{
    public class TestClassBase
    {
        ContainerBuilder builder = new ContainerBuilder();
        IContainer rootContainer;

        public TestClassBase()
        {

            // 初始化 appsettings
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Global.InitAppSettings(configuration);
            NLog.LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));


            // 初始化 Autofac 容器
            // Autofac 注入Orm对象
            //builder.AutofacInjectionOrmModel();
            // Autofac 注入各层之间的依赖
            builder.AutofacInjectionServices();
            // Autofac 注入 AutoMapper
            builder.AutofacInjectionAutoMapper();

            rootContainer = builder.Build();

            //builder.RegisterBuildCallback(scope =>
            //{
            //    Global.AppAutofacContainer((IContainer)scope);
            //});
            Global.AppAutofacContainer(rootContainer);
        }

        public void WriteLoginUserInfo(Guid userKeyId,string userName)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
               new GenericIdentity($"{ Guid.NewGuid() }|{ userKeyId }|{ userName }|"), null);
        }
    }
}

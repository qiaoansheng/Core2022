using Autofac;
using Core2022.Framework;
using Core2022.Framework.Commons.Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            // 初始化 Autofac 容器
            // Autofac 注入Orm对象
            builder.AutofacInjectionOrmModel();
            // Autofac 注入各层之间的依赖
            builder.AutofacInjectionServices();

            rootContainer = builder.Build();

            //builder.RegisterBuildCallback(scope =>
            //{
            //    Global.AppAutofacContainer((IContainer)scope);
            //});
            Global.AppAutofacContainer(rootContainer);
        }


    }
}

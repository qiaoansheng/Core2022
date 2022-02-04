using Autofac;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Core2022.Framework.Settings;
using Autofac.Extras.DynamicProxy;
using System.Collections.Generic;
using Core2022.Framework.UnitOfWork;

namespace Core2022.Framework.Commons.Autofac
{
    public static class AutofacInjectionServicesExtension
    {

        public static ContainerBuilder AutofacInjectionServices(this ContainerBuilder builder)
        {
            foreach (var assemblyString in AppSettings.InjectionServices.AssemblyStrings)
            {
                builder.RegisterType<AutofacAOP>();
                var assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyString);
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces()
                    .InstancePerDependency()
                    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                    .EnableClassInterceptors()
                    .InterceptedBy(new List<Type>().ToArray());
            }

            return builder;
        }
    }
}

using Autofac;
using Autofac.Extras.DynamicProxy;
using Core2022.Framework.Settings;
using Core2022.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Reflection;

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

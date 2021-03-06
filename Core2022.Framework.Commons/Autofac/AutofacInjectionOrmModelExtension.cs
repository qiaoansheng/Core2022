using Autofac;
using System;
using System.Reflection;

namespace Core2022.Framework.Commons.Autofac
{
    public static class AutofacInjectionOrmModelExtension
    {
        public static ContainerBuilder AutofacInjectionOrmModel(this ContainerBuilder builder)
        {
            foreach (var assemblyString in Global.InjectionServices.AssemblyStrings)
            {
                var assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyString);
            }

            return builder;
        }
    }
}

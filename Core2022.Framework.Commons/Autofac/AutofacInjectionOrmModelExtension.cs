using Autofac;
using Core2022.Framework.Settings;
using System;
using System.Reflection;

namespace Core2022.Framework.Commons.Autofac
{
    public static class AutofacInjectionOrmModelExtension
    {
        public static ContainerBuilder AutofacInjectionOrmModel(this ContainerBuilder builder)
        {
            foreach (var assemblyString in AppSettings.InjectionServices.AssemblyStrings)
            {
                var assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyString);
            }

            return builder;
        }
    }
}

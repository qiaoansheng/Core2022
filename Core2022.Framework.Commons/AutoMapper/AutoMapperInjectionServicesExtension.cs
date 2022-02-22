using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core2022.Framework.Commons.AutoMapper
{
    public static class AutoMapperInjectionServicesExtension
    {
        private static readonly Action<IMapperConfigurationExpression> FallBackExpression =
            config => { };

        public static ContainerBuilder AutofacInjectionAutoMapper(this ContainerBuilder builder)
        {
            var assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "Core2022.Application.Services.DTO.dll");
            return RegisterAutoMapperInternal(builder, new[] { assembly });
        }



        private static ContainerBuilder RegisterAutoMapperInternal(ContainerBuilder builder,
       IEnumerable<Assembly> assemblies, Action<IMapperConfigurationExpression>? configExpression = null, bool propertiesAutowired = false)
        {
            var usedAssemblies = assemblies as Assembly[] ?? assemblies.ToArray();

            var usedConfigExpression = configExpression ?? FallBackExpression;

            builder.RegisterModule(new AutoMapperModule(usedAssemblies, usedConfigExpression, propertiesAutowired));

            return builder;
        }
    }
}

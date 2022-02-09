//using Core2022.Framework.Attributes;
//using Core2022.Framework.Entity;
//using Core2022.Framework.Settings;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Reflection;

//namespace Core2022.Framework.Commons.Injections
//{
//    public static class InjectionOrmModelExtension
//    {
//        public static void InjectionInjectionOrmModel(this IServiceCollection services)
//        {
//            foreach (var assemblyString in Global.InjectionServices.AssemblyStrings)
//            {
//                var serviceTypes = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyString).GetTypes();
//                if (serviceTypes != null && serviceTypes.Length > 0)
//                {
//                    foreach (var service in serviceTypes)
//                    {
//                        // 注入ORM对象
//                        if (service.BaseType == typeof(BaseOrmModel))
//                        {
//                            services.AddScoped(service);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}

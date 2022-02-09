//using Core2022.Framework.Attributes;
//using Core2022.Framework.Entity;
//using Core2022.Framework.Settings;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Reflection;

//namespace Core2022.Framework.Commons.Injections
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public static class InjectionServicesExtension
//    {
//        /// <summary>
//        /// 通过反射注册项目中的所有服务
//        /// </summary>
//        /// <param name="services"></param>
//        /// <param name="configuration"></param>
//        /// <returns></returns>
//        public static void InjectionServices(this IServiceCollection services, IConfiguration configuration)
//        {
//            foreach (var assemblyString in Global.InjectionServices.AssemblyStrings)
//            {
//                var serviceTypes = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyString).GetTypes();
//                if (serviceTypes != null && serviceTypes.Length > 0)
//                {
//                    foreach (var service in serviceTypes)
//                    {
//                        var attribute = service.GetCustomAttribute(typeof(InjectionAttribute), false);
//                        if (attribute is InjectionAttribute)
//                        {
//                            InjectionAttribute injectionAttribute = attribute as InjectionAttribute;
//                            var serviceInterfaceName = injectionAttribute.ServiceInterfaceName;
//                            var serviceLifetime = injectionAttribute.ServiceLifetime;
//                            AddInjectionWithLifetime(services, serviceLifetime, serviceInterfaceName, service);
//                        }
//                        //// 注入ORM对象
//                        //if (service.BaseType == typeof(BaseOrmModel))
//                        //{
//                        //    services.AddScoped(typeof(BaseOrmModel), service);
//                        //    services.AddScoped(service);
//                        //}
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 向依赖框架中注册服务
//        /// </summary>
//        /// <param name="services">依赖框架</param>
//        /// <param name="serviceLifetime">服务生命周期</param>
//        /// <param name="service">服务</param>
//        /// <param name="implementation">服务的实现</param>
//        /// <returns></returns>
//        private static IServiceCollection AddInjectionWithLifetime(IServiceCollection services, ServiceLifetime serviceLifetime, Type service, Type implementation)
//        {
//            switch (serviceLifetime)
//            {
//                case ServiceLifetime.Scoped:
//                    return services.AddScoped(service, implementation);
//                case ServiceLifetime.Singleton:
//                    return services.AddSingleton(service, implementation);
//                case ServiceLifetime.Transient:
//                    return services.AddTransient(service, implementation);
//                default:
//                    return services;
//            }
//        }
//    }
//}

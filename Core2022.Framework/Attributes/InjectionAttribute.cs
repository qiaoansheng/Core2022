using Microsoft.Extensions.DependencyInjection;
using System;


namespace Core2022.Framework.Attributes
{
    public class InjectionAttribute : Attribute
    {
        /// <summary>
        /// 服务依赖的接口
        /// </summary>
        public Type ServiceInterfaceName { get; set; }

        /// <summary>
        /// 注册服务的生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }

        /// <summary>
        /// 依赖特性
        /// </summary>
        /// <param name="name">服务依赖接口</param>
        /// <param name="serviceLifetime">服务生命周期</param>
        public InjectionAttribute(Type serviceInterfaceName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceInterfaceName = serviceInterfaceName;
            ServiceLifetime = serviceLifetime;
        }
    }
}

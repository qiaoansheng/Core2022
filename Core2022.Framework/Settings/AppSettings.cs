using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Settings
{
    public static class AppSettings
    {

        public static IServiceScopeFactory ServiceScopeFactory { get; set; }

        public static InjectionServicesSettings InjectionServices { get; set; }
        /// <summary>
        /// 把配置信息转换成全局类型
        /// </summary>
        /// <param name="configuration"></param>
        public static void InitAppSettings(IConfiguration configuration)
        {
            InjectionServices = new InjectionServicesSettings(configuration.GetSection("InjectionServices"));
        }


        public static void AppServiceScopeFactory(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

    }


}

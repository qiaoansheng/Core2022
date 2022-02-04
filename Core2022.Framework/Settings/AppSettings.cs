using Autofac;
using Core2022.Framework.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Settings
{
    public static class AppSettings
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnectionString { get; set; }
        public static string OrmModelDLL { get; set; }

        public static IServiceScopeFactory ServiceScopeFactory { get; set; }

        public static IContainer AutofacContainer { get; set; }

        public static InjectionServicesSettings InjectionServices { get; set; }
        static List<Type> efInitType = null;

        public static List<Type> OrmModelInit
        {
            get
            {
                if (efInitType == null)
                {
                    efInitType = new List<Type>();
                    efInitType.AddRange(Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + OrmModelDLL)
                    .GetTypes()
                    .Where(x => x.GetTypeInfo().BaseType != null && x.BaseType == (typeof(BaseOrmModel)))
                    .ToList());
                }
                return efInitType;
            }
        }


        /// <summary>
        /// 把配置信息转换成全局类型
        /// </summary>
        /// <param name="configuration"></param>
        public static void InitAppSettings(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionString"];
            OrmModelDLL = configuration["OrmModelDLL"];
            InjectionServices = new InjectionServicesSettings(configuration.GetSection("InjectionServices"));
        }


        public static void AppServiceScopeFactory(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
        }


        public static void AppAutofacContainer(IContainer autofacContainer)
        {
            AutofacContainer = autofacContainer;
        }

    }


}

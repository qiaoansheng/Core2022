using Autofac;
using Microsoft.AspNetCore.Http;

namespace Core2022.Framework.Web
{
    public static class HttpContext
    {
        /// <summary>
        /// Startup.Configure 中对该属性初始化
        /// </summary>
        public static ILifetimeScope ServiceProvider;

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                IHttpContextAccessor factory = ServiceProvider.Resolve(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                Microsoft.AspNetCore.Http.HttpContext context = factory?.HttpContext;
                return context;
            }
        }
    }
}

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Core2022.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
#if DEBUG
                        logging.SetMinimumLevel(LogLevel.Debug);
#else
                        logging.SetMinimumLevel(LogLevel.Trace);
#endif
                    }).UseNLog();
                })
                //×¢²áAutofac
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}

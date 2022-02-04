using Autofac;
using Core2022.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core2022.Framework.UnitOfWork
{
    public class AppUnitOfWorkFactory
    {
        private static ThreadLocal<IUnitOfWork> appUnitOfWorkThreadLocal = new ThreadLocal<IUnitOfWork>(() => CreateUnitOfWork());

        public static IUnitOfWork GetAppUnitOfWorkRepository()
        {
            if (appUnitOfWorkThreadLocal.Value == null)
            {
                appUnitOfWorkThreadLocal.Value = CreateUnitOfWork();
            }
            return appUnitOfWorkThreadLocal.Value;
        }


        private static IUnitOfWork CreateUnitOfWork()
        {
            return AppSettings.AutofacContainer.Resolve<IUnitOfWork>();
        }
    }
}

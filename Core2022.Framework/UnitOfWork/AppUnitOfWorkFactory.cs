using Autofac;
using System.Threading;

namespace Core2022.Framework.UnitOfWork
{
    public class AppUnitOfWorkFactory
    {
        private static ThreadLocal<IAppUnitOfWork> appUnitOfWorkThreadLocal = new ThreadLocal<IAppUnitOfWork>(() => CreateUnitOfWork());
        private static ThreadLocal<IReadUnitOfWork> readUnitOfWorkThreadLocal = new ThreadLocal<IReadUnitOfWork>(() => CreateReadUnitOfWork());

        public static IAppUnitOfWork GetAppUnitOfWorkRepository()
        {
            if (appUnitOfWorkThreadLocal.Value == null)
            {
                appUnitOfWorkThreadLocal.Value = CreateUnitOfWork();
            }
            return appUnitOfWorkThreadLocal.Value;
        }

        private static IAppUnitOfWork CreateUnitOfWork()
        {
            return Global.AutofacContainer.Resolve<IAppUnitOfWork>();
        }

        #region 只读
        public static IReadUnitOfWork GetReadUnitOfWorkRepository()
        {
            if (readUnitOfWorkThreadLocal.Value == null)
            {
                readUnitOfWorkThreadLocal.Value = CreateReadUnitOfWork();
            }
            return readUnitOfWorkThreadLocal.Value;
        }

        private static IReadUnitOfWork CreateReadUnitOfWork()
        {
            return Global.AutofacContainer.Resolve<IReadUnitOfWork>();
        }
        #endregion
    }
}

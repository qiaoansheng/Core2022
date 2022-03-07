using Autofac;
using Core2022.Framework.Web;

namespace Core2022.Framework.UnitOfWork
{
    /// <summary>
    /// 创建工作单元
    /// 保证线程内唯一
    /// </summary>
    public class AppUnitOfWorkFactory
    {
        public static IAppUnitOfWork GetAppUnitOfWorkRepository()
        {
            IAppUnitOfWork appUnitOfWork = HttpContext.Current.Items["AppUnitOfWork"] as IAppUnitOfWork;
            if (appUnitOfWork == null)
            {
                appUnitOfWork = CreateUnitOfWork();
                HttpContext.Current.Items["AppUnitOfWork"] = appUnitOfWork;
            }
            return appUnitOfWork;
        }

        private static IAppUnitOfWork CreateUnitOfWork()
        {
            return Global.AutofacContainer.Resolve<IAppUnitOfWork>();
        }

        #region 只读
        public static IReadUnitOfWork GetReadUnitOfWorkRepository()
        {
            IReadUnitOfWork readUnitOfWork = HttpContext.Current.Items["ReadUnitOfWork"] as IReadUnitOfWork;
            if (readUnitOfWork == null)
            {
                readUnitOfWork = CreateReadUnitOfWork();
                HttpContext.Current.Items["ReadUnitOfWork"] = readUnitOfWork;
            }
            return readUnitOfWork;
        }

        private static IReadUnitOfWork CreateReadUnitOfWork()
        {
            return Global.AutofacContainer.Resolve<IReadUnitOfWork>();
        }
        #endregion
    }
}

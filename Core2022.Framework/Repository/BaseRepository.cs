using Autofac;
using Autofac.Extras.DynamicProxy;
using Core2022.Framework.Attributes;
using Core2022.Framework.Entity;
using Core2022.Framework.Settings;
using Core2022.Framework.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Repository
{
    [Intercept(typeof(AutofacAOP))]
    public abstract class BaseRepository<IDomain, OrmEntity> : IBaseRepository<IDomain, OrmEntity>
        where OrmEntity : BaseOrmModel
    {
        private IUnitOfWork UnitOfWork
        {
            get
            {
                return AppUnitOfWorkFactory.GetAppUnitOfWorkRepository();
            }
        }

        [AOPLog]
        public virtual IDomain Find(Guid keyId, bool readOnly = false)
        {
            OrmEntity entity = UnitOfWork.CreateSet<OrmEntity>().Find(keyId);

            IDomain domain = AppSettings.AutofacContainer.Resolve<IDomain>(new NamedParameter("entity", entity));

            return domain;
        }
    }
}

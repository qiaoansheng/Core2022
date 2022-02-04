using Autofac;
using Autofac.Extras.DynamicProxy;
using Core2022.Framework.Attributes;
using Core2022.Framework.Domain;
using Core2022.Framework.Entity;
using Core2022.Framework.Settings;
using Core2022.Framework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public void Add(IDomain domain)
        {
            if (domain != null)
            {
                BaseDomain baseDomain = domain as BaseDomain;
                BaseOrmModel itemEntity = baseDomain.GetModel();
                itemEntity.CreateTime = DateTime.Now;
                if (itemEntity.CreateUserKeyId == default(Guid))
                {
                    itemEntity.CreateUserKeyId = Guid.Parse("99999999-9999-9999-9999-999999999999");
                }
                itemEntity.UpdateTime = DateTime.Now;
                itemEntity.UpdateUserKeyId = Guid.Parse("99999999-9999-9999-9999-999999999999");
                itemEntity.Version = 1;
                itemEntity.IsDelete = false;
                this.GetSet().Add((OrmEntity)itemEntity);
            }
            else
            {
                throw new Exception("Domain对象转换层 ORM 对象时失败");

            }
        }



        [AOPLog]
        public virtual IDomain Find(Guid keyId, bool readOnly = false)
        {
            OrmEntity entity = UnitOfWork.CreateSet<OrmEntity>().Find(keyId);

            IDomain domain = AppSettings.AutofacContainer.Resolve<IDomain>(new NamedParameter("entity", entity));

            return domain;
        }


        public IList<IDomain> FindList(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false)
        {
            IQueryable<OrmEntity> entityList = UnitOfWork.CreateSet<OrmEntity>().Where(predicate);
            if (entityList == null)
            {
                return default;
            }
            var domain = this.Convert(entityList);
            return domain;
        }


        private DbSet<OrmEntity> GetSet()
        {
            return this.UnitOfWork.CreateSet<OrmEntity>();
        }


        private IList<IDomain> Convert(IQueryable<OrmEntity> orms)
        {
            IList<IDomain> domains = new List<IDomain>();
            foreach (var item in orms)
            {
                domains.Add(Convert(item));
            }
            return domains;
        }


        private IDomain Convert(OrmEntity orm)
        {
            return AppSettings.GetT<IDomain>("entity", orm);
        }


    }
}

using Autofac;
using Autofac.Extras.DynamicProxy;
using Core2022.Framework.Attributes;
using Core2022.Framework.Authorizations;
using Core2022.Framework.Domain;
using Core2022.Framework.Entity;
using Core2022.Framework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core2022.Framework.Repository
{
    [Intercept(typeof(AutofacFilter))]
    public abstract class BaseRepository<IDomain, OrmEntity> : IBaseRepository<IDomain, OrmEntity>
        where OrmEntity : BaseOrmModel
        where IDomain : IBaseDomain
    {
        private IUnitOfWork UnitOfWork(bool readOnly = false)
        {
            if (!readOnly)
            {
                return AppUnitOfWorkFactory.GetAppUnitOfWorkRepository();
            }
            return AppUnitOfWorkFactory.GetReadUnitOfWorkRepository();
        }

        /// <summary>
        /// 新增 把领域对象添加到数据库上下文中。
        /// 同步方法，异步方法没有意义
        /// </summary>
        /// <param name="domain"></param>
        public void Add(IDomain domain)
        {
            if (domain != null)
            {
                BaseDomain baseDomain = domain as BaseDomain;
                BaseOrmModel itemEntity = baseDomain.GetModel();
                itemEntity.CreateTime = DateTime.Now;
                if (itemEntity.CreateUserKeyId == default(Guid))
                {
                    itemEntity.CreateUserKeyId = AuthorizationUtil.GetCurrentUserKeyId();
                }
                itemEntity.UpdateTime = DateTime.Now;
                itemEntity.UpdateUserKeyId = AuthorizationUtil.GetCurrentUserKeyId();
                itemEntity.Version = 1;
                itemEntity.IsDelete = false;
                this.CreateSet().Add((OrmEntity)itemEntity);
            }
            else
            {
                throw new Exception("Domain对象转换层 ORM 对象时失败");

            }
        }

        /// <summary>
        /// 通过指定 KeyId 获取数据
        /// </summary>
        /// <param name="keyId">主键KeyId</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象</returns>
        [AOPLog]
        public virtual IDomain Find(Guid keyId, bool readOnly = false)
        {
            OrmEntity entity = UnitOfWork(readOnly).CreateSet<OrmEntity>().Find(keyId);
            if (entity == null)
            {
                return default;
            }
            IDomain domain = this.Convert(entity);
            return domain;
        }

        /// <summary>
        /// 通过指定 KeyId 获取数据
        /// </summary>
        /// <param name="keyId">主键KeyId</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象</returns>
        [AOPLog]
        public virtual async Task<IBaseDomain> FindAsync(Guid keyId, bool readOnly = false)
        {
            OrmEntity entity = await UnitOfWork(readOnly).CreateSet<OrmEntity>().FindAsync(keyId);
            if (entity == null)
            {
                return default;
            }
            IBaseDomain domain = this.Convert(entity).AsBaseDomain();//.AsTask(;
            return domain;
        }


        /// <summary>
        /// 通过指定 参数 获取领域对象
        /// </summary>
        /// <param name="predicate">表达式树</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象</returns>
        public async Task<IBaseDomain> FindAsync(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false)
        {
            OrmEntity entity = await UnitOfWork(readOnly).CreateSet<OrmEntity>().Where(predicate).FirstOrDefaultAsync();
            if (entity == null)
            {
                return default;
            }
            IBaseDomain domain = this.Convert(entity).AsBaseDomain();
            return domain;
        }


        /// <summary>
        /// 通过指定 参数 获取列表数据
        /// </summary>
        /// <param name="predicate">表达式树</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象集合</returns>
        public IList<IDomain> FindList(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false)
        {
            IQueryable<OrmEntity> entityList = UnitOfWork(readOnly).CreateSet<OrmEntity>().Where(predicate);
            if (entityList == null)
            {
                return default;
            }
            var domain = this.Convert(entityList);
            return domain;
        }

        /// <summary>
        /// 通过指定 参数 获取列表数据
        /// </summary>
        /// <param name="predicate">表达式树</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象集合</returns>
        public async Task<IList<IDomain>> FindListAsync(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false)
        {
            IList<OrmEntity> entityList = await UnitOfWork(readOnly).CreateSet<OrmEntity>().Where(predicate).ToListAsync();
            if (entityList == null)
            {
                return default;
            }
            var domain = this.Convert(entityList.AsQueryable());
            return domain;
        }

        private DbSet<OrmEntity> CreateSet()
        {
            return this.UnitOfWork().CreateSet<OrmEntity>();
        }

        #region ORM 对象转 领域对象
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
            return Global.GetT<IDomain>("entity", orm);
        }
        #endregion

    }
}

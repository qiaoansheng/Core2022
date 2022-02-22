using Core2022.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core2022.Framework.Repository
{
    public interface IBaseRepository<IDomain, OrmEntity>
    {
        /// <summary>
        /// 通过指定 KeyId 获取数据
        /// </summary>
        /// <param name="keyId">主键KeyId</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象</returns>
        IDomain Find(Guid keyId, bool readOnly = false);

        /// <summary>
        /// 通过指定 KeyId 获取数据
        /// </summary>
        /// <param name="keyId">主键KeyId</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象</returns>
        Task<IBaseDomain> FindAsync(Guid keyId, bool readOnly = false);

        /// <summary>
        /// 通过指定 参数 获取领域对象
        /// </summary>
        /// <param name="predicate">表达式树</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象</returns>
        Task<IBaseDomain> FindAsync(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false);

        /// <summary>
        /// 通过指定 参数 获取列表数据
        /// </summary>
        /// <param name="predicate">表达式树</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象集合</returns>
        IList<IDomain> FindList(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false);

        /// <summary>
        /// 通过指定 参数 获取列表数据
        /// </summary>
        /// <param name="predicate">表达式树</param>
        /// <param name="readOnly">读写分离，true 读库，false 写库，默认写库</param>
        /// <returns>返回 领域对象集合</returns>
        Task<IList<IDomain>> FindListAsync(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false);

        /// <summary>
        /// 新增 把领域对象添加到数据库上下文中。
        /// 同步方法，异步方法没有意义
        /// </summary>
        /// <param name="domain"></param>
        void Add(IDomain domain);

    }
}

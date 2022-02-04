using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Repository
{
    public interface IBaseRepository<IDomain, OrmEntity>
    {

        public IDomain Find(Guid keyId, bool readOnly = false);

        public IList<IDomain> FindList(Expression<Func<OrmEntity, bool>> predicate, bool readOnly = false);

        public void Add(IDomain domain);
    }
}

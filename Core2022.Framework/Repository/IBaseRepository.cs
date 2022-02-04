using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Repository
{
    public interface IBaseRepository<IDomain, OrmEntity>
    {

        public IDomain Find(Guid keyId, bool readOnly = false);


    }
}

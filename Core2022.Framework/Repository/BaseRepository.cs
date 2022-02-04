using Core2022.Framework.Entity;
using Core2022.Framework.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Repository
{
    public class BaseRepository<IDomain, OrmEntity> : IBaseRepository<IDomain, OrmEntity>
        where OrmEntity : BaseOrmModel
    {

        public IDomain Find(Guid keyId, bool readOnly = false)
        {

            if (AppSettings.ServiceScopeFactory != null)
            {
                using (var scope = AppSettings.ServiceScopeFactory.CreateScope())
                {
                    return scope.ServiceProvider.GetRequiredService<IDomain>();
                }
            }

            return default(IDomain);
        }
    }
}

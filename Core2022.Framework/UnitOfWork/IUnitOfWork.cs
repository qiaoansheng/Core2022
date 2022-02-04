using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<OrmEntity> CreateSet<OrmEntity>() where OrmEntity : class;
    }
}

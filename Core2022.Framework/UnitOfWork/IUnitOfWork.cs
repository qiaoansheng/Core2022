using Microsoft.EntityFrameworkCore;
using System;

namespace Core2022.Framework.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<OrmEntity> CreateSet<OrmEntity>() where OrmEntity : class;

    }
}

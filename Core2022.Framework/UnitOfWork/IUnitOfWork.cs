using Microsoft.EntityFrameworkCore;

namespace Core2022.Framework.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<OrmEntity> CreateSet<OrmEntity>() where OrmEntity : class;

        int SaveChanges();
    }
}

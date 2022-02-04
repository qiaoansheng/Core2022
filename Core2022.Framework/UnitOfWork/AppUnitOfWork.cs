using Core2022.Framework.Settings;
using Microsoft.EntityFrameworkCore;

namespace Core2022.Framework.UnitOfWork
{
    public class AppUnitOfWork : DbContext, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppSettings.OrmModelInit.ForEach(t =>
            {
                modelBuilder.Model.AddEntityType(t);
            });
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<OrmEntity> CreateSet<OrmEntity>()
          where OrmEntity : class
        {
            return base.Set<OrmEntity>();
        }


        public new int SaveChanges()
        {
            return base.SaveChanges();
        }


    }
}

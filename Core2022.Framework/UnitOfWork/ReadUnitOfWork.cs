using Microsoft.EntityFrameworkCore;

namespace Core2022.Framework.UnitOfWork
{
    public class ReadUnitOfWork : DbContext, IReadUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Global.ReadConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Global.OrmModelInit.ForEach(t =>
            {
                modelBuilder.Model.AddEntityType(t);
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OrmEntity> CreateSet<OrmEntity>() where OrmEntity : class
        {
            return base.Set<OrmEntity>();
        }


    }
}

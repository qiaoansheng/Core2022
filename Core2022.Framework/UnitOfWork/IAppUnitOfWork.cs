using System.Threading.Tasks;

namespace Core2022.Framework.UnitOfWork
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

    }
}

using Core2022.Domain.Interface;
using Core2022.Domain.Model;
using Core2022.Framework.Repository;

namespace Core2022.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<IUserDomain, UserEntity>
    {

    }
}

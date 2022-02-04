using Core2022.Domain.Interface;
using Core2022.Domain.Model;
using Core2022.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<IUserDomain, UserEntity>
    {
        public void Test();
    }
}

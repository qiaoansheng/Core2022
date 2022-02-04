using Core2022.Domain.Interface;
using Core2022.Domain.Model;
using Core2022.Framework.Repository;
using Core2022.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Repository
{
    public class RoleRepository : BaseRepository<IRoleDomain, RoleEntity>, IRoleRepository
    {
    }
}

using Core2022.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Application.Services
{
    public class ApplicationServiceBase
    {
        public IUserRepository UserRepository { get; set; }
    }
}

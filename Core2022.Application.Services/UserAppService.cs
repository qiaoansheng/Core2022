using Core2022.Application.Services.Interface;
using Core2022.Domain.Interface;
using Core2022.Framework.Attributes;
using Core2022.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Application.Services
{
    [Injection(typeof(IUserAppService))]
    public class UserAppService : ApplicationServiceBase, IUserAppService
    {

        public UserAppService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public void Test()
        {
            // 测试 仓储对象
            UserRepository.Test();

            // 模拟一个 ORM对象转领域对象
            IUserDomain userDomain = UserRepository.Find(Guid.Parse("b405c2de-3854-437a-9c75-b3fb46e5bd18"));
            userDomain.GetTest();

            userDomain.SetTest("11111");

            userDomain.GetTest();

        }



    }
}

using Autofac;
using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Application.Services.Test
{
    [TestClass]
    public class UserAppServiceTest : TestClassBase
    {

        IUserAppService userAppService;
        public UserAppServiceTest()
        {
            userAppService = Global.GetT<IUserAppService>();
        }

        [TestMethod]
        public void CreateUser()
        {

            var respDto = userAppService.CreateUser(new UserRequestDto()
            {
                UserName = "222333",
                PassWord = "111111"
            });



        }







    }
}

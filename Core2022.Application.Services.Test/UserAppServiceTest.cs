using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod]
        public void UpdateUser()
        {
            var respDto = userAppService.UpdateUser(new UserRequestDto()
            {
                KeyId = Guid.Parse("71060A92-8FB9-4F1E-BE51-F3306166DD61"),
                UserName = "8888881",
                PassWord = "9999991"
            });
        }




    }
}

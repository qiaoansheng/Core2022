using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Framework;
using Core2022.Framework.Authorizations;
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
            IdentifyInfo identify = new IdentifyInfo()
            {
                UserKeyId = Guid.NewGuid(),
                UserName = "",
            };

            userAppService = Global.GetT<IUserAppService>();

            WriteLoginUserInfo(Guid.Parse("99999999-9999-9999-9999-999999999999"), "单元测试");
        }

        [TestMethod]
        public void CreateUser()
        {
            var respDto = userAppService.CreateUser(new UserRequestDto()
            {
                UserName = "222333",
                PassWord = "111111"
            });
            Assert.IsNotNull(respDto);
            Assert.IsNotNull(respDto.Result);
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
            Assert.IsNotNull(respDto);
            Assert.IsTrue(respDto.Result.Data);
        }

        [TestMethod]
        public void FindUser()
        {
            Guid userKeyId = Guid.Parse("71060A92-8FB9-4F1E-BE51-F3306166DD61");
            var respDto = userAppService.Find(new UserRequestDto()
            {
                KeyId = userKeyId,
            });
            Assert.IsNotNull(respDto);
            Assert.IsNotNull(respDto.Result);
            Assert.AreEqual(respDto.Result.Data.KeyId, userKeyId);
        }

        [TestMethod]
        public void FindAllUser()
        {
            var respDto = userAppService.FindList(new UserRequestDto()
            {
                UserName = "2222222"
            });
            Assert.IsNotNull(respDto);
            Assert.IsNotNull(respDto.Result);


        }



    }
}

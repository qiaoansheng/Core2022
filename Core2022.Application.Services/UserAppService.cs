using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Domain.Interface;
using Core2022.Framework;
using Core2022.Framework.Attributes;
using Core2022.Framework.Settings;
using Core2022.Framework.UnitOfWork;
using Core2022.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core2022.Application.Services
{
    [Injection(typeof(IUserAppService))]
    public class UserAppService : ApplicationServiceBase, IUserAppService
    {

        public UserAppService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public Guid CreateUser(UserRequestDto request)
        {
            IUserDomain userDomain = CreateUserDomain(request.UserName);
            userDomain.SetPassWord(request.PassWord); // 自己随意写的就不加盐加密了
            userDomain.SetLastLoginTime(DateTime.Now);

            IUnitOfWork uw = GetUnitOfWork();

            Global.GetT<IUserRepository>().Add(userDomain);

            if (uw.SaveChanges() > 0)
            {
                return userDomain.GetKeyId();
            }
            return Guid.Empty;
        }

        public bool DeleteUser(Guid keyId)
        {
            IUserDomain userDomain = UserRepository.Find(keyId);
            userDomain.SetIsDelete(true);

            return SaveChanges() > 0;
        }

        public bool UpdateUser(UserRequestDto request)
        {
            IUserDomain userDomain = UserRepository.Find(request.KeyId);
            userDomain.SetUserName(request.UserName);
            userDomain.SetPassWord(request.PassWord);
            userDomain.SetLastLoginTime(DateTime.Now);


            return SaveChanges() > 0;
        }

        public UserResponseDto Find(UserRequestDto request)
        {
            IUserDomain userDomain = UserRepository.Find(request.KeyId);
            UserResponseDto respDto = new UserResponseDto()
            {
                KeyId = userDomain.GetKeyId(),
                CreateUserKeyId = userDomain.GetCreateUserKeyId(),
                UpdateUserKeyId = userDomain.GetUpdateUserKeyId(),
                CreateTime = userDomain.GetCreateTime(),
                UpdateTime = userDomain.GetUpdateTime(),
                Version = userDomain.GetVersion(),
                IsDelete = userDomain.GetIsDelete(),
                UserName = userDomain.GetUserName(),
                PassWord = userDomain.GetPassWord(),
                LastLoginTime = userDomain.GetLastLoginTime()
            };
            return respDto;
        }

        public List<UserResponseDto> FindList(UserRequestDto request)
        {
            List<IUserDomain> userDomains = UserRepository.FindList(i => !i.IsDelete && i.UserName == "2222222").ToList();
            List<UserResponseDto> respDto = new List<UserResponseDto>();

            foreach (var item in userDomains)
            {
                respDto.Add(new UserResponseDto()
                {
                    KeyId = item.GetKeyId(),
                    CreateUserKeyId = item.GetCreateUserKeyId(),
                    UpdateUserKeyId = item.GetUpdateUserKeyId(),
                    CreateTime = item.GetCreateTime(),
                    UpdateTime = item.GetUpdateTime(),
                    Version = item.GetVersion(),
                    IsDelete = item.GetIsDelete(),
                    UserName = item.GetUserName(),
                    PassWord = item.GetPassWord(),
                    LastLoginTime = item.GetLastLoginTime()
                });
            }

            return respDto;
        }



    }
}

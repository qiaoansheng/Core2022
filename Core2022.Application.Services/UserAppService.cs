using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Domain.Interface;
using Core2022.Framework;
using Core2022.Framework.Attributes;
using Core2022.Framework.Domain;
using Core2022.Framework.Settings;
using Core2022.Framework.UnitOfWork;
using Core2022.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Guid> CreateUser(UserRequestDto request)
        {
            IUserDomain userDomain = CreateUserDomain(request.UserName);
            userDomain.SetPassWord(request.PassWord); // 自己随意写的就不加盐加密了
            userDomain.SetLastLoginTime(DateTime.Now);

            Global.GetT<IUserRepository>().Add(userDomain);

            if (await SaveChangesAsync() > 0)
            {
                return userDomain.GetKeyId();
            }
            return Guid.Empty;
        }

        public async Task<bool> DeleteUser(Guid keyId)
        {
            //IBaseDomain baseDomain = await UserRepository.FindAsync(keyId);
            ////IContravariant<IBaseDomain> contravariant = baseDomain;
            ////Func<IUserDomain, IBaseDomain> func = (s) => {
            ////    return baseDomain;
            ////};
            ////func(baseDomain);
            //ICovariant<IBaseDomain> contravariant = new Covariant<IUserDomain>();
            //IContravariant<IUserDomain> u = new Contravariant<IBaseDomain>();


            IUserDomain userDomain = UserRepository.Find(keyId); ;
            userDomain.SetIsDelete(true);

            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(UserRequestDto request)
        {
            IUserDomain userDomain = UserRepository.Find(request.KeyId);
            userDomain.SetUserName(request.UserName);
            userDomain.SetPassWord(request.PassWord);
            userDomain.SetLastLoginTime(DateTime.Now);

            return await SaveChangesAsync() > 0;
        }

        public async Task<UserResponseDto> Find(UserRequestDto request)
        {
            IBaseDomain baseDomain = await UserRepository.FindAsync(request.KeyId);
            IUserDomain userDomain = (IUserDomain)baseDomain;
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

        public async Task<List<UserResponseDto>> FindList(UserRequestDto request)
        {
            IList<IUserDomain> userDomains = await UserRepository.FindListAsync(i => !i.IsDelete && i.UserName == request.UserName);
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

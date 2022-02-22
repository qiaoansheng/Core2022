using AutoMapper;
using Core2022.Application.Services.DTO;
using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Domain.Interface;
using Core2022.Enum;
using Core2022.Framework;
using Core2022.Framework.Attributes;
using Core2022.Framework.Authorizations;
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
        private IMapper _mapper;
        public UserAppService(IUserRepository userRepository, IMapper mapper)
        {
            UserRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ResponseDto<string>> LogIn(string userName, string password)
        {
            ResponseDto<string> resp = new ResponseDto<string>();
            IBaseDomain baseDomain = await UserRepository.FindAsync(i => i.UserName == userName && i.PassWord == password, true);
            if (baseDomain != null)
            {
                IUserDomain userDomain = (IUserDomain)baseDomain;
                Token token = new Token()
                {
                    UserKeyId = userDomain.GetKeyId(),
                    UserName = userDomain.GetUserName()
                };
                resp.Data = CodingUtils.AesEncrypt(Newtonsoft.Json.JsonConvert.SerializeObject(token));
                resp.Status = ResultConfig.OK;
                resp.Info = ResultConfig.SuccessfulMessage;
            }
            else
            {
                resp.Status = ResultConfig.OK;
                resp.Info = ResultConfig.SuccessfulMessage;
            }
            return resp;
        }


        public async Task<ResponseDto<Guid>> CreateUser(UserRequestDto request)
        {
            ResponseDto<Guid> resp = new ResponseDto<Guid>();
            IUserDomain userDomain = CreateUserDomain(request.UserName);
            userDomain.SetPassWord(request.PassWord); // 自己随意写的就不加盐加密了
            userDomain.SetLastLoginTime(DateTime.Now);

            Global.GetT<IUserRepository>().Add(userDomain);

            if (await SaveChangesAsync() > 0)
            {
                resp.Data = userDomain.GetKeyId();
                resp.Status = ResultConfig.OK;
                resp.Info = ResultConfig.SuccessfulMessage;
                return resp;
            }
            resp.Status = ResultConfig.Fail;
            resp.Info = ResultConfig.FailMessage;
            return resp;
        }

        public async Task<bool> DeleteUser(Guid keyId)
        {
            IUserDomain userDomain = UserRepository.Find(keyId);
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

        public async Task<ResponseDto<UserResponseDto>> Find(UserRequestDto request)
        {
            ResponseDto<UserResponseDto> resp = new ResponseDto<UserResponseDto>();
            IBaseDomain baseDomain = await UserRepository.FindAsync(request.KeyId, true);
            if (baseDomain != null)
            {
                IUserDomain userDomain = (IUserDomain)baseDomain;
                resp.Data = _mapper.Map<UserResponseDto>(userDomain);
                resp.Status = ResultConfig.OK;
                resp.Info = ResultConfig.SuccessfulMessage;
                return resp;
            }
            resp.Status = ResultConfig.Fail;
            resp.Info = ResultConfig.FailMessage;
            return resp;
        }

        public async Task<List<UserResponseDto>> FindList(UserRequestDto request)
        {
            IList<IUserDomain> userDomains = await UserRepository.FindListAsync(i => !i.IsDelete && i.UserName == request.UserName);
            List<UserResponseDto> respDto = _mapper.Map<List<UserResponseDto>>(userDomains);
            return respDto;
        }



    }
}

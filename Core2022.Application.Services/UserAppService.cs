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
using Core2022.Framework.Nlog;
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
            resp.Status = ResultConfig.Fail;
            resp.Info = ResultConfig.FailMessage;
            LogHelper.Info("LogIn", "日志");
            try
            {
                IBaseDomain baseDomain = await UserRepository.FindAsync(i => i.UserName == userName && i.PassWord == password);
                if (baseDomain != null)
                {
                    IUserDomain userDomain = (IUserDomain)baseDomain;
                    if (userDomain.GetIsDelete())
                    {
                        resp.Msg = "该账号已经被删除";
                        return resp;
                    }
                    Token token = new Token()
                    {
                        UserKeyId = userDomain.GetKeyId(),
                        UserName = userDomain.GetUserName()
                    };
                    userDomain.SetLastLoginTime(DateTime.Now);
                    await SaveChangesAsync();
                    resp.Data = CodingUtils.AesEncrypt(Newtonsoft.Json.JsonConvert.SerializeObject(token));
                    resp.Status = ResultConfig.OK;
                    resp.Info = ResultConfig.SuccessfulMessage;
                    return resp;
                }
                else
                {
                    resp.Msg = "账号密码错误";
                }
            }
            catch (Exception ex)
            {
                resp.Status = ResultConfig.NotSystem;
                resp.Info = ResultConfig.FailMessageNotSystem;
                LogHelper.Error("LogIn", ex);
            }
            return resp;
        }


        public async Task<ResponseDto<Guid>> CreateUser(UserRequestDto request)
        {
            ResponseDto<Guid> resp = new ResponseDto<Guid>();
            LogHelper.Info("CreateUser", "日志");
            try
            {
                // 通过 UserName 查询一下是否存在，如果存在就不允许新增
                IBaseDomain baseDomain = await UserRepository.FindAsync(i => i.UserName == request.UserName && !i.IsDelete, true);
                if (baseDomain != null)
                {
                    resp.Status = ResultConfig.Fail;
                    resp.Info = ResultConfig.FailMessage;
                    resp.Msg = "账号已存在";
                    return resp;
                }

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
            }
            catch (Exception ex)
            {
                resp.Status = ResultConfig.NotSystem;
                resp.Info = ResultConfig.FailMessageNotSystem;
                LogHelper.Error("CreateUser", ex);
            }

            return resp;
        }

        public async Task<ResponseDto<bool>> DeleteUser(Guid keyId)
        {
            ResponseDto<bool> resp = new ResponseDto<bool>();
            LogHelper.Info("DeleteUser", "日志");
            try
            {
                IUserDomain userDomain = UserRepository.Find(keyId);
                if (userDomain == null)
                {
                    resp.Status = ResultConfig.Fail;
                    resp.Info = ResultConfig.FailMessage;
                    resp.Msg = "不存在该用户";
                    return resp;
                }
                userDomain.SetIsDelete(true);
                resp.Data = await SaveChangesAsync() > 0;
                if (resp.Data)
                {
                    resp.Status = ResultConfig.OK;
                    resp.Info = ResultConfig.SuccessfulMessage;
                    return resp;
                }
                resp.Status = ResultConfig.Fail;
                resp.Info = ResultConfig.FailMessage;
            }
            catch (Exception ex)
            {
                resp.Status = ResultConfig.NotSystem;
                resp.Info = ResultConfig.FailMessageNotSystem;
                LogHelper.Error("DeleteUser", ex);
            }
            return resp;
        }

        public async Task<ResponseDto<bool>> UpdateUser(UserRequestDto request)
        {
            ResponseDto<bool> resp = new ResponseDto<bool>();
            LogHelper.Info("UpdateUser", "日志");
            try
            {
                IUserDomain userDomain = UserRepository.Find(request.KeyId);
                if (userDomain == null)
                {
                    resp.Status = ResultConfig.Fail;
                    resp.Info = ResultConfig.FailMessage;
                    resp.Msg = "不存在该用户";
                    return resp;
                }
                if (userDomain.GetUserName() == request.UserName &&
                    userDomain.GetPassWord() == request.PassWord)
                {
                    resp.Status = ResultConfig.Fail;
                    resp.Info = ResultConfig.FailMessage;
                    resp.Msg = "修改的账号密码与原账号密码一致";
                    return resp;
                }
                userDomain.SetUserName(request.UserName);
                userDomain.SetPassWord(request.PassWord);
                resp.Data = await SaveChangesAsync() > 0;
                if (resp.Data)
                {
                    resp.Status = ResultConfig.OK;
                    resp.Info = ResultConfig.SuccessfulMessage;
                    return resp;
                }
                resp.Status = ResultConfig.Fail;
                resp.Info = ResultConfig.FailMessage;
            }
            catch (Exception ex)
            {
                resp.Status = ResultConfig.NotSystem;
                resp.Info = ResultConfig.FailMessageNotSystem;
                LogHelper.Error("UpdateUser", ex);
            }
            
            
            
            return resp;
        }

        public async Task<ResponseDto<UserResponseDto>> Find(UserRequestDto request)
        {
            ResponseDto<UserResponseDto> resp = new ResponseDto<UserResponseDto>();
            LogHelper.Info("Find", "日志");
            try
            {
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
            }
            catch (Exception ex)
            {
                resp.Status = ResultConfig.NotSystem;
                resp.Info = ResultConfig.FailMessageNotSystem;
                LogHelper.Error("Find", ex);
            }

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

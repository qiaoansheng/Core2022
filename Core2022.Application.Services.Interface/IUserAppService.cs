using Core2022.Application.Services.DTO;
using Core2022.Application.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core2022.Application.Services.Interface
{
    public interface IUserAppService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ResponseDto<string>> LogIn(string userName, string password);

        Task<ResponseDto<Guid>> CreateUser(UserRequestDto request);

        Task<bool> DeleteUser(Guid keyId);

        Task<bool> UpdateUser(UserRequestDto request);

        Task<ResponseDto<UserResponseDto>> Find(UserRequestDto request);

        Task<List<UserResponseDto>> FindList(UserRequestDto request);

    }
}

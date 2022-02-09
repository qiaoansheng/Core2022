using Core2022.Application.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core2022.Application.Services.Interface
{
    public interface IUserAppService
    {
        public Task<Guid> CreateUser(UserRequestDto request);

        public Task<bool> DeleteUser(Guid keyId);

        public Task<bool> UpdateUser(UserRequestDto request);

        public Task<UserResponseDto> Find(UserRequestDto request);

        public Task<List<UserResponseDto>> FindList(UserRequestDto request);

    }
}

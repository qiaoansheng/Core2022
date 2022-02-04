using Core2022.Application.Services.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Application.Services.Interface
{
    public interface IUserAppService
    {
        public Guid CreateUser(UserRequestDto request);

        public bool DeleteUser(Guid keyId);

        public bool UpdateUser(UserRequestDto request);

        public UserResponseDto Find(UserRequestDto request);

        public List<UserResponseDto> FindList(UserRequestDto request);

    }
}

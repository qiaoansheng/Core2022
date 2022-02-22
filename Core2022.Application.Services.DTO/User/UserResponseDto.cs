using AutoMapper;
using Core2022.Domain;
using System;

namespace Core2022.Application.Services.DTO.User
{
    public class UserResponseProfile : Profile
    {
        public UserResponseProfile()
        {
            CreateMap<UserDomain, UserResponseDto>();
        }
    }

    public class UserResponseDto : BaseResponseDto
    {
        #region 登录
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        #endregion
    }
}

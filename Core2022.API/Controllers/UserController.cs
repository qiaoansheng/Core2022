using Core2022.API.Filters;
using Core2022.Application.Services.DTO;
using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Core2022.Enum;
using Core2022.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core2022.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        public IUserAppService UserAppService { get; }
        public UserController(IUserAppService userAppService)
        {
            this.UserAppService = userAppService;
        }

        [HttpPost]
        public async Task<ResponseDto<string>> Login(UserRequestDto req)
        {
            ResponseDto<string> resp = await UserAppService.LogIn(req.UserName, req.PassWord);
            if (resp.Status == ResultConfig.OK && !string.IsNullOrEmpty(resp.Data))
            {
                HttpContext.Response.Cookies.Append(Global.CurrentLoginUserKey, resp.Data);
            }
            return resp;
        }

        [HttpPost]
        [AuthorizeFilter]
        public async Task<ResponseDto<Guid>> CreateUser(UserRequestDto req)
        {
            ResponseDto<Guid> resp = await UserAppService.CreateUser(req);
            return resp;
        }

        [HttpPost]
        [AuthorizeFilter]
        public async Task<ResponseDto<UserResponseDto>> FindUserAsync(UserRequestDto req)
        {
            ResponseDto<UserResponseDto> resp = await UserAppService.Find(req);
            return resp;
        }

        [HttpPost]
        [AuthorizeFilter]
        public ResponseDto<UserResponseDto> FindUser1(UserRequestDto req)
        {
            ResponseDto<UserResponseDto> resp = UserAppService.Find(req).Result;
            return resp;
        }




    }
}

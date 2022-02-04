using Core2022.Application.Services.DTO.User;
using Core2022.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core2022.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        public IUserAppService UserAppService { get; }
        public UserController(IUserAppService userAppService)
        {
            this.UserAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser(UserRequestDto request)
        {
            request.UserName = "2222222";
            request.PassWord = "1111111";
            return Json(UserAppService.CreateUser(request));
        }

        public IActionResult DeleteUser(Guid keyId)
        {
            return Json(UserAppService.DeleteUser(keyId));
        }

        public IActionResult UpdateUser(UserRequestDto request)
        {
            return Json(UserAppService.UpdateUser(request));
        }

        public IActionResult Find(UserRequestDto request)
        {
            return Json(UserAppService.Find(request));
        }

        public IActionResult FindList(UserRequestDto request)
        {
            return Json(UserAppService.FindList(request));
        }



    }
}

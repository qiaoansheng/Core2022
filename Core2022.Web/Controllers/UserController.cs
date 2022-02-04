using Core2022.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            UserAppService.Test();
            return View();
        }



    }
}

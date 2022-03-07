using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Principal;
using System.Threading;

namespace Core2022.API
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 设置用户登陆信息
        /// </summary>
        /// <param name="userKeyId"></param>
        /// <param name="userName"></param>
        public void SetUserLoginInfo(Guid userKeyId,string userName)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
               new GenericIdentity($"{ Guid.NewGuid() }|{ userKeyId }|{ userName }|"), null);
        }

        public void SetDefaultUser()
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
               new GenericIdentity($"{ Guid.NewGuid() }|{ "99999999-9999-9999-9999-999999999999" }|{ "manage" }|"), null);
        }

    }
}

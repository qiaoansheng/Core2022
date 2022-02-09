using Core2022.Framework.Authorizations;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Principal;
using System.Threading;

namespace Core2022.Web.Filter
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 服务访问身份信息
        /// </summary>
        public IdentifyInfo identify;

        /// <summary>
        /// 在Action执行之前由 .NET Core MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            identify = new IdentifyInfo()
            {
                OperatorKeyId = Guid.NewGuid(),
                UserName = "超级用户",
                UserKeyId = Guid.Parse("99999999-9999-9999-9999-999999999999")
            };

            Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity($"{ identify.OperatorKeyId }|{ identify.UserKeyId }|{ identify.UserName }|"), null);

            base.OnActionExecuting(context);
        }

    }
}

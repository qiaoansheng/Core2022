using Core2022.Framework;
using Core2022.Framework.Authorizations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core2022.API.Filters
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = "";
            context.HttpContext.Request.Cookies.TryGetValue(Global.CurrentLoginUserKey, out token);
            if (string.IsNullOrEmpty(token))
            {
                // 未登录
            }
            else
            {
                string json = CodingUtils.AesDecrypt(token);
                Token tokenObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(json);
                ((BaseController)context.Controller).SetUserLoginInfo(tokenObj.UserKeyId, tokenObj.UserName);
            }
            base.OnActionExecuting(context);
        }



    }
}

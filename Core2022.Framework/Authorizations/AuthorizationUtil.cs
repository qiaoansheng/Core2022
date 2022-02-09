using System;
using System.Threading;

namespace Core2022.Framework.Authorizations
{
    public class AuthorizationUtil
    {
        /// <summary>
        /// 操作KeyId
        /// </summary>
        /// <returns></returns>
        public static string GetOperatorKeyId()
        {
            try
            {
                return Thread.CurrentPrincipal.Identity.Name.Split('|')[0].ToString();
            }
            catch
            {
                throw new Exception("无法找到用户登录信息");
            }
        }

        public static Guid GetCurrentUserKeyId()
        {
            try
            {
                return Guid.Parse(Thread.CurrentPrincipal.Identity.Name.Split('|')[1].ToString());
            }
            catch
            {
                throw new Exception("无法找到用户登录信息");
            }
        }

        public static string GetCurrentUserName()
        {
            try
            {
                return Thread.CurrentPrincipal.Identity.Name.Split('|')[2].ToString();
            }
            catch
            {
                throw new Exception("无法找到用户登录信息");
            }
        }
    }
}

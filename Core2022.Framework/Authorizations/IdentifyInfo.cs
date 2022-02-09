using System;

namespace Core2022.Framework.Authorizations
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class IdentifyInfo
    {
        /// <summary>
        /// 操作KeyId
        /// </summary>
        public Guid OperatorKeyId { get; set; }

        /// <summary>
        /// 当前用户keyId
        /// </summary>
        public Guid UserKeyId { get; set; }

        /// <summary>
        /// 当前用户名
        /// </summary>
        public string UserName { get; set; }
    }
}

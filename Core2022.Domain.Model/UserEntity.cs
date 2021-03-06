using Core2022.Framework.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core2022.Domain.Model
{
    [Table("User")]
    public class UserEntity : BaseOrmModel
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

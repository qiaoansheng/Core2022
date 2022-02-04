using Core2022.Framework.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core2022.Domain.Model
{
    [Table("User")]
    public class UserEntity : BaseOrmModel
    {
        public string Test { get; set; } = "测试数据";

    }
}

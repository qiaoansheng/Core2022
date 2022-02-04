using Core2022.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Domain.Model
{
    public class UserEntity : BaseOrmModel
    {

        public Guid KeyId { get; set; }

        public string Test { get; set; } = "测试数据";

    }
}

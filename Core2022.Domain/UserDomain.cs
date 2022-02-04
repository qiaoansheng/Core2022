using Core2022.Domain.Interface;
using Core2022.Domain.Model;
using Core2022.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Domain
{
    [Injection(typeof(IUserDomain))]
    public class UserDomain : IUserDomain
    {
        UserEntity UserEntity;
        public UserDomain(UserEntity entity)
        {
            UserEntity = entity;
        }

        public Guid GetKeyId()
        {
            return UserEntity.KeyId;
        }

        public string GetTest()
        {
            return UserEntity.Test;
        }

        public void SetTest(string text)
        {
            UserEntity.Test = text;
        }

        public void Test()
        {
     

        }

    }
}

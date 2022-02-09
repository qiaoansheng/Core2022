using Core2022.Domain.Interface;
using Core2022.Domain.Model;
using Core2022.Framework.Attributes;
using Core2022.Framework.Domain;
using System;
using System.Threading.Tasks;

namespace Core2022.Domain
{
    //[Injection(typeof(IUserDomain))]
    public class UserDomain : BaseDomain, IUserDomain
    {
        private UserEntity SelfEntity
        {
            get { return this.Entity as UserEntity; }
            set { this.Entity = value; }
        }

        public UserDomain(UserEntity entity)
        {
            this.Entity = entity;
        }

        public UserDomain(string userName)
        {
            this.Entity = new UserEntity()
            {
                KeyId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreateUserKeyId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                UserName = userName
            };
        }

        public string GetUserName()
        {
            return SelfEntity.UserName;
        }

        public void SetUserName(string UserName)
        {
            SelfEntity.UserName = UserName;
        }

        public string GetPassWord()
        {
            return SelfEntity.PassWord;
        }

        public void SetPassWord(string PassWord)
        {
            SelfEntity.PassWord = PassWord;
        }

        public DateTime GetLastLoginTime()
        {
            return SelfEntity.LastLoginTime;
        }

        public void SetLastLoginTime(DateTime LastLoginTime)
        {
            SelfEntity.LastLoginTime = LastLoginTime;
        }

        public IBaseDomain AsBaseDomain()
        {
            return (IBaseDomain)this;
        }
    }
}

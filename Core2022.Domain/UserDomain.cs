using Core2022.Domain.Interface;
using Core2022.Domain.Model;
using Core2022.Framework.Attributes;
using Core2022.Framework.Domain;
using System;

namespace Core2022.Domain
{
    [Injection(typeof(IUserDomain))]
    public class UserDomain : BaseDomain, IUserDomain
    {
        private UserEntity SelfModel
        {
            get { return this.Model as UserEntity; }
            set { this.Model = value; }
        }

        public UserDomain(UserEntity entity)
        {
            this.Model = entity;
        }

        public UserDomain(string userName)
        {
            this.Model = new UserEntity()
            {
                KeyId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreateUserKeyId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                UserName = userName
            };
        }

        public string GetUserName()
        {
            return SelfModel.UserName;
        }

        public void SetUserName(string UserName)
        {
            SelfModel.UserName = UserName;
        }

        public string GetPassWord()
        {
            return SelfModel.PassWord;
        }

        public void SetPassWord(string PassWord)
        {
            SelfModel.PassWord = PassWord;
        }

        public DateTime GetLastLoginTime()
        {
            return SelfModel.LastLoginTime;
        }

        public void SetLastLoginTime(DateTime LastLoginTime)
        {
            SelfModel.LastLoginTime = LastLoginTime;
        }

    }
}

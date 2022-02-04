using Core2022.Framework.Domain;
using System;

namespace Core2022.Domain.Interface
{
    public interface IUserDomain : IBaseDomain
    {
        public string GetUserName();
        public void SetUserName(string UserName);
        public string GetPassWord();
        public void SetPassWord(string PassWord);
        public DateTime GetLastLoginTime();
        public void SetLastLoginTime(DateTime LastLoginTime);

    }
}

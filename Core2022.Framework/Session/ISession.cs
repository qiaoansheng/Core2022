using System;

namespace Core2022.Framework.Session
{
    public interface ISession
    {
        Guid OperatorKeyId { get; }
        Guid CurrentUserKeyId { get; }
        string CurrentUserName { get; }
        void Use(Guid userKeyId, string userName);
    }
}

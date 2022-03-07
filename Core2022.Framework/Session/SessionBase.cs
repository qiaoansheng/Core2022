using System;

namespace Core2022.Framework.Session
{
    public class SessionBase : ISession
    {
        public Guid OperatorKeyId { get; }
        public Guid CurrentUserKeyId { get; }
        public string CurrentUserName { get; }
        public void Use(Guid userKeyId, string userName)
        {

        }
    }
}

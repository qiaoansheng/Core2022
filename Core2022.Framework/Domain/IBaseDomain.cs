using Core2022.Framework.Entity;
using System;

namespace Core2022.Framework.Domain
{
    public interface IBaseDomain
    {
        public BaseOrmModel GetModel();

        public IBaseDomain AsBaseDomain();

        public Guid GetKeyId();
        public Guid GetCreateUserKeyId();
        public void SetCreateUserKeyId(Guid CreateUserKeyId);
        public Guid GetUpdateUserKeyId();
        public void SetUpdateUserKeyId(Guid UpdateUserKeyId);
        public DateTime GetCreateTime();
        public void SetCreateTime(DateTime CreateTime);
        public DateTime GetUpdateTime();
        public void SetUpdateTime(DateTime UpdateTime);
        public int GetVersion();
        public void SetVersion(int Version);
        public bool GetIsDelete();
        public void SetIsDelete(bool IsDelete);
    }
}

using Core2022.Framework.Entity;
using System;

namespace Core2022.Framework.Domain
{
    public class BaseDomain
    {
        protected BaseOrmModel Entity;

        public BaseOrmModel GetModel()
        {
            return Entity;
        }

        public Guid GetKeyId()
        {
            return Entity.KeyId;
        }

        public void SetKeyId(Guid KeyId)
        {
            Entity.KeyId = KeyId;
        }

        public Guid GetCreateUserKeyId()
        {
            return Entity.CreateUserKeyId;
        }

        public void SetCreateUserKeyId(Guid CreateUserKeyId)
        {
            Entity.CreateUserKeyId = CreateUserKeyId;
        }

        public Guid GetUpdateUserKeyId()
        {
            return Entity.UpdateUserKeyId;
        }

        public void SetUpdateUserKeyId(Guid UpdateUserKeyId)
        {
            Entity.UpdateUserKeyId = UpdateUserKeyId;
        }

        public DateTime GetCreateTime()
        {
            return Entity.CreateTime;
        }

        public void SetCreateTime(DateTime CreateTime)
        {
            Entity.CreateTime = CreateTime;
        }

        public DateTime GetUpdateTime()
        {
            return Entity.UpdateTime;
        }

        public void SetUpdateTime(DateTime UpdateTime)
        {
            Entity.UpdateTime = UpdateTime;
        }

        public int GetVersion()
        {
            return Entity.Version;
        }

        public void SetVersion(int Version)
        {
            Entity.Version = Version;
        }

        public bool GetIsDelete()
        {
            return Entity.IsDelete;
        }

        public void SetIsDelete(bool IsDelete)
        {
            Entity.IsDelete = IsDelete;
        }



    }
}

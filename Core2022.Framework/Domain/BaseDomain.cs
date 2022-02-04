using Core2022.Framework.Entity;
using System;

namespace Core2022.Framework.Domain
{
    public class BaseDomain
    {
        protected BaseOrmModel Model;

        public BaseOrmModel GetModel()
        {
            return Model;
        }

        public Guid GetKeyId()
        {
            return Model.KeyId;
        }

        public void SetKeyId(Guid KeyId)
        {
            Model.KeyId = KeyId;
        }

        public Guid GetCreateUserKeyId()
        {
            return Model.CreateUserKeyId;
        }

        public void SetCreateUserKeyId(Guid CreateUserKeyId)
        {
            Model.CreateUserKeyId = CreateUserKeyId;
        }

        public Guid GetUpdateUserKeyId()
        {
            return Model.UpdateUserKeyId;
        }

        public void SetUpdateUserKeyId(Guid UpdateUserKeyId)
        {
            Model.UpdateUserKeyId = UpdateUserKeyId;
        }

        public DateTime GetCreateTime()
        {
            return Model.CreateTime;
        }

        public void SetCreateTime(DateTime CreateTime)
        {
            Model.CreateTime = CreateTime;
        }

        public DateTime GetUpdateTime()
        {
            return Model.UpdateTime;
        }

        public void SetUpdateTime(DateTime UpdateTime)
        {
            Model.UpdateTime = UpdateTime;
        }

        public int GetVersion()
        {
            return Model.Version;
        }

        public void SetVersion(int Version)
        {
            Model.Version = Version;
        }

        public bool GetIsDelete()
        {
            return Model.IsDelete;
        }

        public void SetIsDelete(bool IsDelete)
        {
            Model.IsDelete = IsDelete;
        }

    

    }
}

using Core2022.Domain.Interface;
using Core2022.Framework;
using Core2022.Framework.Authorizations;
using Core2022.Framework.UnitOfWork;
using Core2022.Repository.Interface;
using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Core2022.Application.Services
{
    public class ApplicationServiceBase
    {
        /// <summary>
        /// 服务访问身份信息
        /// </summary>
        //public IdentifyInfo identify;

        public ApplicationServiceBase()
        {
            //identify.OperatorKeyId = Guid.NewGuid();
            //this.identify = identify;
            //Thread.CurrentPrincipal = new GenericPrincipal(
            //    new GenericIdentity($"{ identify.OperatorKeyId }|{ identify.UserKeyId }|{ identify.UserName }|"), null);
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        private IAppUnitOfWork UnitOfWork;

        protected IAppUnitOfWork GetUnitOfWork()
        {
            if (UnitOfWork == null)
            {
                UnitOfWork = AppUnitOfWorkFactory.GetAppUnitOfWorkRepository();
            }
            return UnitOfWork;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await GetUnitOfWork().SaveChangesAsync();
        }

        public IUserRepository UserRepository { get; set; }

        public IUserDomain CreateUserDomain(string userName)
        {
            return Global.GetT<IUserDomain>("userName", userName);
        }

    }
}

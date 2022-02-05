using Core2022.Domain.Interface;
using Core2022.Framework;
using Core2022.Framework.UnitOfWork;
using Core2022.Repository.Interface;

namespace Core2022.Application.Services
{
    public class ApplicationServiceBase
    {

        /// <summary>
        /// 工作单元
        /// </summary>
        private IUnitOfWork UnitOfWork;

        protected IUnitOfWork GetUnitOfWork()
        {
            if (UnitOfWork == null)
            {
                UnitOfWork = AppUnitOfWorkFactory.GetAppUnitOfWorkRepository();
            }
            return UnitOfWork;
        }

        public int SaveChanges()
        {
            return GetUnitOfWork().SaveChanges();
        }

        public IUserRepository UserRepository { get; set; }

        public IUserDomain CreateUserDomain(string userName)
        {
            return Global.GetT<IUserDomain>("userName", userName);
        }

    }
}

using Core2022.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Domain.Interface
{
    public interface IUserDomain : IBaseDomain
    {
        public Guid GetKeyId();

        public string GetTest();

        public void SetTest(string text);

        public void Test();
    }
}

using Castle.DynamicProxy;
using Core2022.Framework.Authorizations;
using Core2022.Framework.Domain;
using Core2022.Framework.Entity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core2022.Framework
{
    public class AutofacFilter : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            DateTime startTime = DateTime.Now;
            //objs 是当前拦截方法的入参
            object[] objs = invocation.Arguments;
            invocation.Proceed();
            // ret 是当前方法的返回值
            object ret = invocation.ReturnValue;
            DateTime endTime = DateTime.Now;

            if (invocation.Method.CustomAttributes?.FirstOrDefault(i => i.AttributeType.Name == "AOPLogAttribute") != null)
            {
                if (invocation.Method.Name == "FindAsync")
                {
                    var taskType = ret.GetType();

                    IBaseDomain domain = (IBaseDomain)taskType.GetProperty("Result").GetValue(ret);

                    BaseOrmModel model = domain.GetModel();

                    DBLogSeleteModel logModel = new DBLogSeleteModel()
                    {
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        StartTimer = startTime,
                        EndTimer = endTime,
                        TakeTime = (endTime - startTime).TotalMilliseconds.ToString(),
                        ReturnValue = JsonConvert.SerializeObject(model),
                        OperatorKeyId = AuthorizationUtil.GetOperatorKeyId(),
                        OperatorUserkeyId = AuthorizationUtil.GetCurrentUserKeyId(),
                        OperatorUserName = AuthorizationUtil.GetCurrentUserName(),
                    };
                    DBLog.WriteLog(logModel);
                }

                if (invocation.Method.Name == "Find")
                {
                    IBaseDomain domain = (IBaseDomain)ret;
                    BaseOrmModel model = domain.GetModel();

                    DBLogSeleteModel logModel = new DBLogSeleteModel()
                    {
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        StartTimer = startTime,
                        EndTimer = endTime,
                        TakeTime = (endTime - startTime).TotalMilliseconds.ToString(),
                        ReturnValue = JsonConvert.SerializeObject(model),
                        OperatorKeyId = AuthorizationUtil.GetOperatorKeyId(),
                        OperatorUserkeyId = AuthorizationUtil.GetCurrentUserKeyId(),
                        OperatorUserName = AuthorizationUtil.GetCurrentUserName(),
                    };
                    DBLog.WriteLog(logModel);
                }
            }
        }
    }
}

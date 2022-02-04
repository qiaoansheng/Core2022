using Castle.DynamicProxy;
using System;
using System.Linq;

namespace Core2022.Framework.UnitOfWork
{
    public class AutofacAOP : IInterceptor
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
                if (invocation.Method.Name == "Find")
                {
                  
                }
            }
        }
    }
}

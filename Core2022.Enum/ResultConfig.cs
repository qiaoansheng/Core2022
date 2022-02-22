using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Enum
{
    public static class ResultConfig
    {

        public static int OK = 1;
        public static string SuccessfulMessage = "操作成功";


        public static int Fail = 0;
        public static string FailMessage = "操作失败";


        public static int NotPower = 2;
        public static string FailMessageNotPower = "没有权限，操作失败";


        public static int NotSystem = 3;
        public static string FailMessageNotSystem = "系统错误，操作失败";


    }
}

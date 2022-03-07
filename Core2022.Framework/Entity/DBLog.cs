using Core2022.Framework.Nlog;
using Newtonsoft.Json;

namespace Core2022.Framework.Entity
{
    public static class DBLog
    {

        public static void WriteLog(DBLogBaseModel log)
        {
            LogHelper.Info("DBLog", JsonConvert.SerializeObject(log));
        }

    }
}

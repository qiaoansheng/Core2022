using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core2022.Framework.Entity
{
    public static class DBLog
    {
        static Queue<string> queue_log = new Queue<string>();
        static Queue<DBLogBaseModel> queue_log_model = new Queue<DBLogBaseModel>();
        static object DequeueModelLock = new object();
        static object EnqueueModelLock = new object();

        static DBLog()
        {
            Task.Run(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                string logPath = AppContext.BaseDirectory + "EFLog.txt";
                /// *** 主线程结束后当前线程也会结束，
                /// *** 简单粗暴的把他设置成一个后台线程，虽然不会退出了，但是这是个无限循环的方法
                /// *** 所以这里需要手动控制一下主线程结束后，队列中没有数据了在结束该线程
                while (true)
                {
                    while (queue_log.Count > 0)
                    {
                        string log = queue_log.Dequeue();
                        AppendAllLineText(logPath, log);
                    }
                    while (queue_log_model.Count > 0)
                    {
                        DBLogBaseModel log = DequeueModel();
                        if (log != null)
                        {
                            log.OptionType = log.GetType().Name;
                            AppendAllLineText(logPath, JsonConvert.SerializeObject(log));
                        }
                    }
                    Thread.Sleep(100);
                }
            });
        }

        public static void WriteLog(string log)
        {
            queue_log.Enqueue(log);

        }

        public static void WriteLog(DBLogBaseModel log)
        {
            lock (EnqueueModelLock)
            {
                queue_log_model.Enqueue(log);
            }
        }

        private static DBLogBaseModel DequeueModel()
        {
            lock (DequeueModelLock)
            {
                return queue_log_model.Dequeue();
            }
        }

        /// <summary>
        /// 另起一行在把文本内容追加到后面
        /// </summary>
        /// <param name="path"></param>
        /// <param name="textContent"></param>
        public static void AppendAllLineText(string path, string textContent)
        {
            File.AppendAllText(path, $" \t\n{ textContent }");
        }

    }



}

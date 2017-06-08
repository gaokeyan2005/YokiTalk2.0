using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Yoki.Comm.Object;

namespace Yoki.Comm
{
    public class LogUtil : IDisposable
    {
        static LogUtil()
        {

        }

        private static System.IO.StreamWriter _sw = null;
        private static System.IO.StreamWriter SW
        {
            get
            {
                if (_sw == null)
                {
                    string path = System.Windows.Forms.Application.StartupPath + @"\YokiLogs\";
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    string filePath = path + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
                    System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                    _sw = new System.IO.StreamWriter(fs);
                }
                return _sw;
            }

        }

        public static void WriteLine(string log)
        {
            lock (SW)
            {
                SW.Flush();
                SW.WriteLine(log);
            }
        }

        public void Dispose()
        {
            if (SW != null)
            {
                SW.Close();
            }
        }
        /// <summary>
        /// 本地记录操作日志
        /// </summary>
        /// <param name="strType">操作类型：
        //Select = 0,//查询
        //Insert = 1,//写入
        //Update = 2,//更新
        //Delete=3,//删除
        //Save = 4,//保存
        //Close=5,//关闭或退出
        //Answer=6//接听或应答</param>
        /// <param name="strLog">操作内容</param>
        public static void WriteLogOperation(LogType strType, string strLog)
        {
            if (!Directory.Exists(Application.StartupPath + @"\YokiLogs\LogOperation"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\YokiLogs\LogOperation");
            }
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\YokiLogs\LogOperation\operationlog" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
            {
                string log = string.Format("\"operation_time\": \"{0}\",\"computername\": \"{1}\",\"account\": \"{2}\",\"operation_type\": \"{3}\",\"operation_log\": \"{4}\"", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Dns.GetHostName(), CommUserInfo.Account, strType, strLog);
                log = "{" + log + "},";
                sw.WriteLine(log);
                sw.Close();
            }
        }
        /// <summary>
        /// 本地记录异常错误日志
        /// </summary>
        /// <param name="strSource">源</param>
        /// <param name="strMessage">错误消息</param>
        public static void WriteLogError(string strSource, string strMessage)
        {
            if (!Directory.Exists(Application.StartupPath + @"\YokiLogs\LogError"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\YokiLogs\LogError");
            }
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\YokiLogs\LogError\errorlog" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
            {
                string log = string.Format("\"error_time\": \"{0}\",\"computername\": \"{1}\",\"account\": \"{2}\",\"error_source\": \"{3}\",\"error_message\": \"{4}\"", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Dns.GetHostName(), CommUserInfo.Account, strSource, strMessage);
                log = "{" + log + "},";
                sw.WriteLine(log);
                sw.Close();
            }
        }
    }
}

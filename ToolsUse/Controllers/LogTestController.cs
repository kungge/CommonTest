using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ToolsUse.Controllers
{
    public class LogTestController : Controller
    {
        // GET: LogTest
        public ActionResult Index()
        {
            return View();
        }

        //refer：http://www.cnblogs.com/s0611163/p/4023859.html
        public ActionResult LogWan()
        {
            //string p = TestPath.path;
            //string pp=TestPath.CreateLogPath();
            //lock (new object())
            //{

            //}

            //TestWriteLog
            int n = 10000;
            DateTime dtStart = DateTime.Now;
            for (int i = 1; i <= n; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object obj)
                {
                    int j = (int)obj;
                    LogUtil.Log("测试" + j.ToString());

                    if (j == n)
                    {
                        double sec = DateTime.Now.Subtract(dtStart).TotalSeconds;
                        //MessageBox.Show(n + "条日志完成，耗时" + sec.ToString("0.000") + "秒");
                        string msgStr = n + "条日志完成，耗时" + sec.ToString("0.000") + "秒";
                        LogUtil.Log("--本次写完，" + msgStr);
                    }
                }), i);
            }
            return View();
        }
    }

    #region LogWan 辅助类
    /// <summary>
    /// 写日志类
    /// </summary>
    public class LogUtil
    {
        #region 字段
        public static object _lock = new object();
        public static string path = "/upload/logFile";
        //public static string path = ConfigurationManager.AppSettings["LogPath"];
        //public static string path = HttpContext.Current.Request.PhysicalApplicationPath+ ConfigurationManager.AppSettings["LogPath"];
        public static int fileSize = 10 * 1024 * 1024; //日志分隔文件大小
        #endregion

        #region 写文件
        /// <summary>
        /// 写文件
        /// </summary>
        public static void WriteFile(string log, string path)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                if (!File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create)) { fs.Close(); }
                }

                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        #region 日志内容
                        string value = string.Format(@"{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), log);
                        #endregion

                        sw.WriteLine(value);
                        sw.Flush();
                    }
                    fs.Close();
                }
            }
            catch { }
        }
        #endregion

        #region 生成日志文件路径
        /// <summary>
        /// 生成日志文件路径
        /// </summary>
        public static string CreateLogPath()
        {
            int index = 0;
            string logPath;
            bool bl = true;
            do
            {
                index++;
                logPath = Path.Combine(path, "Log" + DateTime.Now.ToString("yyyyMMdd") + (index == 1 ? "" : "_" + index.ToString()) + ".txt");
                if (File.Exists(logPath))
                {
                    FileInfo fileInfo = new FileInfo(logPath);
                    if (fileInfo.Length < fileSize)
                    {
                        bl = false;
                    }
                }
                else
                {
                    bl = false;
                }
            } while (bl);

            return logPath;
        }
        #endregion

        #region 写错误日志
        /// <summary>
        /// 写错误日志
        /// </summary>
        public static void LogError(string log)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_lock)
                {
                    WriteFile("[Error] " + log, CreateLogPath());
                }
            });
        }
        #endregion

        #region 写操作日志
        /// <summary>
        /// 写操作日志
        /// </summary>
        public static void Log(string log)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_lock)
                {
                    WriteFile("[Info]  " + log, CreateLogPath());
                }
            });
        }
        #endregion

    }

    #endregion

    #region otherTest
    public class TestPath
    {
        public static int fileSize = 10 * 1024 * 1024;
        public static string path = HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["LogPath"];
        public static string CreateLogPath()
        {
            int index = 0;
            string logPath;
            bool bl = true;
            do
            {
                index++;
                logPath = Path.Combine(path, "Log" + DateTime.Now.ToString("yyyyMMdd") + (index == 1 ? "" : "_" + index.ToString()) + ".txt");
                if (File.Exists(logPath))
                {
                    FileInfo fileInfo = new FileInfo(logPath);
                    if (fileInfo.Length < fileSize)
                    {
                        bl = false;
                    }
                }
                else
                {
                    bl = false;
                }
            } while (bl);

            return logPath;
        }
    }
    #endregion
}
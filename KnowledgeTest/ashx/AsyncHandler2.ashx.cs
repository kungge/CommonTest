using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace KnowledgeTest.ashx
{
    /// <summary>
    /// AsyncHandler2 的摘要说明
    /// </summary>
    public class AsyncHandler2 : IHttpHandler
    {
        //refer：http://www.cnblogs.com/wisdomqq/archive/2012/03/29/2417723.html
        public void ProcessRequest(HttpContext context)
        {
            DateTime begin = DateTime.Now;
            int t1, t2, t3;
            ThreadPool.GetAvailableThreads(out t1, out t3);
            ThreadPool.GetMaxThreads(out t2, out t3);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            DateTime end = DateTime.Now;
            context.Response.ContentType = "text/plain";
            var thread = Thread.CurrentThread;
            context.Response.Write(
                string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
                    thread.ManagedThreadId,
                    context.ApplicationInstance.GetHashCode(),
                    begin,
                    end,
                    t2 - t1
                    )
                );
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
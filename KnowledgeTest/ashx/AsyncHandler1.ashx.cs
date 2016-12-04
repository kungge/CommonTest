using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace KnowledgeTest.ashx
{
    /// <summary>
    /// AsyncHandler1 的摘要说明
    /// </summary>
    public class AsyncHandler1 : IHttpHandler
    {
        //refer：http://www.cnblogs.com/wisdomqq/archive/2012/03/29/2417723.html
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var thread = Thread.CurrentThread;
            context.Response.Write(
                string.Format("Name:{0}\r\nManagedThreadId:{1}\r\nIsBackground:{2}\r\nIsThreadPoolThread:{3}",
                    thread.Name,
                    thread.ManagedThreadId,
                    thread.IsBackground,
                    thread.IsThreadPoolThread)
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
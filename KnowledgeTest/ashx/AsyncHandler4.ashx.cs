using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KnowledgeTest.ashx
{
    /// <summary>
    /// AsyncHandler4 的摘要说明
    /// </summary>
    public class AsyncHandler4 : IHttpHandler,IHttpAsyncHandler
    {
        //refer：http://www.cnblogs.com/wisdomqq/archive/2012/03/29/2417723.html
        //同步方法
        public void ProcessRequest(HttpContext context)
        {
            FileStream fs = new FileStream("", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous);
            fs.CopyTo(context.Response.OutputStream);
        }

        public bool IsReusable
        {
            //设置允许重用对象
            get { return false; }
        }

        //异步方法开始
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            FileStream fs = new FileStream("E:\\jobFiles\\myproj\\online\\CommonTest\\KnowledgeTest\\asyncTest.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous);
            var task = fs.CopyToAsync(context.Response.OutputStream);
            task.GetAwaiter().OnCompleted(() => cb(task));
            return task;
        }

        //异步方法结束
        public void EndProcessRequest(IAsyncResult result)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace KnowledgeTest.ashx
{
    /// <summary>
    /// AsyncHandler3 的摘要说明
    /// </summary>
    public class AsyncHandler3 : IHttpHandler,IHttpAsyncHandler
    {
        //refer：http://www.cnblogs.com/wisdomqq/archive/2012/03/29/2417723.html
        public void ProcessRequest(HttpContext context)
        {
            //异步处理器不执行该方法
        }

        public bool IsReusable
        {
            //设置允许重用对象
            get { return false; }
        }

        //请求开始时由ASP.NET调用此方法
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write("App:");
            context.Response.Write(context.ApplicationInstance.GetHashCode());
            context.Response.Write("\tBegin:");
            context.Response.Write(DateTime.Now.ToString("mm:ss,ffff"));
            //输出当前线程
            context.Response.Write("\tThreadId:");
            context.Response.Write(Thread.CurrentThread.ManagedThreadId);
            //构建异步结果并返回
            var result = new WebAsyncResult(cb, context);
            //用一个定时器来模拟异步触发完成
            Timer timer = null;
            timer = new Timer(o =>
            {
                result.SetComplete();
                timer.Dispose();
            }, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return result;
        }

        //异步结束时，由ASP.NET调用此方法
        public void EndProcessRequest(IAsyncResult result)
        {
            WebAsyncResult webresult = (WebAsyncResult)result;
            webresult.Context.Response.Write("\tEnd:");
            webresult.Context.Response.Write(DateTime.Now.ToString("mm:ss,ffff"));
            //输出当前线程
            webresult.Context.Response.Write("\tThreadId:");
            webresult.Context.Response.Write(Thread.CurrentThread.ManagedThreadId);
        }

        //WEB异步方法结果
        class WebAsyncResult : IAsyncResult
        {
            private AsyncCallback _callback;

            public WebAsyncResult(AsyncCallback cb, HttpContext context)
            {
                Context = context;
                _callback = cb;
            }

            //当异步完成时调用该方法
            public void SetComplete()
            {
                IsCompleted = true;
                if (_callback != null)
                {
                    _callback(this);
                }
            }

            public HttpContext Context
            {
                get;
                private set;
            }

            public object AsyncState
            {
                get { return null; }
            }

            //由于ASP.NET不会等待WEB异步方法，所以不使用此对象
            public WaitHandle AsyncWaitHandle
            {
                get { throw new NotImplementedException(); }
            }

            public bool CompletedSynchronously
            {
                get { return false; }
            }

            public bool IsCompleted
            {
                get;
                private set;
            }
        }
    }
}
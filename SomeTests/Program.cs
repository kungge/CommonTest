using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SomeTests
{
    class Program
    {
        private static bool _isDone = false;
        public static object _lock = new object();
        static SemaphoreSlim _sem = new SemaphoreSlim(3);//限制能同时访问的线程数量
        static void Main(string[] args)
        {
            #region 测试asp.net 异步
            //***********************************************测试asp.net 异步*************************************************************
            //refer：http://www.cnblogs.com/wisdomqq/archive/2012/03/29/2417723.html
            //var url = new Uri("http://localhost:32499/ashx/AsyncHandler2.ashx");
            //var url = new Uri("http://localhost:32499/ashx/AsyncHandler3.ashx");
            //var num = 50;
            //for (int i = 0; i < num; i++)
            //{
            //    var request = WebRequest.Create(url);
            //    request.GetResponseAsync().ContinueWith(t =>
            //    {
            //        var stream = t.Result.GetResponseStream();
            //        using (TextReader tr = new StreamReader(stream))
            //        {
            //            Console.WriteLine(i.ToString()+"："+tr.ReadToEnd());
            //        }
            //    });
            //}
            #endregion

            #region jesse2013异步编程系列_async & await 的前世今生（Updated） 
            //refer：http://www.cnblogs.com/jesse2013/p/async-and-await.html
            ////****************************************创建线程********************
            //Console.WriteLine("Mian Start threadId={0}  {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //new Thread(Go).Start();// .NET 1.0开始就有的 
            //Task.Factory.StartNew(Go);// .NET 4.0 引入了 TPL
            //Task.Run(new Action(Go));// .NET 4.5 新增了一个Run的方法
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            ////****************************************线程池********************
            //Console.WriteLine("Mian Start，我是主线程：Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //ThreadPool.QueueUserWorkItem(Go);
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            ////****************************************传入参数********************
            //Console.WriteLine("Mian Start {0}",DateTime.Now.ToString("mm:ss.ffff"));
            //new Thread(Go).Start("arg1");// 没有匿名委托之前，我们只能这样传入一个object的参数
            //new Thread(delegate() {//有了匿名委托
            //    GoGoGo("arg11","arg12","arg13");
            //});
            //new Thread(()=> {//Lambada
            //    GoGoGo("arg111", "arg112", "arg113");
            //}).Start();
            //Task.Run(() => {// Task能这么灵活，也是因为有了Lambda呀
            //    GoGoGo("arg1111", "arg1112", "arg1113");
            //});
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            ////****************************************返回值********************
            //Console.WriteLine("Mian Start   Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //var rtn = Task.Run<string>(() => {
            //    Console.WriteLine("Task.Run Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //    return RtnSomeThing();
            //});
            //Console.WriteLine("after Task.Run Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //Console.WriteLine("返回值={0} Thread Id={1}  {2}", rtn.Result, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            //****************************************共享数据 * *******************
            //new Thread(Done).Start();
            //new Thread(Done).Start();
            //*********************************


            //****************************************线程安全********************
            new Thread(Done).Start();
            new Thread(Done).Start();
            //*********************************


            //****************************************锁********************
            //new Thread(Done).Start();
            //new Thread(Done).Start();
            //*********************************


            //****************************************Semaphore 信号量********************
            //for (int i = 1; i <= 5; i++)
            //{
            //    new Thread(Enter).Start(i);
            //}
            //*********************************


            ////****************************************异常处理********************
            ////try
            ////{
            ////    new Thread(Go).Start();
            ////}
            ////catch (Exception ex)
            ////{
            ////    // 其它线程里面的异常，我们这里面是捕获不到的。
            ////    Console.WriteLine("Exception!");
            ////}

            //try
            //{
            //    //var task=Task.Run(()=> { Go(); });
            //    //task.Wait();// 在调用了这句话之后，主线程才能捕获task里面的异常

            //    // 对于有返回值的Task, 我们接收了它的返回值就不需要再调用Wait方法了
            //    // GetName 里面的异常我们也可以捕获到
            //    var task2 = Task.Run(() => { return GetName(); });
            //    var r = task2.Result;
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("Exception!");
            //}
            ////*********************************


            //****************************************一个小例子认识async & await********************
            //Console.WriteLine("Mian Start，Current Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //Test();// 这个方法其实是多余的, 本来可以直接写下面的方法
            //// await GetName()  
            //// 但是由于控制台的入口方法不支持async,所有我们在入口方法里面不能 用 await
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));



            //Console.WriteLine("Main Thread Id: {0}  {1}\r\n", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //TestV2();
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            //*********************************

            ////*************************************只有async方法在调用前才能加await么？
            //Console.WriteLine("Main Thread Id: {0}  {1}\r\n", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //TestV3();
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            ////*************************************不用await关键字，如何确认Task执行完毕了？
            //Console.WriteLine("Main Thread Id: {0}  {1} \r\n ", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //var task = Task.Run(() => {
            //    Console.WriteLine("Task.Run currentThreadId={0}  {1}", Thread.CurrentThread.ManagedThreadId,DateTime.Now.ToString("mm:ss.ffff"));
            //    return GetNameV4();
            //});

            //task.GetAwaiter().OnCompleted(()=> {
            //    Console.WriteLine("task.GetAwaiter().OnCompleted currentThreadId={0}  {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //    var name = task.Result;
            //    Console.WriteLine("My name is {0} currentThreadId={1}  {2}", name, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //});
            //Console.WriteLine("Mian End {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            //*************************************Task如何让主线程挂起等待？
            //Console.WriteLine("Main Thread Id: {0}  {1} \r\n ", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //var task = Task.Run(() => {
            //    Console.WriteLine("Task.Run currentThreadId={0}  {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //    return GetNameV4();
            //});
            //var rtnStr=task.GetAwaiter().GetResult();
            //Console.WriteLine("rtnStr={0} currentThreadId={1}  {2}", rtnStr, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //Console.WriteLine("Mian End 主线程执行完毕 {0}", DateTime.Now.ToString("mm:ss.ffff"));
            //*********************************


            ////*************************************await 实质是在调用awaitable对象的GetResult方法
            //Console.WriteLine("Main Thread Id: {0}  {1} \r\n ", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //TestV5();

            //Console.WriteLine("Mian End 主线程执行完毕 {0}", DateTime.Now.ToString("mm:ss.ffff"));
            ////*********************************


            #endregion


            Console.ReadLine();
        }

        //*************************************await 实质是在调用awaitable对象的GetResult方法
        static async Task TestV5()
        {
            Console.WriteLine("TestV5 start currentThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));

            Task<string> task = Task.Run(() => {
                Console.WriteLine("另一个线程在运行！Task.Run()  start currentThreadId={0} {1} ", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));  // 这句话只会被执行一次
                Thread.Sleep(2000);
                Console.WriteLine("Task.Run() end  currentThreadId={0} {1} ", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));  // 这句话只会被执行一次
                return "Hello World";
            });

            //Console.WriteLine("before task.GetAwaiter().GetResult() currentThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            //// 这里主线程会挂起等待，直到task执行完毕我们拿到返回结果
            //var rtnStr = task.GetAwaiter().GetResult();
            //Console.WriteLine("after task.GetAwaiter().GetResult() currentThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));

            Console.WriteLine("before await task currentThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            // 这里不会挂起等待，因为task已经执行完了，我们可以直接拿到结果
            var rtnStr2 = await task;
            Console.WriteLine("after await task currentThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));


            Console.WriteLine("TestV5 end rtnStr={0} currentThreadId={1} {2} ", rtnStr2, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
        }
        //*********************************

        //*************************************不用await关键字，如何确认Task执行完毕了？
        static string GetNameV4()
        {
            Console.WriteLine("GetNameV4 start currentThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            Console.WriteLine("另外一个线程在获取名称");
            Thread.Sleep(2000);
            Console.WriteLine("GetNameV4 end  {0} ", DateTime.Now.ToString("mm:ss.ffff"));
            return "nice";
        }
        //*********************************


        //*************************************只有async方法在调用前才能加await么？
        static async void TestV3()
        {
            Console.WriteLine("TestV3 start {0}", DateTime.Now.ToString("mm:ss.ffff"));
            Task<string> task = Task.Run(() => {
                Thread.Sleep(5000);
                return "date1117";
            });
            string str = await task;//5 秒之后才会执行这里
            Console.WriteLine("TestV3 end TestV3_str= {0} {1}", str,DateTime.Now.ToString("mm:ss.ffff"));
        }
        //*********************************

        //****************************************一个小例子认识async & await********************
        static async Task TestV2()
        {
            Console.WriteLine("Before calling GetNameV2, Thread Id: {0}  {1}\r\n", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            var rtnName = GetNameV2();  //我们这里没有用 await,所以下面的代码可以继续执行
            // 但是如果上面是 await GetName()，下面的代码就不会立即执行，输出结果就不一样了。
            Console.WriteLine("End calling GetName.  {0}\r\n", DateTime.Now.ToString("mm:ss.ffff"));
            Console.WriteLine("Get result from GetName: {0} {1}", await rtnName, DateTime.Now.ToString("mm:ss.ffff"));
        }
        static async Task<string> GetNameV2()
        {
            // 这里还是主线程
            Console.WriteLine("Before calling Task.Run, current thread Id is: {0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            return await Task.Run(()=> {
                Thread.Sleep(1000);
                Console.WriteLine("'GetNameV2' Thread Id: {0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
                return "date1117";
            });
        }

        static async Task GetName()
        {
            Console.WriteLine("GetName Start {0}", DateTime.Now.ToString("mm:ss.ffff"));
            await Task.Delay(1000);
            Console.WriteLine("Current Thread Id :{0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            Console.WriteLine("In antoher thread.....");
            Console.WriteLine("GetName End {0}", DateTime.Now.ToString("mm:ss.ffff"));
        }
        static async Task Test()
        {
            // 方法打上async关键字，就可以用await调用同样打上async的方法
            // await 后面的方法将在另外一个线程中执行
            await GetName();
        }
        //*********************************


        ////****************************************异常处理********************
        //static void Go() { throw null; }
        //static string GetName() { throw null; }
        ////*********************************


        //****************************************Semaphore 信号量********************
        static void Enter(object id)
        {
            Console.WriteLine("{0} 开始排队...{1}",id, DateTime.Now.ToString("mm:ss.ffff"));
            _sem.Wait();
            Console.WriteLine("{0} 开始执行...{1}", id, DateTime.Now.ToString("mm:ss.ffff"));
            Thread.Sleep(1000*(int)id);
            Console.WriteLine("{0} 执行完毕，离开！...{1}", id, DateTime.Now.ToString("mm:ss.ffff"));
            _sem.Release();
        }
        //*********************************


        ////****************************************锁********************
        //static void Done()
        //{
        //    lock (_lock)
        //    {
        //        if (!_isDone)
        //        {
        //            Console.WriteLine("Done {0}", DateTime.Now.ToString("mm:ss.ffff"));// 猜猜这里面会被执行几次？
        //            _isDone = true;
        //        }
        //    }

        //}
        ////*********************************


        //****************************************线程安全********************
        static void Done()
        {
            if (!_isDone)
            {
                Console.WriteLine("Done ThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));// 猜猜这里面会被执行几次？
                _isDone = true;
            }
        }
        //*********************************

        //****************************************共享数据********************
        //static void Done()
        //{
        //    if (!_isDone)
        //    {
        //        _isDone = true;// 第二个线程来的时候，就不会再执行了(也不是绝对的，取决于计算机的CPU数量以及当时的运行情况)
        //        Console.WriteLine("Done ThreadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
        //    }
        //}
        //*********************************

        //****************************************返回值********************
        public static string RtnSomeThing()
        {
            Console.WriteLine("RtnSomeThing start  Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            Console.WriteLine("RtnSomeThing 数据处理中...  Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            Thread.Sleep(3000);
            Console.WriteLine("RtnSomeThing 数据处理完毕  Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
            return "Hello Boy！";
        }
        //*********************************


        //****************************************创建线程*********************
        //public static void Go()
        //{
        //    Console.WriteLine("我是另一个线程 start threadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
        //    Console.WriteLine("我是另一个线程 正在执行任务...... threadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
        //    Thread.Sleep(2000);
        //    Console.WriteLine("我是另一个线程 end threadId={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
        //}
        //*********************************


        //****************************************线程池******************************
        public static void Go(object data)
        {
            Console.WriteLine("我是另一个线程，Thread Id={0} {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("mm:ss.ffff"));
        }
        //*********************************


        //****************************************传入参数********************
        //public static void Go(object name)
        //{
        //    // TODO
        //    Console.WriteLine("Go {0} {1}",name, DateTime.Now.ToString("mm:ss.ffff"));
        //}

        //public static void GoGoGo(string arg1, string arg2, string arg3)
        //{
        //    // TODO
        //    Console.WriteLine("GoGoGo {0} {1} {2} {3}", arg1, arg2, arg3,DateTime.Now.ToString("mm:ss.ffff"));
        //}
        //*********************************
    }
}

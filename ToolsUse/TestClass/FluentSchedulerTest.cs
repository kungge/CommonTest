using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsUse.CommonHelper;

namespace ToolsUse.TestClass
{
    public class FluentSchedulerTest: Registry
    {
        public FluentSchedulerTest()
        {
            //Schedule<TestJob>().ToRunNow().AndEvery(5).Seconds();
        }
    }

    public static class FluentSchedulerTest2
    {
        public static void WriteTxt(string path)
        {
            FileHelper.SaveTxtFile(path, "FSTxtTest.txt", DateTime.Now.ToString("yyyyMMddHHmmss")+"\r\n", false);
        }

        public static void StartWriteTxtJob(string path)
        {
            Registry registry = new Registry();
            registry.Schedule(() => WriteTxt(path)).WithName("WriteTxt").ToRunNow().AndEvery(5).Seconds();
            JobManager.Initialize(registry);
        }
        public static void EndWriteTxtJob(string path)
        {
            FileHelper.SaveTxtFile(path, "FSTxtTest.txt", "JobEnd", false);
            JobManager.RemoveJob("WriteTxt");
        }

        public static void EndJob(string jobName)
        {
            JobManager.RemoveJob(jobName);
        }
    }
    public class TestJob : IJob
    {
        public void Execute()
        {
            Trace.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }

    public static class WriteTxtTest
    {
        public static void WriteTxt1(string path)
        {
            FileHelper.SaveTxtFile(path,"FSTxtTest.txt",DateTime.Now.ToString("yyyyMMddHHmmss"),false);
        }
    }
}
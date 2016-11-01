using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ToolsUse.TestClass
{
    public class FluentSchedulerTest: Registry
    {
        public FluentSchedulerTest()
        {
            Schedule<MyJob>().ToRunNow().AndEvery(2).Seconds();
        }
    }
    public class MyJob : IJob
    {
        public void Execute()
        {
            
        }
    }
}
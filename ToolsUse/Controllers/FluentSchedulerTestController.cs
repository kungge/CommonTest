using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsUse.TestClass;

namespace ToolsUse.Controllers
{
    public class FluentSchedulerTestController : Controller
    {
        // GET: FluentSchedulerTest
        public ActionResult Index()
        {
            JobManager.Initialize(new FluentSchedulerTest());

            return View();
        }
    }
}
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
            return View();
        }

        [HttpPost]
        public JsonResult StartJob()
        {
            FluentSchedulerTest2.StartWriteTxtJob(Server.MapPath("/upload/testfile/"));
            return Json("");
        }

        [HttpPost]
        public JsonResult EndJob()
        {
            FluentSchedulerTest2.EndWriteTxtJob(Server.MapPath("/upload/testfile/"));
            return Json("");
        }

    }
}
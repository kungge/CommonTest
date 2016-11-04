using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToolsUse.Controllers
{
    //发布：.NET开发人员必备的可视化调试工具（你值的拥有） http://www.cnblogs.com/cyq1162/p/6027051.html
    //kv：todo 未测试完
    public class CYQVisualierController : Controller
    {
        // GET: CYQVisualier
        public ActionResult Index()
        {

            string str="{\"resultType\":\"Success\",\"message\":\"130****4360\",\"success\":true}";

            return View(str);
        }
    }
}
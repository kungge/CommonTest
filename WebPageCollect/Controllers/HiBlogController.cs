using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPageCollect.CommonHelp;

namespace WebPageCollect.Controllers
{
    public class HiBlogController : Controller
    {
        // GET: HiBlog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimulateLogin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SimulateLogin(int id=1)
        {
            string uname = "kungge";
            string pwd = "356352";
            string result = "";
            bool rtnBool = HiBlogHelper.LoginHiBlog(uname: uname, pwd: pwd,result: result);
            return Json(rtnBool);
        }
    }
}
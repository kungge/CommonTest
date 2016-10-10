using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPageCollect.CommonHelp;

namespace WebPageCollect.Controllers
{
    public class LianTongController : Controller
    {
        // GET: LianTong
        public ActionResult Index()
        {
           var rtn=LianTongHelper.LoginLianTong2("","","");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("loginResult",rtn?"Login Success":"Login fail");
            return View(dic);
        }

        [HttpPost]
        public JsonResult GetDetailInfo()
        {
            LianTongHelper.GetDetailInfo("");
            return Json("");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetTech.Controllers
{
    public class ApiTestController : Controller
    {
        // GET: ApiTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestGuestV1()
        {
            return View();
        }

        public ActionResult TestGuestV2()
        {
            return View();
        }

        #region 调用apiProj
        public ActionResult TestProductV1()
        {
            return View();
        }
        #endregion
    }
}
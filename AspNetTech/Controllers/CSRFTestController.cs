using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AspNetTech.Controllers
{
    public class CSRFTestController : Controller
    {
        // GET: CSRFTest
        public ActionResult Index()
        {
            //AntiForgery
            return View();
        }
    }
}
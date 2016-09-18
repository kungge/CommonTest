using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebPageCollect.CommonHelp;

namespace WebPageCollect.Controllers
{
    public class JiedaiWController : Controller
    {
        // GET: JiedaiW
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimulateLogin()
        {
            ViewBag.AuthImgUrl = GetJiedaiAuthImgUrl();
            return View();
        }

        [HttpPost]
        public JsonResult SimulateLogin(string Captcha)
        {
            string uname = "13041694360";
            string pwd = "123456";
            string result = "";
            bool rtnBool=JiedaiWHelper.LoginJiedaiW(uname:uname, pwd:pwd,authCode:Captcha, result: result);
            return Json(rtnBool);
        }

        #region 信息获取
        private string GetJiedaiAuthImgUrl()
        {
            string url = "http://www.jiedai.cn/auth/login";
            string authimg_url = "";
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url)).Result;
            string str = response.Content.ReadAsStringAsync().Result;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            HtmlNode node_authimg = doc.DocumentNode.SelectSingleNode("//img[@id='authimg']");
            if (node_authimg != null && node_authimg.Attributes.Count > 0)
            {
                authimg_url = "http://www.jiedai.cn" + new Regex("src=\"(.*?)\" style=\"height: 38px;\" id=\"authimg\" ").Match(str).Groups[1].Value;
            }
            return authimg_url;
        }
        #endregion
    }
}
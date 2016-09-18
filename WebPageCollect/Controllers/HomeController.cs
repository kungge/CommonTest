using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace WebPageCollect.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string authimg_url = GetJiedaiAuthImgUrl();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseImg = httpClient.GetAsync(new Uri(authimg_url)).Result;
            byte[] bArray= responseImg.Content.ReadAsByteArrayAsync().Result;
            string filePath = Server.MapPath("/Upload/images/") + "jiedaiwAuthImg.jpg";
            System.IO.File.WriteAllBytes(filePath,bArray);

            return View();
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
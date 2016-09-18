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
    public class CnblogsController : Controller
    {
        // GET: Cnblogs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTuiJianBlog()
        {
            string tuijianUrl = "http://www.cnblogs.com/aggsite/ExpertBlogs";//kwan_wonder 怎么知道这个地址是取推荐博客的
            HtmlDocument doc = new HtmlDocument();

            //获取post请求的返回数据
            StringContent cnt = new StringContent("");
            cnt.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response= httpClient.PostAsync(new Uri(tuijianUrl), cnt).Result;
            string htmlStr = response.Content.ReadAsStringAsync().Result;
            doc.LoadHtml(htmlStr);
            var nodes=doc.DocumentNode.SelectNodes("//ul//a");

            List<object> list = new List<object>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var url = "http://www.cnblogs.com" + nodes[i].Attributes["href"].Value;
                var name = nodes[i].InnerText;
                list.Add(new { url=url,name=name});
            }
            return View();
        }

        public ActionResult GetBlogModuleByUName(string uname="kungge")
        {
            string url = "http://www.cnblogs.com/" + uname + "/mvc/blog/sidecolumn.aspx?blogApp=" + uname;
            HtmlDocument doc = new HtmlDocument();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage rspMsg = httpClient.GetAsync(new Uri(url)).Result;
            string htmlStr=rspMsg.Content.ReadAsStringAsync().Result;
            doc.LoadHtml(htmlStr);

            //var nodes = doc.DocumentNode.SelectNodes("//div[@class='catListPostCategory']//a");//随笔分类
            var nodes = doc.DocumentNode.SelectNodes("//div[@id='sidebar_postcategory']//a");//随笔分类
            if (nodes == null || nodes.Count <= 0)
                return View("");
            List<object> list = new List<object>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var aUrl = nodes[i].Attributes["href"].Value;
                var name = nodes[i].InnerText;
                list.Add(new { url = aUrl, name = name });
            }

                return View();
        }
    }
}
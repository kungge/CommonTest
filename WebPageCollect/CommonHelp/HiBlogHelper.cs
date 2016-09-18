using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebPageCollect.CommonHelp
{
    public class HiBlogHelper
    {
        public static bool LoginHiBlog(string uname = "", string pwd = "", string result = "")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");

            string loginUrl = "http://www.kungge.com/UserManage/Login";

            var paramList = new Dictionary<string, string>();
            paramList.Add("UserName", uname);
            paramList.Add("UserPass", pwd);
            paramList.Add("ischeck", "on");
            paramList.Add("X-Requested-With", "XMLHttpRequest");
            HttpResponseMessage response = httpClient.PostAsync(new Uri(loginUrl), new FormUrlEncodedContent(paramList)).Result;

            result = response.Content.ReadAsStringAsync().Result;
            if (result.Contains("\"State\":3"))
            {
                //string url = "http://www.kungge.com/";
                //HttpResponseMessage response_index= httpClient.GetAsync(new Uri(url)).Result;
                //string str = response_index.Content.ReadAsStringAsync().Result;
                //HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(str);
                //var node_userOperation = doc.DocumentNode.SelectNodes("//div[@class='userOperation']");//kwan_目前无法获知，因为头部的用户信息是通过ajax从部分页取  故这样取出来的是空的
                //if(node_userOperation!=null)
                //{
                //    var href = node_userOperation[0].Attributes["href"].Value;                 
                //}

                string url = "http://www.kungge.com/Home/BlogHead";
                HttpResponseMessage response_index = httpClient.GetAsync(new Uri(url)).Result;
                string str = response_index.Content.ReadAsStringAsync().Result;
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(str);
                var node_userOperation = doc.DocumentNode.SelectSingleNode("//a[@class='userOperation']");//todo
                return true;
            }
            else if (result.Contains("\"State\":2"))
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
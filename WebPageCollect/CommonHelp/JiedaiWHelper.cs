using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebPageCollect.CommonHelp
{
    public class JiedaiWHelper
    {
        public static bool LoginJiedaiW(string uname="",string pwd="",string authCode="",string result="")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");

            string loginUrl = "http://www.jiedai.cn/auth/login";

            var paramList = new Dictionary<string, string>();
            paramList.Add("UserPhone",uname);
            paramList.Add("UserPass", pwd);
            paramList.Add("Captcha", authCode);
            paramList.Add("AutoLogin", "0");
            HttpResponseMessage response= httpClient.PostAsync(new Uri(loginUrl), new FormUrlEncodedContent(paramList)).Result;

            result=response.Content.ReadAsStringAsync().Result;
            if (result.Contains("<title>用户登录-借贷网</title>"))
            {
                return false;
            }
            else if(result.Contains("<title>【借贷网】民间个人企业信用无抵押快速小额借贷款网络投资理财资讯首席门户平台</title>"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
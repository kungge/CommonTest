using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace WebPageCollect.CommonHelp
{
    public class LianTongHelper
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool LoginLianTong(string userName = "", string password = "", string result = "")
        {

            /*
            callback:jQuery172011088612383256091_1475992415996
            req_time:1475995445170
            redirectURL:http://iservice.10010.com/e3/index_server.html
            userName:13041694360
            password:2324234234
            pwdType:01
            productType:01
            redirectType:01
            rememberMe:1
            _:1475995445173


            jQuery172011088612383256091_1475992415996({resultCode:"7007",redirectURL:"http://iservice.10010.com/e3/index_server.html",errDesc:"null",msg:'用户名或密码不正确。<a href="https://uac.10010.com/cust/resetpwd/inputName" target="_blank" style="color: #36c;cursor: pointer;text-decoration:underline;">忘记密码？</a>',needvode:"1",errorFrom:"bss"});
            */

            /*
            http://iservice.10010.com/e3/index.html
            */


            //设置参数
            string callback = "jQuery172011088612383256091_1475992415996";
            //string req_time = DateTime.Now.ToFileTime().ToString();
            string req_time = GetTimeStamp(false);
            string redirectURL = "http://iservice.10010.com/e3/index_server.html";
            userName = "13041694360";
            password = "356352";
            string pwdType = "01";
            string redirectType = "01";
            string rememberMe = "1";
            //string other = (DateTime.Now.ToFileTime() + 3).ToString();
            string other= GetTimeStamp(false);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, sdch, br");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.4");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Add("Cookie", "mallcity=11|110; WT_FPC=id=2400ebef371c13b10fa1476006737513:lv=1476006737520:ss=1476006737513; Hm_lvt_9208c8c641bfb0560ce7884c36938d9d=1476006739; _n3fa_cid=d0542477d66e4e179cce0139c7896da5; _n3fa_ext=ft=1476065825; _n3fa_lvt_a9e72dfe4a54a20c3d6e671b3bad01d9=1476065825,1476081299; _n3fa_lpvt_a9e72dfe4a54a20c3d6e671b3bad01d9=1476081299; unisecid=92676833B1D877B7039AAEBDEC206F55; SHOP_PROV_CITY=");
            httpClient.DefaultRequestHeaders.Add("Host", "uac.10010.com");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://uac.10010.com/portal/homeLogin");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
           


            string loginUrl = "https://uac.10010.com/portal/Service/MallLogin";//登录提交的地址
            string loginUrl2 = "https://uac.10010.com/portal/mallLogin.jsp";//登录地址

            HttpResponseMessage response2= httpClient.GetAsync(new Uri(loginUrl2)).Result;
            //response2.Headers.GetValues("Set-Cookie");

       //test
       var cookieContainer = new CookieContainer();
            CookieCollection cookies = cookieContainer.GetCookies(new Uri(loginUrl2));//kwan_tip failure
            //cookieContainer.Add(new Cookie("test", "0"));
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer,
                AllowAutoRedirect = true,
                UseCookies = true
            };
            HttpClient httpClient1 = new HttpClient(httpClientHandler);


             //post
             //var paramList = new Dictionary<string, string>();
             //paramList.Add("req_time", req_time);
             //paramList.Add("redirectURL", redirectURL);
             //paramList.Add("userName", userName);
             //paramList.Add("password", password);
             //paramList.Add("pwdType", pwdType);
             //paramList.Add("redirectType", redirectType);
             //paramList.Add("rememberMe", rememberMe);
             //HttpResponseMessage response = httpClient.PostAsync(new Uri(loginUrl), new FormUrlEncodedContent(paramList)).Result;
             //get
             loginUrl += "?callback=" + callback + "&req_time=" + req_time + "&redirectURL=" + redirectURL + "&userName=" + userName + "&password=" + password + "&pwdType=" + pwdType + "&redirectType=" + redirectType + "&rememberMe=" + rememberMe + "&_=" + other;
            HttpResponseMessage response = httpClient.GetAsync(new Uri(loginUrl)).Result;
          

            result = response.Content.ReadAsStringAsync().Result;

            if (result.Contains("<title>用户登录-</title>"))
            {
                return false;
            }
            else if (result.Contains("<title>***</title>"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool LoginLianTong2(string userName = "", string password = "", string result = "")
        {
            //设置参数
            //string callback = "jQuery172011088612383256091_1475992415996";
            ////string req_time = DateTime.Now.ToFileTime().ToString();
            //string req_time = GetTimeStamp(false);
            //string redirectURL = "http://iservice.10010.com/e3/index_server.html";
            //userName = "13041694360";
            //password = "356352";
            //string pwdType = "01";
            //string redirectType = "01";
            //string rememberMe = "1";
            //string other = GetTimeStamp(false);

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                CookieContainer = SetCookie(),
                AllowAutoRedirect = true,
                UseCookies = true
            };
            HttpClient httpClient1 = new HttpClient(httpClientHandler);
            httpClient1.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36");
            //string loginUrl = "https://uac.10010.com/portal/Service/MallLogin";//登录提交的地址
            //loginUrl += "?callback=" + callback + "&req_time=" + req_time + "&redirectURL=" + redirectURL + "&userName=" + userName + "&password=" + password + "&pwdType=" + pwdType + "&redirectType=" + redirectType + "&rememberMe=" + rememberMe + "&_=" + other;
            string loginUrl = "https://uac.10010.com/portal/Service/MallLogin?callback=jQuery17205380501410004572_1476081792283&req_time=1476081858278&redirectURL=http://www.10010.com&userName=13041694360&password=356352&pwdType=01&productType=01&redirectType=01&rememberMe=1&_=1476081858279";
            HttpResponseMessage response = httpClient1.GetAsync(new Uri(loginUrl)).Result;


            result = response.Content.ReadAsStringAsync().Result;

            if (result.Contains("resultCode:\"0000\""))//登录成功
            {
                HttpClientHandler httpClientHandler2 = new HttpClientHandler()
                {
                    CookieContainer = SetCookie2(),
                    AllowAutoRedirect = true,
                    UseCookies = true
                };
                HttpClient httpClient2 = new HttpClient(httpClientHandler2);
                string url_detailInfo = "http://iservice.10010.com/e3/query/call_dan.html?menuId=000100030001";
                HttpResponseMessage response_detail = httpClient2.GetAsync(new Uri(url_detailInfo)).Result;
                var detail_result=response_detail.Content.ReadAsStringAsync().Result;
                HtmlAgilityPack.HtmlDocument htmldocument = new HtmlDocument();
                htmldocument.LoadHtml(detail_result);
                var customerNode = htmldocument.DocumentNode.SelectNodes("//*[@id='callDetailAll']/dl[1]/dd");
                return true;
            }
            else
            {
                return false;
            }

        }

        private static CookieContainer SetCookie2()
        {
            CookieContainer cc = new CookieContainer();
            Cookie cook = new Cookie("Hm_lpvt_9208c8c641bfb0560ce7884c36938d9d","1476084815");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);

            //cook = new Cookie("Hm_lvt_9208c8c641bfb0560ce7884c36938d9d","1476006739,1476084295");
            cook = new Cookie("Hm_lvt_9208c8c641bfb0560ce7884c36938d9d", "1476006739");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("JUT","3n0WubVqs5AfrtGoroMa7hC + KHRyb0D666ISrNm9qonp0dSoePJ6m + EIxQW2WKvILvMQYUb1IFAP1WLVbzoSQk7AC / 6LI7LrQcUYMhIBt6hjkW1D4Eg2ot406yuIDokfENttLa82v7TOulPeW0K4Xvq5Iq1nXq2qtOuE + TCmdQjd38E4h / yPAAQzc + 4LDTSxAFZmHsrJolU8GweJ71MUra18qXF64vaCm8oWl0th9ToPiinv4mJL1VAOZW / uCxT + X2eOLqWTWa2Y / VN68Oh + ljpgK4Jf5t8JaNmHW8O4KFNn4AkPVHD9JDYEBTxjgQhpKT1nB2uU9hZjNnyqKqDdchpUQI / SUd0ZajQ29T86UMVS + 5 + R7p / y / 6 / avTN / CG859gSz9b / mtXfZMcFFRzlVztUFXcnHiSxZilSLnIk9YdHiylFb / O / 5OKRl78AZKGqMZN0iko1 + ur9KIuBPoSGswzt74Q1uYwqnuOh4yVX3TUtM1 / 5Gnyt2vKSqGPzyH9Jl2Daf3QghhVbTjHkLEIq0NHh + 4r / wp3 + M9O2KyWep7mo = f3Yw6JiJxWch + YxpLnZSJA == ");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("MENUURL"," / e3 / navhtml3 / WT3 / WT_MENU_3_001 / 031 / 021_1101.html ? _ = 1476084811073");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("MIE", "00090001");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("MII", "000100030001");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("WT_FPC", "id = 2400ebef371c13b10fa1476006737513: lv = 1476084811178:ss = 1476084292975");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("_dc_gtm_UA-27681312-1", "1");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("_ga", "GA1.3.389078819.1476006741");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_cid", "d0542477d66e4e179cce0139c7896da5");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_ext", "ft = 1476065825");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_lpvt_a9e72dfe4a54a20c3d6e671b3bad01d9", "1476084801");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            //cook = new Cookie("_n3fa_lvt_a9e72dfe4a54a20c3d6e671b3bad01d9", "1476065825, 1476081299, 1476084801");
            cook = new Cookie("_n3fa_lvt_a9e72dfe4a54a20c3d6e671b3bad01d9", "1476065825");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("_uop_id", "07f7ec9a2acf239106ef206eb99a4765");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            //cook = new Cookie("Hm_lvt_9208c8c641bfb0560ce7884c36938d9d", "1476006739, 1476084295");
            cook = new Cookie("Hm_lvt_9208c8c641bfb0560ce7884c36938d9d", "1476006739");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("e3", "0m3ZX7CF30n3sJCFdc8BTyf2vyFtxy01td40Xjn1tTn0Kpgq4f1p!-780485118");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            cook = new Cookie("mallcity", "11 | 110");
            cook.Domain = "iservice.10010.com";
            cc.Add(cook);
           // cook = new Cookie("piw", "{ \"login_name\":\"130****4360\", \"nickName\":\"ä¸å¤\", \"rme\":{ \"ac\":\"\",\"at\":\"\",\"pt\":\"01\",\"u\":\"13041694360\"},\"verifyState\":\"\"}");
           // cook.Domain = "iservice.10010.com";
           //cc.Add(cook);
              cook = new Cookie("route", "895671933e5d836b9514d11073c97f20");
        cook.Domain = "iservice.10010.com";
            cc.Add(cook);
            return cc;
        }

        private static CookieContainer SetCookie()
        {
            CookieContainer cc = new CookieContainer();
            Cookie cook = new Cookie("gipgeo", "31|310");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);

            cook = new Cookie("mallcity", "31|310");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("SHOP_PROV_CITY", "");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("WT_FPC", "id=298f8d00e1df97eabee1476079374314:lv=1476079374317:ss=1476079374314");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("unisecid", "7AE74A047D31491D4E1B2B17D100BC87");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_cid", "8075da6432e548debb4a3875d74e454d");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_ext", "ft=1476007700");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_lvt_a9e72dfe4a54a20c3d6e671b3bad01d9", "1476007700");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            cook = new Cookie("_n3fa_lpvt_a9e72dfe4a54a20c3d6e671b3bad01d9", "1476079957");
            cook.Domain = "uac.10010.com";
            cc.Add(cook);
            return cc;
        }


        public static string GetDetailInfo(string url="")
        {
            url = url==""? "http://iservice.10010.com/e3/query/call_dan.html?menuId=000100030001":url;
            HtmlAgilityPack.HtmlWeb htmlweb = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();         
            var docment = htmlweb.Load(url);
            var customerNode= document.DocumentNode.SelectNodes("//*[@id='callDetailAll']/dl[1]/dd");

            return "";
        }


        /// <summary>  
        /// 获取当前时间戳  
        /// </summary>  
        /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>  
        /// <returns></returns>  
        public static string GetTimeStamp(bool bflag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string ret = string.Empty;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds).ToString();
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            return ret;
        }

    }
}
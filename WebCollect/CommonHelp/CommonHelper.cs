using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCollect.CommonHelp
{
    public static class CommonHelper
    {
        #region HttpClient
        private static HttpClient _httpClient;
        public static HttpClient httpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                    _httpClient.Timeout = new TimeSpan(0, 4, 0);

                }
                return _httpClient;
            }
            set { _httpClient = value; }
        }

        public static HtmlDocument GetHtmlDocument
        {
            get { return new HtmlDocument(); }
        }
        #endregion

        #region get请求
        /// <summary>
        /// get请求返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRequestStr(string url)
        {
            try
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// get请求返回的二进制
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] GetRequestByteArr(string url)
        {
            try
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;
                return response.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region post请求
        /// <summary>
        /// post请求返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostRequestStr(string url)
        {
            try
            {
                string contentStr = "";
                StringContent sc = new StringContent(contentStr);
                sc.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");//todo
                var response = httpClient.PostAsync(new Uri(url), sc).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

    }
}

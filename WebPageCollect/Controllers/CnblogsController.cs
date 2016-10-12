using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebPageCollect.CommonHelp;
using WebPageCollect.Models;

namespace WebPageCollect.Controllers
{
    public class CnblogsController : Controller
    {
        // GET: Cnblogs
        public ActionResult Index()
        {
            return View();
        }

        #region 获取推荐博客
        /// <summary>
        /// kwan_tip 不用
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTuiJianBlog()
        {
            string tuijianUrl = "http://www.cnblogs.com/aggsite/ExpertBlogs";
            HtmlDocument doc = new HtmlDocument();

            //获取post请求的返回数据
            StringContent cnt = new StringContent("");
            cnt.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response= httpClient.PostAsync(new Uri(tuijianUrl), cnt).Result;
            string htmlStr = response.Content.ReadAsStringAsync().Result;
            doc.LoadHtml(htmlStr);
            var nodes=doc.DocumentNode.SelectNodes("//ul//a");

            List<BlogInfo> list = new List<BlogInfo>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var url = "http://www.cnblogs.com" + nodes[i].Attributes["href"].Value;
                var name = nodes[i].InnerText;
                list.Add(new BlogInfo() { BlogUrl=url,BlogName=name});
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("NameBlogList",list);
            return View(dic);
        }
        public ActionResult GetRecommendBlog()
        {
            string rmdUrl = "http://www.cnblogs.com/aggsite/ExpertBlogs";
            string htmlStr=CommonHelper.PostRequestStr(rmdUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);
            var nodes = doc.DocumentNode.SelectNodes("//ul//a");

            List<BlogInfo> list = new List<BlogInfo>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var url = "http://www.cnblogs.com" + nodes[i].Attributes["href"].Value;
                var name = nodes[i].InnerText;
                list.Add(new BlogInfo() { BlogUrl = url, BlogName = name });
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("NameBlogList", list);
            return View(dic);
        }
        #endregion

        #region 获取随笔分类
        public ActionResult GetBlogTypesByUName(string uname = "kungge")
        {
            List<BlogType> list = GettBlogTypeList(uname);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("NameBlogList", list);
            return View(dic);
        }
        private List<BlogType> GettBlogTypeList(string uname)
        {
            string url = "http://www.cnblogs.com/" + uname + "/mvc/blog/sidecolumn.aspx?blogApp=" + uname;
            string htmlStr = CommonHelper.GetRequestStr(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);
            var nodes = doc.DocumentNode.SelectNodes("//div[@id='sidebar_postcategory']//a");//随笔分类
            if (nodes == null || nodes.Count <= 0)
                return null;
            List<BlogType> list = new List<BlogType>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var aUrl = nodes[i].Attributes["href"].Value;
                var name = nodes[i].InnerText;
                list.Add(new BlogType() { BlogTypeUrl = aUrl, BlogTypeName = name.Contains("(")?name.Split('(')[0]:name });
            }
            return list;
        }
        #endregion

        #region 下载博客文章
        public ActionResult SaveBlog()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveBlog(string uname)
        {
            //获取数据
            List<BlogType> types = GettBlogTypeList(uname);
            var blogList = GetBlogsByType(types[0].BlogTypeUrl);
            var blogDetailList = GetBlogDetail(blogList);//得到最终数据
            Dictionary<BlogType, List<BlogInfo>> dic = new Dictionary<BlogType, List<BlogInfo>>();
            dic.Add(types[0],blogDetailList);
            DowanloadBlog(dic,uname);

            return Json("");
        }
        #endregion

        /// <summary>
        /// 根据分类获取博客
        /// </summary>
        /// <param name="typeUrl"></param>
        /// <returns></returns>
        public List<BlogInfo> GetBlogsByType(string typeUrl)
        {
            List<BlogInfo> list = new List<BlogInfo>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(CommonHelper.GetRequestStr(typeUrl));
            var typeNameNode=doc.DocumentNode.SelectSingleNode("//div[@class='entrylist']/h1");
            string typeName=typeNameNode.InnerText;
            var listPosttitleNodes = doc.DocumentNode.SelectNodes("//div[@class='entrylistPosttitle']/a");
            if (listPosttitleNodes != null && listPosttitleNodes.Count > 0)
            {
                for(int i = 0; i < listPosttitleNodes.Count; i++)
                {
                    list.Add(new BlogInfo() {
                        BlogUrl =listPosttitleNodes[i].Attributes["href"].Value,BlogTitle=listPosttitleNodes[i].InnerText,BlogTypeName=typeName
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取详细的博客信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<BlogInfo> GetBlogDetail(List<BlogInfo> list)
        {
            HtmlDocument doc = new HtmlDocument();
            for(int i=0;i<list.Count;i++)
            {
                doc.LoadHtml(CommonHelper.GetRequestStr(list[i].BlogUrl));
                var bodyNode=doc.DocumentNode.SelectSingleNode("//div[@id='cnblogs_post_body']");
                var dateNode = doc.DocumentNode.SelectSingleNode("//span[@id='post-date']");
                var userNode = doc.DocumentNode.SelectSingleNode("//div[@class='postDesc']/a[1]");
                list[i].BlogContent = bodyNode.InnerHtml;
                list[i].BlogPostTime = dateNode.InnerText;
                list[i].BlogName = userNode.InnerText;
            }
            return list;
        }

        public void DowanloadBlog(Dictionary<BlogType,List<BlogInfo>> dic,string uname)
        {
            for(int i=0;i<dic.Keys.Count;i++)
            {
                var blogType = dic.Keys.ElementAt(i);
                var blogList = dic[blogType];
                var dicPath = Server.MapPath("/BlogFiles/" + uname + "/" + blogType.BlogTypeName);
                FileHelper.CreatePath(dicPath);
                var blogModel = new BlogInfo();
                for (int j = 0; j < blogList.Count; j++)
                {
                    try
                    {
                        blogModel = blogList[j];
                        var filePath = dicPath + "/" + FileHelper.FilterInvalidChar(blogModel.BlogTitle,"_") + ".html";
                        HtmlDocument doc = new HtmlDocument();
                        doc.DocumentNode.InnerHtml = blogModel.BlogContent;

                        //处理图片
                        var imgPath = dicPath + "\\images";
                        FileHelper.CreatePath(imgPath);
                        SaveImage(doc, imgPath);

                        //去掉a标签
                        var aNodes=doc.DocumentNode.SelectNodes("//a");
                        if(aNodes!=null&&aNodes.Count>0)
                        {
                           for(int a=0;a<aNodes.Count;a++)
                            {
                                if(aNodes[a].Attributes["href"]!=null&& aNodes[a].Attributes["href"].Value != "#")
                                {
                                    aNodes[a].Attributes["href"].Value = "javascript:void()";
                                }                              
                            }
                        }
                        doc.DocumentNode.InnerHtml = "<div id='div_head'>" + uname + " " + blogType.BlogTypeName + "</div><div id='div_title'>" + blogModel.BlogTitle + "<div><div id='div_body'>" + doc.DocumentNode.InnerHtml + "</div>";
                        doc.Save(filePath, Encoding.UTF8);
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = DateTime.Now.ToString("yyyyMMdd HH:mm:ss")+"\r\n" + "url="+blogModel.BlogUrl+"\r\n"+"title="+ blogModel.BlogTitle + "\r\n" + "errorMsg="+ex.Message+"\r\n"+ "stackTrace="+ex.StackTrace+ "\r\n\r\n\r\n";
                        FileHelper.SaveTxtFile(dicPath, "errorLog.txt", errorMsg, false);
                    }

                }


            }
        }

        public void SaveImage(HtmlDocument doc, string filePath)
        {
            var imgNodes = doc.DocumentNode.SelectNodes("//img");
            if(imgNodes!=null&&imgNodes.Count>0)
            {
                for(int i=0;i<imgNodes.Count;i++)
                {
                    try
                    {
                        string src = imgNodes[i].Attributes["src"].Value;
                        string fileName = "";
                        if (src != null && src.Contains("/"))
                        {
                            fileName = src.Substring(src.LastIndexOf("/") + 1);
                            string imgPath = filePath + "\\" + fileName;
                            imgNodes[i].Attributes["src"].Value = imgPath;
                            byte[] imgByte = CommonHelper.GetRequestByteArr(src);
                            if (imgByte != null)
                            {
                                FileHelper.SaveImage(imgPath, imgByte);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("SaveImage_Error:"+ex.Message);
                    }

                }
            }
        }

    }

}
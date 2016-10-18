using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCollect.CommonHelp;
using WebCollect.Models;

namespace WebCollect
{
    class Program
    {
        static void Main(string[] args)
        {
            
           //输入博客名称
            string uname = "";        
            bool unameNull = true;
            
            do
            {
                Console.WriteLine("--请输入要下载的博客名称--");
                uname = Console.ReadLine();
                if (string.IsNullOrEmpty(uname))
                {
                    Console.WriteLine("--请输入要下载的博客名称--");
                    uname = Console.ReadLine();
                }
                else
                {
                    unameNull = false;
                }
            } while (unameNull);

            //获取博客分类
            bool hasTypes = true;
            List<BlogType> types = new List<BlogType>();
            do
            {
                types = GettBlogTypeList(uname);
                if (types == null || types.Count == 0)
                {
                    Console.WriteLine("--未获取到分类，请重新输入要下载的博客名称--");
                    uname = Console.ReadLine();
                    types = GettBlogTypeList(uname);
                }else
                {
                    hasTypes = false;
                }
            } while (hasTypes);                    
            Console.WriteLine("获取{0}的随笔分类如下：", uname);
            foreach(var item in types)
            {
                Console.WriteLine("{0}", item.BlogTypeNameShow);
            }

            //操作
            bool handlerRight = true;
            string handlerFlag = "";
            do
            {
                Console.WriteLine("--1=获取文章列表，2=下载所有文章，0=退出--");
                handlerFlag = Console.ReadLine();
                if(handlerFlag=="0")
                {
                    return;
                }
                else if(handlerFlag == "1")
                {
                    var typeUrls = types.Select(x => x.BlogTypeUrl);
                    var blogList = GetBlogsByType(typeUrls);
                    for(int i = 0; i < blogList.Count; i++)
                    {
                        Console.WriteLine("{0}.title={1}，url={2}",i,blogList[i].BlogTitle,blogList[i].BlogUrl);
                    }
                    Console.WriteLine("*******获取文章列表完毕*******");
                    handlerRight = false;
                }
                else if (handlerFlag == "2")
                {
                    long time1 = 0;
                    long time2 = 0;
                    long timeDownload = 0;
                    Console.WriteLine("正在爬取，请耐心等待...");
                    var blogList = GetBlogsByType(types,out time1);
                    var blogDetailList = GetBlogDetail(blogList,out time2);
                    Console.WriteLine("爬取完毕，开始下载...");
                    string filePath=DowanloadBlog(blogDetailList, uname,out timeDownload);
                    Console.WriteLine("**处理完毕，爬取用时{0}ms，下载用时{1}ms，{2}", time1+time2, timeDownload, filePath);
                    handlerRight = false;
                }
                else
                {
                    Console.WriteLine("--1=获取文章列表，2=下载所有文章，0=退出--");
                    handlerFlag = Console.ReadLine();
                }
            } while (handlerRight);
           
            Console.ReadKey();
        }

        #region 下载博客
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="filePath"></param>
        public static void SaveImage(HtmlDocument doc, string filePath)
        {
            var imgNodes = doc.DocumentNode.SelectNodes("//img");
            if (imgNodes != null && imgNodes.Count > 0)
            {
                for (int i = 0; i < imgNodes.Count; i++)
                {
                    try
                    {                      
                        string src = imgNodes[i].Attributes["src"].Value;
                        string fileName = "";
                        if (src != null && src.Contains("/"))
                        {
                            fileName = src.Substring(src.LastIndexOf("/") + 1);
                            Console.WriteLine("~~开始下载图片【{0}】~~", fileName);
                            string imgPath = filePath + "\\" + fileName;
                            imgNodes[i].Attributes["src"].Value = imgPath;
                            byte[] imgByte = CommonHelper.GetRequestByteArr(src);
                            if (imgByte != null)
                            {
                                FileHelper.SaveImage(imgPath, imgByte);
                                Console.WriteLine("~~下载图片【{0}】完成~~", fileName);
                            }
                            else
                            {
                                Console.WriteLine("~~下载图片【{0}】失败~~", fileName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("SaveImage_Error:" + ex.Message);
                    }

                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="uname"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        public static string DowanloadBlog(Dictionary<BlogType, List<BlogInfo>> dic, string uname,out long useTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int countFlag = 0;
            for (int i = 0; i < dic.Keys.Count; i++)
            {
                var blogType = dic.Keys.ElementAt(i);
                var blogList = dic[blogType];
                var dicPath = AppDomain.CurrentDomain.BaseDirectory +"BlogFiles\\" + uname + "\\" + blogType.BlogTypeName;
                Console.WriteLine("<<开始处理分类【{0}】<<", blogType.BlogTypeName);
                FileHelper.CreatePath(dicPath);
                var blogModel = new BlogInfo();
                for (int j = 0; j < blogList.Count; j++)
                {
                    countFlag++;
                    try
                    {
                        Console.WriteLine("~~~~开始处理文章{0}【{1}】~~~~", countFlag,blogModel.BlogTitle);
                        blogModel = blogList[j];
                        var filePath = dicPath + "\\" + FileHelper.FilterInvalidChar(blogModel.BlogTitle, "_") + ".html";
                        HtmlDocument doc = new HtmlDocument();
                        doc.DocumentNode.InnerHtml = blogModel.BlogContent;

                        //处理图片
                        Console.WriteLine("~~开始处理图片");
                        var imgPath = dicPath + "\\images";
                        FileHelper.CreatePath(imgPath);
                        SaveImage(doc, imgPath);
                        Console.WriteLine("~~处理图片完成");

                        //去掉a标签
                        var aNodes = doc.DocumentNode.SelectNodes("//a");
                        if (aNodes != null && aNodes.Count > 0)
                        {
                            for (int a = 0; a < aNodes.Count; a++)
                            {
                                if (aNodes[a].Attributes["href"] != null && aNodes[a].Attributes["href"].Value != "#")
                                {
                                    aNodes[a].Attributes["href"].Value = "javascript:void()";
                                }
                            }
                        }
                        doc.DocumentNode.InnerHtml = "<div id='div_head'>" + uname + " " + blogType.BlogTypeName + "</div><div id='div_title'>" + blogModel.BlogTitle + "<div><div id='div_body'>" + doc.DocumentNode.InnerHtml + "</div>";
                        doc.Save(filePath, Encoding.UTF8);
                        Console.WriteLine("~~~~处理文章{0}【{1}】完毕~~~~",countFlag,blogModel.BlogTitle);
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\r\n" + "url=" + blogModel.BlogUrl + "\r\n" + "title=" + blogModel.BlogTitle + "\r\n" + "errorMsg=" + ex.Message + "\r\n" + "stackTrace=" + ex.StackTrace + "\r\n\r\n\r\n";
                        Console.WriteLine("error>>处理文章【{0}】出现错误，开始记录错误信息~~", blogModel.BlogTitle);
                        FileHelper.SaveTxtFile(dicPath, "errorLog.txt", errorMsg, false);
                        Console.WriteLine("error>>处理文章【{0}】出现错误，记录错误信息完成~~", blogModel.BlogTitle);
                    }
                }
                Console.WriteLine("<<处理分类【{0}】完成<<", blogType.BlogTypeName);

            }
            sw.Start();
            useTime = sw.ElapsedMilliseconds;
            return AppDomain.CurrentDomain.BaseDirectory + "BlogFiles\\" + uname;
        }
        #endregion

        #region 获取博客分类
        /// <summary>
        /// 获取博客分类
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        private static List<BlogType> GettBlogTypeList(string uname)
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
                list.Add(new BlogType() { BlogTypeUrl = aUrl, BlogTypeName = name.Contains("(") ? name.Split('(')[0] : name,BlogTypeNameShow=name });
            }
            return list;
        }
        #endregion

        #region 根据分类获取博客
        /// <summary>
        /// 根据分类获取博客
        /// </summary>
        /// <param name="typeUrl"></param>
        /// <returns></returns>
        public static List<BlogInfo> GetBlogsByType(IEnumerable<string> typeUrls)
        {
            List<BlogInfo> list = new List<BlogInfo>();
            foreach (var typeUrl in typeUrls)
            {              
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(CommonHelper.GetRequestStr(typeUrl));
                var typeNameNode = doc.DocumentNode.SelectSingleNode("//div[@class='entrylist']/h1");
                string typeName = typeNameNode.InnerText;
                var listPosttitleNodes = doc.DocumentNode.SelectNodes("//div[@class='entrylistPosttitle']/a");
                if (listPosttitleNodes != null && listPosttitleNodes.Count > 0)
                {
                    for (int i = 0; i < listPosttitleNodes.Count; i++)
                    {
                        Console.WriteLine("正在爬取文章【{0}】...", listPosttitleNodes[i].InnerText);
                        list.Add(new BlogInfo()
                        {
                            BlogUrl = listPosttitleNodes[i].Attributes["href"].Value,
                            BlogTitle = listPosttitleNodes[i].InnerText,
                            BlogTypeName = typeName
                        });
                    }
                }
            }         
            return list;
        }
        /// <summary>
        /// 根据分类获取博客
        /// </summary>
        /// <param name="blogTypes"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        public static Dictionary<BlogType,List<BlogInfo>> GetBlogsByType(List<BlogType> blogTypes,out long useTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Dictionary<BlogType, List<BlogInfo>> dic = new Dictionary<BlogType, List<BlogInfo>>();           
            foreach (var blogType in blogTypes)
            {
                List<BlogInfo> list = new List<BlogInfo>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(CommonHelper.GetRequestStr(blogType.BlogTypeUrl));
                var typeNameNode = doc.DocumentNode.SelectSingleNode("//div[@class='entrylist']/h1");
                string typeName = typeNameNode.InnerText;
                var listPosttitleNodes = doc.DocumentNode.SelectNodes("//div[@class='entrylistPosttitle']/a");
                if (listPosttitleNodes != null && listPosttitleNodes.Count > 0)
                {
                    for (int i = 0; i < listPosttitleNodes.Count; i++)
                    {
                        Console.WriteLine("正在爬取文章【{0}】...", listPosttitleNodes[i].InnerText);
                        list.Add(new BlogInfo()
                        {
                            BlogUrl = listPosttitleNodes[i].Attributes["href"].Value,
                            BlogTitle = listPosttitleNodes[i].InnerText,
                            BlogTypeName = typeName
                        });
                    }
                }
                dic.Add(blogType,list);
            }
            sw.Stop();
            useTime = sw.ElapsedMilliseconds;
            return dic;
        }
        #endregion

        #region 获取详细的博客信息
        /// <summary>
        /// 获取详细的博客信息
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        public static Dictionary<BlogType, List<BlogInfo>> GetBlogDetail(Dictionary<BlogType, List<BlogInfo>> dic, out long useTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            HtmlDocument doc = new HtmlDocument();
            for(int k=0;k<dic.Keys.Count;k++)
            {
                var blogType = dic.Keys.ElementAt(k);
                var list = dic[blogType];
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("正在获取文章【{0}】内容...", list[i].BlogTitle);
                    doc.LoadHtml(CommonHelper.GetRequestStr(list[i].BlogUrl));
                    var bodyNode = doc.DocumentNode.SelectSingleNode("//div[@id='cnblogs_post_body']");
                    var dateNode = doc.DocumentNode.SelectSingleNode("//span[@id='post-date']");
                    var userNode = doc.DocumentNode.SelectSingleNode("//div[@class='postDesc']/a[1]");
                    list[i].BlogContent = bodyNode == null ? "内容获取失败" : bodyNode.InnerHtml;
                    list[i].BlogPostTime = dateNode == null ? "发布时间获取失败" : dateNode.InnerText;
                    list[i].BlogName = userNode == null ? "用户获取失败" : userNode.InnerText;
                }
                dic[blogType] = list;
            }
            sw.Stop();
            useTime = sw.ElapsedMilliseconds;
            return dic;
        }
        #endregion

    }
}

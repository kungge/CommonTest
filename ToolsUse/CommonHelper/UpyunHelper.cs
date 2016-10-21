using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ToolsUse.CommonHelper
{
    public class UpyunHelper
    {

        #region 基本配置信息
        /// <summary>
        /// 图片所属类型
        /// </summary>
        public enum ImgType
        {
            other = 0,//其他
            article = 1,//文章
            ad = 2,//广告
            bbs = 3//论坛
        }
        /// <summary>
        /// 文件所属服务器类型
        /// </summary>
        public enum BucketType
        {
            Picure,
            Video,
            File,
            Avatar,
            Comprehensive
        }
        public static readonly string upYunDomain = "v0.api.upyun.com";
        public static readonly string uname = "kunggewan";
        public static readonly string pwd = "kungge356352";

        public static readonly string bucketName_avatar = "kungge-avatar";
        public static readonly string bucketName_pic = "kungge-pic";
        public static readonly string bucketName_video = "kungge-video";
        public static readonly string bucketName_file = "kungge-file";
        public static readonly string bucketName_comprehensive = "kwan-upyun";

        public static readonly string siteDomain = "img.kungge.com";
        #endregion


        #region 文件扩展名集合
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png", ".giff", ".bmp" };
        public static readonly List<string> VideoextExtensions = new List<string> { ".avi", ".flv", ".mpeg", ".rm", ".rmvb", ".mpg", ".3gp", ".mp4", ".mov", ".mtv", ".wmv", ".amv" };
        public static readonly List<string> FileExtensions = new List<string> { ".txt", ".csv", ".xls", ".xlsx", ".doc", ".docx", ".pdf", ".wps", ".wpt", ".et", ".ett" };
        #endregion

        #region 返回文件所属的服务器类型
        /// <summary>
        /// 返回文件类型
        /// </summary>
        /// <param name="extensionName">扩展名</param>
        /// <returns></returns>
        public static BucketType GetFileType(string extensionName)
        {
            extensionName = extensionName.ToLower();
            if(extensionName.Contains("comprehensive"))
            {
                return BucketType.Comprehensive;
            }
            if (VideoextExtensions.Contains(extensionName))
            {
                return BucketType.Video;
            }
               
            if (ImageExtensions.Contains(extensionName))
            {
                return BucketType.Picure;
            }
                
            if (FileExtensions.Contains(extensionName))
            {
                return BucketType.File;
            }             
            throw new Exception("不支持上传此类型【"+ extensionName + "】文件！");
        }
        #endregion

        #region 构建文件夹路径
        /// <summary>
        /// 构建文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string BuildFilePath(int typdId = 0)
        {
            return string.Format("/{0}/{1}/{2}/{3}", Enum.GetName(typeof(ImgType), typdId), DateTime.Now.Year,
                                               DateTime.Now.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),
                                               DateTime.Now.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
        }
        #endregion

        #region 构建文件名
        /// <summary>
        /// 构建文件名
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string BuildFileName(string extension)
        {
            return (Guid.NewGuid() + extension).ToLower();
        }
        #endregion

        #region 将文件流转为byte[]
        /// <summary>
        /// 将文件流转为byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static byte[] SteamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        #endregion

        #region 返回服务器名
        /// <summary>
        /// 返回存储空间名
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        private static string GetFileBucketName(BucketType zoneName)
        {
            var bucketName = "";
            switch (zoneName)
            {
                case BucketType.Avatar:
                    bucketName = "kungge-avatar"; break;
                case BucketType.Picure:
                    bucketName = "kungge-pic"; break;
                case BucketType.Video:
                    bucketName = "kungge-video"; break;
                case BucketType.File:
                    bucketName = "kungge-file"; break;
                case BucketType.Comprehensive:
                    bucketName = "kwan-upyun"; break;
                default:
                    bucketName = "kwan-upyun"; break;
            }
            return bucketName;
        }
        #endregion

        #region 创建路径
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        public static bool CreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }
        #endregion

        #region 处理url
        /// <summary>
        /// 处理URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="zoneType"></param>
        /// <returns></returns>
        public static string ProcessUrl(string url, BucketType zoneType)
        {
            if (string.IsNullOrEmpty(url))
                return url;
            switch (zoneType)
            {
                case BucketType.Avatar:
                    return url.Replace(upYunDomain + "/" + bucketName_avatar, siteDomain);
                case BucketType.Picure:
                    return url.Replace(upYunDomain + "/" + bucketName_pic, siteDomain);
                case BucketType.Video:
                    return url.Replace(upYunDomain + "/" + bucketName_video, siteDomain);
                case BucketType.File:
                    return url.Replace(upYunDomain + "/" + bucketName_file, siteDomain);
                case BucketType.Comprehensive:
                    return url.Replace(upYunDomain + "/" + bucketName_comprehensive, siteDomain);
                default:
                    return url;
            }
        }
        #endregion

        #region 上传
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="zoneName"></param>
        /// <param name="uploadPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string UpLoad(Stream stream, BucketType zoneType, string uploadPath, string fileName)
        {
            string bucketName = GetFileBucketName(zoneType);
            Hashtable headers = new Hashtable();
            byte[] bytes = SteamToBytes(stream);
            var uploadUrl = "http://" +upYunDomain+ "/" + bucketName.ToLower() + uploadPath + "/" + fileName;

            string url = string.Empty;
            uploadUrl.PostBytesToUrl(bytes,
                                 requestFilter:
                                     request =>
                                     {
                                         request.KeepAlive = true;
                                         request.ContentLength = bytes.Length;
                                         request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(uname + ":" + pwd)));
                                         request.Headers.Add("mkdir", "true");
                                         if (headers != null && headers.Count > 0)
                                         {
                                             foreach (DictionaryEntry var in headers)
                                             {
                                                 request.Headers.Add(var.Key.ToString(), var.Value.ToString());
                                             }
                                         }
                                     }, 
                                    responseFilter: r =>
                                     {
                                         if (r.StatusCode == HttpStatusCode.OK)
                                         {
                                             url = r.ResponseUri.AbsoluteUri;
                                         }
                                     });
            return url;
        }
        public static string UpLoad(byte[] bytes, BucketType zoneType, string uploadPath, string fileName)
        {
            string bucketName = GetFileBucketName(zoneType);
            Hashtable headers = new Hashtable();
            var uploadUrl = "http://" + upYunDomain + "/" + bucketName.ToLower() + uploadPath + "/" + fileName;

            string url = string.Empty;
            uploadUrl.PostBytesToUrl(bytes,
                                 requestFilter:
                                     request =>
                                     {
                                         request.KeepAlive = true;
                                         request.ContentLength = bytes.Length;
                                         request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(uname + ":" + pwd)));
                                         request.Headers.Add("mkdir", "true");
                                         if (headers != null && headers.Count > 0)
                                         {
                                             foreach (DictionaryEntry var in headers)
                                             {
                                                 request.Headers.Add(var.Key.ToString(), var.Value.ToString());
                                             }
                                         }
                                     },
                                    responseFilter: r =>
                                    {
                                        if (r.StatusCode == HttpStatusCode.OK)
                                        {
                                            url = r.ResponseUri.AbsoluteUri;
                                        }
                                    });
            return url;
        }
        #endregion




    }
}
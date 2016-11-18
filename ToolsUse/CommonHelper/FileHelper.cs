using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ToolsUse.CommonHelper
{
    public static class FileHelper
    {
        #region 配置
        /// <summary>
        /// 图片路径总目录
        /// </summary>
        public enum ImgType
        {
            others = 0,//其他
            article = 1,//文章
            gg = 2,//广告
            bbs = 3//论坛
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public enum ZoneType
        {
            Photo,
            Video,
            File,
            Avatar,
        }
        #endregion

        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png", ".giff", ".bmp" };
        public static readonly List<string> VideoextExtensions = new List<string> { ".avi",".flv",".mpeg",".rm", ".rmvb",".mpg",".3gp",".mp4",".mov",".mtv",".wmv",".amv" };
        public static readonly List<string> FileExtensions = new List<string> { ".txt", ".csv", ".xls", ".xlsx", ".doc", ".docx", ".pdf", ".wps", ".wpt", ".et", ".ett" };

        /// <summary>
        /// 判断文件类型
        /// </summary>
        /// <param name="expandName"></param>
        /// <returns></returns>
        public static ZoneType GetFileType(string expandName)
        {
            expandName = expandName.ToLower();
            if (VideoextExtensions.Contains(expandName))
                return ZoneType.Video;
            if (ImageExtensions.Contains(expandName))
                return ZoneType.Photo;
            if (FileExtensions.Contains(expandName))
                return ZoneType.File;
            throw new Exception("不支持此类型的上传！");
        }

        /// <summary>
        /// 按时间构建文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string BuildPath(int typdId = 0)
        {
            return string.Format("/{0}/{1}/{2}/{3}", Enum.GetName(typeof(ImgType), typdId), DateTime.Now.Year,
                                               DateTime.Now.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),
                                               DateTime.Now.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
        }

        /// <summary>
        /// 构建文件名
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string BuildFileName(string extension)
        {
            return (Guid.NewGuid() + extension).ToLower();
        }

        /// <summary>
        /// 将文件流转为byte
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

        /// <summary>
        /// GetFileZoneName
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        private static string GetFileZoneName(ZoneType zoneName)
        {
            var zone = "";
            switch (zoneName)
            {
                case ZoneType.Avatar:
                    zone = "kungge-avatar"; break;
                case ZoneType.Photo:
                    zone = "kungge-photo"; break;
                case ZoneType.Video:
                    zone = "kungge-video"; break;
                case ZoneType.File:
                    zone = "kungge-file"; break;
                default:
                    zone = "kungge-file"; break;
            }
            return zone;
        }

        /// <summary>
        /// 创建路径 如果存在 return false
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

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="zoneName"></param>
        /// <param name="uploadPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string UpLoad(Stream stream, string zoneName, string uploadPath, string fileName)
        {
            CreatePath(uploadPath);
            uploadPath = uploadPath + "\\"+fileName;
            File.WriteAllBytes(uploadPath, SteamToBytes(stream));
            return uploadPath;
        }
    }

}
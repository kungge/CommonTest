using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ToolsUse.CommonHelper
{
    public static class FileHelper
    {
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

        #region 保存图片
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="bt"></param>
        public static void SaveImage(string filePath, byte[] bt)
        {
            try
            {
                File.WriteAllBytes(filePath, bt);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 保存文本文件
        public static void SaveTxtFile(string filePath, string fileName, string txtStr, bool isCover = true)
        {
            try
            {
                CreatePath(filePath);
                if (isCover)
                    File.WriteAllText(filePath + fileName, txtStr, Encoding.Default);
                else
                    File.AppendAllText(filePath + fileName, txtStr, Encoding.Default);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 过滤文件名中特殊字符
        public static string FilterInvalidChar(string fileName, string replaceStr)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c.ToString(), replaceStr);
            }
            return fileName;
        }
        #endregion
    }

}
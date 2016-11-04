using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsUse.CommonHelper;

namespace ToolsUse.Controllers
{
    public class UploadifyController : Controller
    {
        // GET: Uploadify
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UploadTest()
        {
            //文件检测
            if (Request.Files.Count == 0)
            {
                return Json(AjaxResult.Error("无要上传的文件"));
            }
            var file = Request.Files[0];
            if (file == null)
            {
                return Json(AjaxResult.Error("无要上传的文件"));
            }
            if (file.ContentLength > 2 * 1024 * 1024)
            {
                return Json(AjaxResult.Error("文件过大"));
            }
            var fileExt=Path.GetExtension(file.FileName);
            if (!CloudFileHelper.ImageExtensions.Contains(fileExt))
            {
                return Json(AjaxResult.Error("请上传图片格式的文件"));
            }
            var fileType=CloudFileHelper.GetFileType(fileExt);//文件类型
            var buildFileName = CloudFileHelper.BuildFileName(fileExt);//构建文件名称
            var pathName = CloudFileHelper.BuildPath(1);//构建文件路径
            pathName = Server.MapPath("\\upload")+ pathName;
            var physicalPath=CloudFileHelper.UpLoad(file.InputStream,"",pathName, buildFileName);

            return Json(AjaxResult.Success(PhysicalToSiteUrl(physicalPath), "上传成功"));
        }

        #region 信息处理
        //本地路径转换成URL相对路径
        private string PhysicalToSiteUrl(string physicalPath)
        {
            string rootPath = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string siteUrl = physicalPath.Replace(rootPath, ""); //转换成相对路径
            siteUrl = siteUrl.Replace(@"\", @"/");
            siteUrl = siteUrl.Substring(0, 1) != "/" ? "/" + siteUrl : siteUrl;
            return siteUrl;
        }
        //相对路径转换成服务器本地物理路径
        private string ToPhysicalPath(string siteUrl)
        {
            string rootPath = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string physicalPath = rootPath + siteUrl.Replace(@"/", @"\"); //转换成绝对路径
            return physicalPath;
        }
        #endregion

    }
}
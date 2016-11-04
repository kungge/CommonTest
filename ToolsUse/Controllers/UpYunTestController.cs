using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsUse.CommonHelper;

namespace ToolsUse.Controllers
{
    public class UpYunTestController : Controller
    {
        // GET: UpYunTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test1()
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
            var extensionName = Path.GetExtension(file.FileName);
            if (!CloudFileHelper.ImageExtensions.Contains(extensionName))
            {
                return Json(AjaxResult.Error("请上传图片格式的文件"));
            }
            var bucketType = UpyunHelper.GetFileType(extensionName+ "_comprehensive ");//设置只传到综合的服务器kwan-upyun中
            var fileName = UpyunHelper.BuildFileName(extensionName);
            var filePath = UpyunHelper.BuildFilePath(1);
            var url=UpyunHelper.UpLoad(file.InputStream,bucketType,filePath,fileName);
            var rtnUrl = UpyunHelper.ProcessUrl(url,bucketType);
            return Json(AjaxResult.Success(rtnUrl, "上传成功"));
        }



    }
}
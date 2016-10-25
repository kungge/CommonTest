using DemoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProj.Controllers
{
    public class JSTestController : Controller
    {
        // GET: JSTest
        public ActionResult Index()
        {
            return View();
        }

        #region 三级联动
        //http://www.cnblogs.com/eggTwo/p/5991925.html
        public ActionResult ThreeCategory()
        {
            return View();
        }
        public ActionResult ThreeCategoryV2()
        {
            return View();
        }
        public JsonResult GetCategorys(int? pid = null)
        {
            if (pid == null)
            {
                var list = GetAllProductCategoryList().Where(c=>c.LevelFlag == 1).Select(c => new { Value = c.Id, Display = c.Name }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = GetAllProductCategoryList().Where(c => c.ParentId == pid).Select(c => new { Value = c.Id, Display = c.Name }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        private List<ProductCategory> GetAllProductCategoryList()
        {
            List<ProductCategory> list = new List<ProductCategory>();
            //一级
            list.Add(new ProductCategory() { Id=101,Name="电脑办公",LevelFlag=1,ParentId=0});
            //二级
            list.Add(new ProductCategory() { Id = 201, Name = "电脑整机", LevelFlag = 2, ParentId = 101 });
            list.Add(new ProductCategory() { Id = 202, Name = "电脑配件", LevelFlag = 2, ParentId = 101 });
            //三级
            list.Add(new ProductCategory() { Id =301 , Name = "笔记本", LevelFlag = 3, ParentId = 201 });
            list.Add(new ProductCategory() { Id =302, Name = "游戏本", LevelFlag = 3, ParentId = 201 });
            list.Add(new ProductCategory() { Id = 303, Name = "显示器", LevelFlag = 3, ParentId = 202 });
            list.Add(new ProductCategory() { Id = 304, Name = "cpu", LevelFlag = 3, ParentId = 202 });

            //一级
            list.Add(new ProductCategory() { Id = 111, Name = "运动户外", LevelFlag = 1, ParentId = 0 });
            //二级
            list.Add(new ProductCategory() { Id = 211, Name = "运动鞋包", LevelFlag = 2, ParentId = 111 });
            list.Add(new ProductCategory() { Id = 212, Name = "运动服饰", LevelFlag = 2, ParentId = 111 });
            //三级
            list.Add(new ProductCategory() { Id = 311, Name = "跑步鞋", LevelFlag = 3, ParentId = 211 });
            list.Add(new ProductCategory() { Id = 312, Name = "休闲鞋", LevelFlag = 3, ParentId = 211 });
            list.Add(new ProductCategory() { Id = 313, Name = "T恤", LevelFlag = 3, ParentId = 212 });
            list.Add(new ProductCategory() { Id = 314, Name = "运动裤", LevelFlag = 3, ParentId = 212 });

            return list;
        }
        #endregion

    }


}
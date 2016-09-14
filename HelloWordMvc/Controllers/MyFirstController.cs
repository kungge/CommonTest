using HelloWordMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWordMvc.Controllers
{
    public class MyFirstController : Controller
    {
        DbContext context = new EFTestDBEntities2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyViewPage1()
        {
            ViewBag.Message = "我爱马媛媛";
            ViewData["stuName"] = "myy";
            ViewData["StudentName"] = "myy";
            StudentInfo stu = new StudentInfo() { 
            StudentId=1,
            StudentName="马媛媛"
            };
            ViewData.Model = stu;

            var result = context.Set<StudentInfo>().Select(c=>c);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in result)
            {
                list.Add(new SelectListItem() { 
                Text=item.StudentName,
                Value=item.StudentId.ToString()
                });
            }
            ViewData["ListStudent"] = list;
            return View();
        }

        public ActionResult MyViewPage2()
        {
            ViewBag.Message = "我爱马媛媛啊";
            ViewData["mylover"] = "马媛媛";
           ViewData.Model = new PersonInfo() { PersonName="小媛媛",PersonAge=25};
           return View();
        }

        public ActionResult MyViewPage3()
        {
            ViewBag.Message = "我爱马媛媛啊";
            ViewData["mylover"] = "马媛媛";
            ViewData.Model = new StudentInfo() { StudentName = "马小媛媛", StudentAge = 25 };
            return View();
        }

        public ActionResult ViewStudentInfo()
        {
            DbContext context=new EFTestDBEntities2();
            //ViewData.Model=context.Set<StudentInfo>().Where(c=>c.StudentId==1).Select(c=>c);
            ViewData.Model = context.Set<StudentInfo>().Select(c => c);
            return View();
        }

        public ActionResult Details()
        {
            DbContext context = new EFTestDBEntities2();
            ViewData.Model = context.Set<StudentInfo>().Where(c => c.StudentId == 1).Select(c => c);
            return View();
        }

        public ActionResult View2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test1(int id)
        {
            ViewData["id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Text1(int addI)
        {
            
            return View();
        }

        public ActionResult GetUpdStudentInfo(int id)
        {
            StudentInfo stu = context.Set<StudentInfo>().Find(id);
            ViewData.Model = stu;
            return View();
        }

        [HttpPost]
        public ActionResult GetUpdStudentInfo(StudentInfo stu)
        {
            context.Entry(stu).State = EntityState.Modified;
            context.SaveChanges();
            return View();
        }
    }
}

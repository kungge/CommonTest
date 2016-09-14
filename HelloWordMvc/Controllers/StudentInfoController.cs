using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWordMvc.Models;

namespace HelloWordMvc.Controllers
{
    public class StudentInfoController : Controller
    {
        private EFTestDBEntities2 db = new EFTestDBEntities2();

        //
        // GET: /StudentInfo/

        public ActionResult Index()
        {
            var studentinfo = db.StudentInfo.Include(s => s.ClassInfo);        
            return View(studentinfo.ToList());
        }

        //
        // GET: /StudentInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            StudentInfo studentinfo = db.StudentInfo.Find(id);
            if (studentinfo == null)
            {
                return HttpNotFound();
            }
            return View(studentinfo);
        }

        //
        // GET: /StudentInfo/Create

        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.ClassInfo, "ClassId", "ClassName");          
            return View();
        }

        //
        // POST: /StudentInfo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentInfo studentinfo)
        {
            if (ModelState.IsValid)
            {
                db.StudentInfo.Add(studentinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.ClassInfo, "ClassId", "ClassName", studentinfo.ClassId);
            return View(studentinfo);
        }

        //
        // GET: /StudentInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StudentInfo studentinfo = db.StudentInfo.Find(id);
            if (studentinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.ClassInfo, "ClassId", "ClassName", studentinfo.ClassId);
            return View(studentinfo);
        }

        //
        // POST: /StudentInfo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentInfo studentinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.ClassInfo, "ClassId", "ClassName", studentinfo.ClassId);
            return View(studentinfo);
        }

        //
        // GET: /StudentInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            StudentInfo studentinfo = db.StudentInfo.Find(id);
            if (studentinfo == null)
            {
                return HttpNotFound();
            }
            return View(studentinfo);
        }

        //
        // POST: /StudentInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentInfo studentinfo = db.StudentInfo.Find(id);
            db.StudentInfo.Remove(studentinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
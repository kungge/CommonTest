using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EFTest2
{
    class Program
    {
        static void Main(string[] args)
        {

            #region code first
            //DbContext context = new EFTestDBContext();
            //context.Database.CreateIfNotExists();//完成数据库中表的创建
            //ClassInfo c = new ClassInfo() { ClassName = "重点班" };
            //c.StudentInfo.Add(new StudentInfo() { StudentName = "李刚", StudentAge = 23 });
            //c.StudentInfo.Add(new StudentInfo() { StudentName = "刘洋", StudentAge = 22 });
            //context.Set<ClassInfo>().Add(c);
            //context.SaveChanges();
            #endregion

            #region CallContext
            AddClassInfo();
            AddStudentInfo();
            Save();
            #endregion


            Console.WriteLine("ok");
            Console.ReadKey();
        }
        static void AddClassInfo()
        {
            DbContext context = ContextFactory.CreaetContext();
            context.Set<ClassInfo>().Add(new ClassInfo { ClassName="软件开发部"});
        }
        static void AddStudentInfo()
        {
            DbContext context = ContextFactory.CreaetContext();
            ClassInfo c1=context.Set<ClassInfo>().Local.First();
            c1.StudentInfo.Add(new StudentInfo() {StudentName="万坤",StudentAge=25 });          
        }

        static void Save()
        {
            DbContext context = ContextFactory.CreaetContext();
            context.SaveChanges();
        }
    }

    public static class ContextFactory
    {
        public static DbContext CreaetContext()
        {
            DbContext context = CallContext.GetData("Context") as DbContext;
            if (context == null)
            {
                context = new EFTestDBContext();
                CallContext.SetData("Context",context);
            }
            return context;
        }
    }
}

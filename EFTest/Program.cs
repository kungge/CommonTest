using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                YYCardEntitiesTwo context = new YYCardEntitiesTwo();
                //UserInfo user = context.UserInfo.Find(3);
                //Console.WriteLine(user.BirthDate);

                #region 集合版cud
                //EFTestDBModelContainer context = new EFTestDBModelContainer();
                //UserInfo user = new UserInfo();
                //user.UserAccount = "IT1";
                //user.UserName = "万峰";
                //context.UserInfo.Add(user);
                //context.SaveChanges();
                //Console.WriteLine(user.ID + " " + user.UserName);

                //新增
                //UserRole role = new UserRole();
                //role.Name = "角色0523";
                //context.UserRole.Add(role);
                //context.SaveChanges();

                ////修改
                //UserRole role = context.UserRole.Find(3);
                //role.Name = "角色0523Upd";
                //context.SaveChanges();

                //删除
                //UserRole role = context.UserRole.Find(3);
                //context.UserRole.Remove(role);
                //context.SaveChanges();
                #endregion

                #region 状态版（跟踪版）-cud
                //修改
                //UserRole role = new UserRole
                //{
                //    ID = 1,
                //    Name="接单啊",
                //    RoleDescription = "描述接单啊"
                //};
                //context.Entry(role).State = EntityState.Modified;

                ////新增
                //UserRole ur2 = new UserRole
                //{
                //    Name = "激活人员"
                //};
                //context.Entry(ur2).State = EntityState.Added;

                //删除
                //UserRole ur3 = new UserRole
                //{
                //    ID = 4
                //};
                //context.Entry(ur3).State = EntityState.Deleted;


                ////修改某个字段值
                //UserRole ur4 = new UserRole
                //{
                //    ID=2
                //};
                //context.UserRole.Attach(ur4);//注
                //context.Entry(ur4).Property("RoleDescription").CurrentValue = "财务确认收款噢噢噢噢";
                //context.Entry(ur4).Property("RoleDescription").IsModified = true;

                ////context.SaveChanges();

                //context.Configuration.ValidateOnSaveEnabled = false;//注：加这个避免报错：对一个或多个实体的验证失败
                //context.SaveChanges();
                //context.Configuration.ValidateOnSaveEnabled = true;

                #endregion

                #region 导航属性完成新增
                //CustomerType t1 = new CustomerType
                //{
                //    Name = "TestT2"
                //};
                //context.CustomerType.Add(t1);
                //Customer c1 = new Customer
                //{
                //    Name = "TestC1",
                //    CustomerType = t1.ID //建立关系的第1种方式：通过外键
                //};
                //context.Customer.Add(c1);


                //CustomerType t1 = new CustomerType
                //{
                //    Name = "TestT3"
                //};
                //context.CustomerType.Add(t1);
                //Customer c1 = new Customer
                //{
                //    Name = "TestC3",
                //};
                //c1.CustomerType1 = t1;
                //context.Customer.Add(c1);


                //CustomerType t1 = new CustomerType
                //{
                //    Name = "TestT4"
                //};
                //context.CustomerType.Add(t1);
                //Customer c1 = new Customer
                //{
                //    Name = "TestC4"
                //};
                //t1.Customer.Add(c1);//建立关系的第2种方式：导航属性
                //context.Customer.Add(c1);

                //context.SaveChanges();

                #endregion

                #region 查询
                //Console.WriteLine("134:");
                //var result = from u in context.UserInfoes
                //             select u;
                //Console.WriteLine(result.Count());

                //var result = from u in context.UserInfoes
                //             select new
                //             {
                //                 ID = u.ID,
                //                 AccountInfo = new
                //                 {
                //                     UserAccount=u.UserAccount,
                //                     UserPwd=u.Pwd
                //                 }
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("150:{0},{1},{2}",item.ID,item.AccountInfo.UserAccount,item.AccountInfo.UserPwd);
                //}

                //var result = from u in context.UserInfoes
                //             select new
                //             {
                //                 ID = u.ID,
                //                 AccountInfo = new
                //                 {
                //                     UserAccount = u.UserAccount,
                //                     UserPwd = u.Pwd,
                //                     IsSafe=u.Pwd.Length>8?true:false
                //                 }
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("166:{0},{1},{2},{3}", item.ID, item.AccountInfo.UserAccount, item.AccountInfo.UserPwd,item.AccountInfo.IsSafe);
                //}

                //var result = from u in context.UserInfo
                //             where u.UserAccount.StartsWith("y")
                //             select u;
                //Console.WriteLine(result.Count());

                //var result=from t in context.CustomerType
                //           join o in context.Customer on t.ID equals o.CustomerType
                //           where t.Name == "银行金融保险类"
                //           select o;
                //Console.WriteLine(result.Count());

                //var result=from t in context.CustomerType
                //           from o in t.Customer  //导航属性的用法
                //           where t.Name == "银行金融保险类"
                //           select o;
                //Console.WriteLine(result.Count());

                //var result = from t in context.CustomerType
                //             from o in t.Customer 
                //             where t.Name == "银行金融保险类" && o.CustomerBelong==35
                //             select o;
                //Console.WriteLine(result.Count());

                //var result = from t in context.CustomerType
                //             from o in t.Customer 
                //             where t.Name == "银行金融保险类" && o.CustomerBelong == 35
                //             select o;
                //foreach (var item in result)
                //{
                //    Console.WriteLine("ID={0},CustomerName={1}", item.ID, item.Name);
                //}

                //var result = from t in context.CustomerType
                //             from o in t.Customer
                //             where t.Name == "银行金融保险类" && o.CustomerBelong == 35
                //             select new {
                //                         CustomerID=o.ID,
                //                         CustomerName=o.Name,
                //                         TypeName=t.Name
                //                };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("CustomerID={0},CustomerName={1},TypeName={2}", item.CustomerID, item.CustomerName,item.TypeName);
                //}

                //var result = from t in context.CustomerType
                //             from o in t.Customer
                //             where t.Name == "银行金融保险类" && o.CustomerBelong == 35
                //             select new
                //             {
                //                 o.ID,
                //                o.Name
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("CustomerID={0},CustomerName={1}", item.ID, item.Name);
                //}

                //var result = from t in context.CustomerTypes
                //             from o in t.Customers
                //             where t.Name == "银行金融保险类" && o.CustomerBelong == 35
                //             select new MyCustomer
                //             {
                //                 MID = o.ID,
                //                 MName = o.Name
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("CustomerID={0},CustomerName={1}", item.MID, item.MName);
                //}

                //Console.WriteLine(result.Count());
                #endregion

                #region 查询2-练习(来自LinQ入门教程：一步一步学Linq to sql系列)
                //var result = from c in context.Customers
                //             where c.OrderInfoes.Count>0 && c.Name.Length>6
                //             select new
                //             {
                //                 CustomerName = c.Name,
                //                 OrderCount = c.OrderInfoes.Count
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("252:CustomerName={0},OrderCount={1}",item.CustomerName,item.OrderCount);
                //}


                ////分页
                var result=(from c in context.Customers select c).OrderBy(c=>c.ID).Skip((2-1)*10).Take(10);
                foreach (var item in result)
                {
                    Console.WriteLine("261:CustomerID={0},CustomerName={1},OrderCount={2}", item.ID,item.Name, item.OrderInfoes.Count);
                }

                //分组
                //var result = from c in context.Customers
                //             group c by c.CustomerBelong into g
                //             where g.Count() > 20
                //             orderby g.Count() descending
                //             select new
                //             {
                //                 CustomerBelongId = g.Key,
                //                 CustomerBelongCount = g.Count()
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("274:CustomerBelongId={0},CusomerBelongCount={1}", item.CustomerBelongId, item.CustomerBelongCount);
                //}

                //分组2-多字段
                //var result = from c in context.Customers
                //             group c by new { c.CustomerType, c.CustomerBelong } into g
                //             where g.Count()>5
                //             orderby g.Count() descending
                //             select new
                //             {
                //                 CustomerType = g.Key.CustomerType,
                //                 CustomerBelong = g.Key.CustomerBelong,
                //                 HasCount=g.Count()
                //             };
                //foreach(var item in result)
                //{
                //    Console.WriteLine("289:CustomerType={0},CustomerBelong={1},HasCount={2}",item.CustomerType,item.CustomerBelong,item.HasCount);
                //}

                //distinct
                //var res = (from c in context.Customers select c.CustomerType).Distinct();
                //foreach (var item in res)
                //{
                //    Console.WriteLine("299:{0}",item);
                //}

                //union 查询客户名字是上海开头和客户属于50这个用户的客户信息，并按照客户名字排序   注：相同的信息不会重复
                //var res = (from c in context.Customers where c.Name.StartsWith("上海") select c).Union(
                //    from c in context.Customers where c.CustomerBelong == 50 select c
                //    ).OrderBy(c => c.Name);
                //foreach (var item in res)
                //{
                //    Console.WriteLine("308:{0},{1},{2}", item.Name,item.CustomerBelong,item.ID);
                //}

                //contact 查询客户名字是上海开头和客户属于50这个用户的客户信息 相同的信息不回过滤
                //var res = (from c in context.Customers where c.Name.StartsWith("上海") select c).Concat(
                //    from c in context.Customers where c.CustomerBelong == 50 select c
                //    ).OrderBy(c => c.Name);
                //foreach (var item in res)
                //{
                //    Console.WriteLine("308:{0},{1},{2}", item.Name, item.CustomerBelong, item.ID);
                //}

                //intersect 查询客户名字是上海开头和客户属于50这个用户的客户信息的交集
                //var res = (from c in context.Customers where c.Name.StartsWith("上海") select c).Intersect(
                //    from c in context.Customers where c.CustomerBelong == 50 select c
                //    ).OrderBy(c => c.Name);
                //foreach (var item in res)
                //{
                //    Console.WriteLine("308:{0},{1},{2}", item.Name, item.CustomerBelong, item.ID);
                //}

                //except 查询客户名字是上海开头并排除客户属于50的客户信息
                //var res = (from c in context.Customers where c.Name.StartsWith("上海") select c).Except(
                //    from c in context.Customers where c.CustomerBelong == 50 select c
                //    ).OrderBy(c => c.Name);
                //foreach (var item in res)
                //{
                //    Console.WriteLine("308:{0},{1},{2}", item.Name, item.CustomerBelong, item.ID);
                //}

                 //子查询 求客户订单数超过两个的客户信息
                //var res = from c in context.Customers
                //          where
                //          (from o in context.OrderInfoes group o by o.CustomerId into g where g.Count() > 2 select g.Key).Contains(c.ID)
                //          select c;
                //foreach(var item in res)
                //{
                //    Console.WriteLine("345:{0},{1}",item.ID,item.Name);
                //}
                  
      
                //in 查询客户类型属于12,13和14的客户信息
                //var res = from c in context.Customers
                //          where
                //          (new int[] { 12, 13, 14 }).Contains(c.CustomerType)
                //          select c;
                //foreach (var item in res)
                //{
                //    Console.WriteLine("356:{0},{1},{2}",item.ID,item.Name,item.CustomerType);
                //}

                //join  内连接 查询有分类的客户
                //var res = from c in context.Customers
                //          join t in context.CustomerTypes
                //          on c.CustomerType equals t.ID
                //          select c;
                //foreach (var item in res)
                //{
                //    Console.WriteLine("{0},{1},{2}",item.ID,item.Name,item.CustomerType);
                //}


                //join 外链接 查询客户 没有分类的也能查出来
                //var res = from c in context.Customers
                //          join t in context.CustomerTypes
                //          on c.CustomerType equals t.ID
                //          into b
                //          from m in b.DefaultIfEmpty()
                //          orderby c.Name descending
                //          select c.Name;
                //foreach (var item in res)
                //{
                //    Console.WriteLine("{0}", item);
                //}


                #endregion

                #region 存储过程（来自LinQ入门教程：一步一步学Linq to sql系列）
                //单结果集存储过程
                //var res = from c in context.Proc_TestInqCustomer()
                //          where c.Name.Contains("上海")
                //          select c;
                //foreach (var item in res)
                //{
                //    Console.WriteLine("{0},{1}",item.ID,item.Name);
                //}

                //带输入和输出参数 注：wk经测试context.Proc_TestWithInputOutPut(5,ref rowCount)在此报错：第二个参数无效         
                //int rowCount = 10;
                //ObjectParameter objpm = new ObjectParameter("rowCount",rowCount);
                //context.Proc_TestWithInputOutPut(5,objpm);//注：这样使用并未得到希望的结果
                //Console.WriteLine("{0}", objpm);


               //有返回值
                //int rtn=context.Proc_TestWithReturnValue(5);//注：奇怪返回值不是预期的值
                //int rtn = context.Proc_TestWithReturnValue2(5);
                //Console.WriteLine("{0}",rtn);

                //新增数据
                //context.Proc_TestAreaInfoIns("上海静安区","2");

                //删除数据
                //context.Proc_TestAreInfoDel(2);

                //修改数据
                //context.Proc_TestAreInfoUpd(1,"上海黄浦区");

                #endregion

                #region lambda
                //var result = context.UserInfo.Where(u => u.UserAccount.Length > 3).Select(u => u);//单条件
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}_{1}",item.UserAccount,item.BirthDate);
                //}

                //var result = context.UserInfo.Where(u => u.UserAccount.Length > 3).Select(u => u.UserAccount);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item);
                //}

                //var result = context.UserInfo.Where(u => u.UserAccount.Length > 3 && u.Pwd.Length>6).Select(u => u);//多条件
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}_{1}", item.UserAccount, item.Pwd);
                //}

                //var result = context.UserInfo.Where(u => u.UserAccount.Length > 3||u.Pwd.Length>6).Select(u => u);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}_{1}", item.UserAccount, item.Pwd);
                //}

                //var result = context.UserInfo.Where(u => u.UserAccount.Contains("y")).Select(u => u);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}_{1}", item.UserAccount, item.Pwd);
                //}

                //连接查询
                //var result = from c in context.Customer
                //             join t in context.CustomerType on c.CustomerType equals t.ID
                //             select new 
                //             {
                //                 CustomerID=c.ID,
                //                 CustomerName=c.Name,
                //                 CustomerTypeName=t.Name
                //             };
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}_{1}",item.CustomerName,item.CustomerTypeName);
                //}

                //多from查询
                //var result = from c in context.Customer
                //             from t in c.CustomerType1  //注：这里竟然不行 todo
                //             select c;

                //分页查询
                //int currentPage = 1, pageSize = 10;
                //var result = context.Customer.Where(c => c.Name.Length > 10)
                //    .OrderByDescending(c => c.Name).Skip((currentPage - 1) * pageSize).Take(pageSize)
                //    .Select(c=>c);

                //var result = GetPageData(currentPage,pageSize,context);
                //var result = GetPageData2<Customer, string>(currentPage, pageSize, c => c.Name.Length > 10, c => c.Name);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}
                #endregion


                #region 延迟加载
                //DbContext context=new YYCardEntities();

                //var result = context.Set<Customer>()
                //    .Where(c => c.Name.Length > 10)
                //    .OrderByDescending(c => c.Name)
                //    .Skip(0)
                //    .Take(10);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}",item.Name);
                //}

                //var result = context.Set<Customer>().AsQueryable()
                //    .Where(c => c.Name.Length > 18);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}

                //var result = context.Set<Customer>().AsEnumerable()// 为非延迟加载
                //    .Where(c => c.Name.Length > 18);
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}

                //var result = context.Set<Customer>().Include("CustomerType1");//将导航属性包含进来
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}

                //var result = context.Set<CustomerType>().Include("Customer");
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}

                //var result = context.Set<CustomerType>().ToList();//不是延迟加载
                //foreach (var item in result)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}

                //var result = context.Set<CustomerType>().First();
                //Console.WriteLine(result.Name+":");
                //foreach (var item in result.Customer)
                //{
                //    Console.WriteLine("{0}", item.Name);
                //}


                #endregion

                #region model first
                //DbContext context = new EFTestDBModelContainer();
                //DepartmentInfo dept = new DepartmentInfo()
                //{
                //    DepartmentName = "财务部"
                //};
                //dept.UserInfo.Add(new UserInfo()
                //{
                //    UserName = "马媛媛",
                //    UserAge = 25
                //});
                //context.Set<DepartmentInfo>().Add(dept);
                //context.SaveChanges();
                #endregion


                Console.WriteLine("ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error:" + ex.Message);
            }

            Console.ReadKey();
        }

        public static IQueryable<Customer> GetPageData(int currentPage, int pageSize, YYCardEntitiesTwo context)
        {
            var result = context.Customers.Where(c => c.Name.Length > 10)
                    .OrderByDescending(c => c.Name).Skip((currentPage - 1) * pageSize).Take(pageSize)
                    .Select(c => c);
            return result;
        }

        public static IQueryable<T> GetPageData2<T, TKey>(int currentPage, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda) where T : class
        {
            DbContext context = new YYCardEntitiesTwo();
            var result = context.Set<T>().Where(whereLambda)
                    .OrderByDescending(orderByLambda).Skip((currentPage - 1) * pageSize).Take(pageSize)
                    .Select(c => c);
            return result;
        }
    }
    public class MyCustomer
    {
        public int MID { get; set; }
        public string MName { get; set; }
    }
}

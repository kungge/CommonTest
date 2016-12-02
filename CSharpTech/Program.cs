using CSharpTech.LiamWang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTech
{
    class Program
    {
        static void Main(string[] args)
        {
            //***********************************LiamWang小牛之路系列
            //refer:http://www.cnblogs.com/willick/p/3208427.html

            #region 特性
            //string input = string.Empty;
            //Order order;
            //do
            //{
            //    Console.WriteLine("请输入订单号：");
            //    input = Console.ReadLine();
            //    order = new Order { OrderId = input };
            //}
            //while (!IsValid(order));
            //Console.WriteLine("订单号输入正确，按任意键退出！");

            #endregion

            #region 自动属性
            //Person p = new Person();
            //p.Name = "jack";
            //p.Age = 180;
            #endregion

            #region 扩展方法
            //var ps = new Product[] {
            //    new Product {Name = "Kayak", Price = 275M},
            //new Product {Name = "Lifejacket", Price = 48.95M},
            //new Product {Name = "Soccer ball", Price = 19.50M},
            //new Product {Name = "Corner flag", Price = 34.95M}
            //};
            //var carts = new ShoppingCart() {
            //    ProductList=new List<Product>()
            //    {
            //        new Product {Name = "Kayak", Price = 275},
            //        new Product {Name = "Lifejacket", Price = 48.95M},
            //    new Product {Name = "Soccer ball", Price = 19.50M},
            //    new Product {Name = "Corner flag", Price = 34.95M}
            //    }
            //};
            //Console.WriteLine("{0}，{1}",ps.TotalPrices(),carts.TotalPrices());

            #endregion

            #region Lambda表达式
            //// 创建商品集合
            //IEnumerable<Product> products = new ShoppingCart
            //{
            //    ProductList = new List<Product> {
            //        new Product {Name = "西瓜", Category = "水果", Price = 2.3M},
            //        new Product {Name = "苹果", Category = "水果", Price = 4.9M},
            //        new Product {Name = "ASP.NET MCV 入门", Category = "书籍", Price = 19.5M},
            //        new Product {Name = "ASP.NET MCV 提高", Category = "书籍", Price = 34.9M}
            //    }
            //};

            ////Func<Product,bool> selector = delegate (Product p) {
            ////    return p.Category == "水果";
            ////};
            ////Func<Product, bool> selector = p => p.Category == "水果";
            ////var list = products.Filter(selector);
            ////var list = products.Filter(x=>x.Category=="水果");
            //var list = products.Filter(x => x.Category == "水果"||x.Price>30);
            //foreach (var item in list)
            //{
            //    Console.WriteLine("{0},{1}",item.Name,item.Price);
            //}
            #endregion

            #region Linq
            Product[] products = {
    new Product {Name = "西瓜", Category = "水果", Price = 2.3M},
    new Product {Name = "苹果", Category = "水果", Price = 4.9M},
    new Product {Name = "空心菜", Category = "蔬菜", Price = 2.2M},
    new Product {Name = "地瓜", Category = "蔬菜", Price = 1.9M}
};
            //var results = from product in products
            //              orderby product.Price descending
            //              select new
            //              {
            //                  product.Name,
            //                  product.Price
            //              };
            ////打印价钱最高的三个商品
            //int count = 0;
            //foreach (var p in results)
            //{
            //    Console.WriteLine("商品：{0}，价钱：{1}", p.Name, p.Price);
            //    if (++count == 3) break;
            //}

            //        var results = products
            //.OrderByDescending(e => e.Price)
            //.Take(3)
            //.Select(e => new { e.Name, e.Price });

            //        foreach (var p in results)
            //        {
            //            Console.WriteLine("商品：{0}，价钱：{1}", p.Name, p.Price);
            //        }

            var results = products
    .OrderByDescending(e => e.Price)
    .Take(3)
    .Select(e => new { e.Name, e.Price });

            //在Linq语句之后对products[1]重新赋值
            products[1] = new Product { Name = "榴莲", Category = "水果", Price = 22.6M };

            //打印
            foreach (var p in results)
            {
                Console.WriteLine("商品：{0}，价钱：{1}", p.Name, p.Price);
            }

            #endregion

            //********************************************************************


            Console.ReadKey();
        }

        private static bool IsMemberValid(int inputLength,MemberInfo member)
        {
            foreach(object attr in member.GetCustomAttributes())
            {
                if(attr is MyStringLengthAttribute)
                {
                    var myStringLengthAttr = (MyStringLengthAttribute)attr;
                    string displayName = myStringLengthAttr.DisplayName;
                    string msg = myStringLengthAttr.ErrorMsg;
                    int minLength = myStringLengthAttr.MinLength;
                    int maxLength = myStringLengthAttr.MaxLength;
                    if (inputLength< minLength || inputLength> maxLength)
                    {
                        Console.WriteLine(msg, displayName, minLength,maxLength);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsValid(Order order)
        {
            if (order == null) return false;
            foreach(var o in typeof(Order).GetProperties())
            {
                if (IsMemberValid(order.OrderId.Length, o))
                {
                    return true;
                }
            }
            return false;
        }

    }
}

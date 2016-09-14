using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 排序和分组
            //int[] array1 = { 11,3,37,8,11,9,3,9,10,16};
            //var res = from a in array1
            //          group a by a into g
            //          select new
            //          {
            //              aItem = g.Key,
            //              aCount = g.Count()
            //          };
            //foreach (var i in res)
            //{
            //    Console.WriteLine("{0},{1}",i.aItem,i.aCount);
            //}

            //var res = array1.Select(a => a + 1).Where(b=>b>10).OrderBy(a=>a);
            //foreach (var i in res)
            //{
            //    Console.WriteLine("{0}",i);
            //}

            //string[] array2 = {"345","834","3","55","11","15","123" };
            //var res = array2.Select(a => Convert.ToInt32(a)).Where(a => a > 100).OrderBy(a=>a);
            //foreach (var i in res)
            //{
            //    Console.WriteLine("{0}", i);
            //}
            #endregion

            #region linq常用扩展方法  
            /*
             下面的方法都是IEnumerable<T>的扩展方法：
            Average计算平均值； Min最小元素；Max最大元素；Sum元素总和； Count元素数量；
            Concat连接两个序列；//Unoin all
            Contains序列是否包含指定元素；
            Distinct取得序列中的非重复元素；
            Except获得两个序列的差集；
            Intersect获得两个序列的交集；
            First取得序列第一个元素；
            Single取得序列的唯一一个元素，如果元素个数不是1个，则报错；！！！严谨的程序。
            FirstOrDefault 取得序列第一个元素，如果没有一个元素，则返回默认值；
            Linq只能用于范型的序列，IEnumerable<T>，对于非范型，可以用Cast或者OfType
            IEnumerable的方法：
            Cast<TResult>：由于Linq要针对范型类型操作，对于老版本.Net类等非范型的IEnumerable序列可以用Cast方法转换为范型的序列。ArrayList l; IEnumerable<int> il = l.Cast<int>();
            OfType<TResult>：Cast会尝试将序列中所有元素都转换为TResult类型，如果待转换的非范型序列中含有其他类型，则会报错。OfType则是只将序列中挑出指定类型的元素转换到范型序列中。
            Linq的效率怎么样（小数据量、对性能要求不高的环节用linq很方便，而且延迟加载机制降低了内存占用，比一般人写的程序效率都高）            
             */


            //练习 去掉最高分和最低分，求平均分
            //int[] array1 = { 90,75,89,35,77, 93,60,92,100,16 };
            //var res1 = from i in array1
            //          select i;
            //var res2 = from i in array1
            //           where i > array1.Min() && i < array1.Max()
            //           select i;

            //var res3 = array1.Select(a => array1.Average()).Where(a => a > array1.Min()).Where(a => a < array1.Max());//注wk：where没有起作用
            //Console.WriteLine("{0},{1},{2}", res1.Average(), res2.Average(),res3.Average());


            #endregion

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}

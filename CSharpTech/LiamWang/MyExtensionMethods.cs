using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTech.LiamWang
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> ps)
        {
            decimal total = 0;
            foreach(var d in ps)
            {
                total += d.Price;
            }
            return total;
        }

        public static IEnumerable<Product> Filter(this IEnumerable<Product> products,Func<Product,bool> selector)
        {
            foreach(var item in products)
            {
                if (selector(item))
                {
                    yield return item;
                }
            }
        }
    }
}

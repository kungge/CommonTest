using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeTests.DependencyInjection.Willick
{
    //默认折扣计算器
    public class DefaultDiscountHelper : IDiscountHelper
    {
        ////v1.0
        //public decimal ApplyDiscount(decimal totalParam)
        //{
        //    return (totalParam - (1m / 10m * totalParam));
        //}

        //v2.0 指定值绑定
        public decimal DiscountSize { get; set; }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (DiscountSize / 10m * totalParam));
        }
    }
}

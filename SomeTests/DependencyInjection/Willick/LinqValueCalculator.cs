using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeTests.DependencyInjection.Willick
{
    public class LinqValueCalculator : IValueCalculator
    {
        //v1.0
        //public decimal ValueProducts(params Product[] products)
        //{
        //    return products.Sum(x=>x.Price);
        //}

        //v2.0
        private IDiscountHelper discounterHelper;
        public LinqValueCalculator(IDiscountHelper pd)
        {
            discounterHelper = pd;
        }
        public decimal ValueProducts(params Product[] products)
        {
            return discounterHelper.ApplyDiscount(products.Sum(x=>x.Price));
        }
    }
}

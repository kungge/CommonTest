using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeTests.DependencyInjection.Willick
{
    public class IterativeValueCalculator:IValueCalculator
    {
        public decimal ValueProducts(params Product[] products)
        {
            decimal totalValue = 0;
            foreach (Product p in products)
            {
                totalValue += p.Price;
            }
            return totalValue;
        }
    }
}

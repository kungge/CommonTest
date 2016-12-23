using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeTests.DependencyInjection.Willick
{
    public class LimitShoppingCart:ShoppingCart
    {
        public LimitShoppingCart(IValueCalculator calcParam) : base(calcParam) { }

        public override decimal CalculateStockValue()
        {
            var limitProducts = products.Where(x=>x.Price<ItemLimit);
            var totalPrice = calculator.ValueProducts(limitProducts.ToArray());//过滤价格超过了上限的商品
            return totalPrice;
        }
        public decimal ItemLimit { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeTests.DependencyInjection.Willick
{
    public class ShoppingCart
    {
        //v1.0
        //public decimal CalculateStockValue()
        //{
        //    Product[] products = {
        //        new Product {Name = "西瓜", Category = "水果", Price = 2.3M},
        //        new Product {Name = "苹果", Category = "水果", Price = 4.9M},
        //        new Product {Name = "空心菜", Category = "蔬菜", Price = 2.2M},
        //        new Product {Name = "地瓜", Category = "蔬菜", Price = 1.9M}
        //    };
        //    IValueCalculator calculator = new LinqValueCalculator();
        //    var totalPrice= calculator.ValueProducts(products);
        //    return totalPrice;
        //}

        ////v2.0
        //protected IValueCalculator calculator;
        //public ShoppingCart(IValueCalculator calc)
        //{
        //    calculator = calc;
        //}
        //public decimal CalculateStockValue()
        //{
        //    Product[] products = {
        //        new Product {Name = "西瓜", Category = "水果", Price = 2.3M},
        //        new Product {Name = "苹果", Category = "水果", Price = 4.9M},
        //        new Product {Name = "空心菜", Category = "蔬菜", Price = 2.2M},
        //        new Product {Name = "地瓜", Category = "蔬菜", Price = 1.9M}
        //    };//11.3 11.3-4.9=6.4
        //    var totalPrice = calculator.ValueProducts(products);
        //    return totalPrice;
        //}

        //v3.0 增加派生类
        protected IValueCalculator calculator;
        protected Product[] products;
        public ShoppingCart(IValueCalculator calcParam)
        {
            calculator = calcParam;
            products = new[]{
            new Product {Name = "西瓜", Category = "水果", Price = 2.3M},
            new Product {Name = "苹果", Category = "水果", Price = 4.9M},
            new Product {Name = "空心菜", Category = "蔬菜", Price = 2.2M},
            new Product {Name = "地瓜", Category = "蔬菜", Price = 1.9M}
            };
        }
        public virtual decimal CalculateStockValue()
        {
            var totalPrice = calculator.ValueProducts(products);
            return totalPrice;
        }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTech.LiamWang
{
    public class ShoppingCart : IEnumerable<Product>
    {
        public List<Product> ProductList { get; set; }
        public IEnumerator<Product> GetEnumerator()
        {
            return ProductList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

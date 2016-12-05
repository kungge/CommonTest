using ApiTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiTech.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products
        Product[] products = new Product[]
         {
                new Product { Id = 1101, Name = "苹果", Category = "水果", Price = 3 },
                 new Product { Id = 1102, Name = "桔子", Category = "水果", Price = 2 },
                  new Product { Id = 1103, Name = "香蕉", Category = "水果", Price = 4 },
                new Product { Id = 1201, Name = "白菜", Category = "蔬菜", Price = 3.75M },
                new Product { Id = 1202, Name = "芹菜", Category = "蔬菜", Price = 16.99M },
                 new Product { Id = 1301, Name = "床", Category = "家具", Price = 169.99M },
                  new Product { Id = 1302, Name = "沙发", Category = "家具", Price = 126.99M }
         };

        public IEnumerable<Product> GetAllProducts()
        {
            //tips：ie打开会下载一个文件。chrome会显示xml数据。firefox会显示xml数据。The reason for the difference is that Internet Explorer and Firefox send different Accept headers, so the web API sends different content types in the response.
            //出现这种差别的原因是IE和Firefox发送了不同的Accept报头，因此，Web API在响应中发送了不同的内容类型。
            return products;
        }
        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return products.Where(
                (p) => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProj.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelFlag { get; set; }
        public int ParentId { get; set; }
    }
}
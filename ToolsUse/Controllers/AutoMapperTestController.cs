using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace ToolsUse.Controllers
{
    //自定义类型转换
    public class AutoMapperTestController : Controller
    {
        // GET: AutoMapper
        public ActionResult Index()
        {
            //int a=Mapper.Map<int>("1108");            
            //int c=Mapper.Map<string, int>("1109");


            Mapper.Initialize(cfg => cfg.CreateMap<Source, Destination>());
            //or
            var config=new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>());


            var mapper=config.CreateMapper();
            //or
            var mapper2=new Mapper(config);

            Source s = new Source();
            var d=mapper.Map<Destination>(s);
            //or
            var d2=Mapper.Map<Destination>(s);

            return View();
        }
    }
    public class Source
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
    }

    public class Destination
    {
        public int Value1 { get; set; }
        public DateTime Value2 { get; set; }
        public Type Value3 { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNetTech.Controllers
{
    public class AsyncTestController : Controller
    {
        // GET: AsyncTest
        public ActionResult Index()
        {
            return View();
        }
        //refer：http://www.cnblogs.com/wisdomqq/archive/2012/03/29/2417723.html
        public async Task<FileResult> Download()
        {
            using (FileStream fs = new FileStream("E:\\jobFiles\\myproj\\online\\CommonTest\\KnowledgeTest\\asyncTest.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                byte[] data = new byte[fs.Length];
                await fs.ReadAsync(data, 0, data.Length);
                return new FileContentResult(data, "application/octet-stream");
            }
        }
    }
}
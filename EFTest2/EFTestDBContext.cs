using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest2
{
    public partial class EFTestDBContext:DbContext
    {
        public EFTestDBContext()
            : base("name = EFTestDBContext")
        {          
        }

        public DbSet<StudentInfo> StudentInfo { get; set; }
        public DbSet<ClassInfo> ClassInfo { get; set; }
    }
}

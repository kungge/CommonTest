using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest2
{
    [Table("ClassInfo")]
    public partial class ClassInfo
    {
        public ClassInfo()
        {
            StudentInfo = new HashSet<StudentInfo>();
        }
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public virtual ICollection<StudentInfo> StudentInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest2
{
    [Table("StudentInfo")]
    public partial  class StudentInfo
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        [ForeignKey("ClassInfo")]
        public int ClassId { get; set; }

        public virtual ClassInfo ClassInfo { get; set; }
    }
}

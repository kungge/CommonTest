//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcLog
    {
        public int ID { get; set; }
        public int ProcType { get; set; }
        public string ProcTypeDescription { get; set; }
        public Nullable<int> CardId { get; set; }
        public Nullable<int> ProcDetailId { get; set; }
        public string CardStartNum { get; set; }
        public string CardEndNum { get; set; }
        public string CardNumSection { get; set; }
        public Nullable<int> CardCount { get; set; }
        public string Processer { get; set; }
        public Nullable<System.DateTime> ProcTime { get; set; }
        public Nullable<int> AllotFlag { get; set; }
        public Nullable<int> SourceFlag { get; set; }
    }
}

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
    
    public partial class InvoiceInfo
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public string InvoiceNum { get; set; }
        public string Title { get; set; }
        public string InvoiceContent { get; set; }
        public decimal Amount { get; set; }
        public string OutInvoiceCompany { get; set; }
        public string InvoiceRemark { get; set; }
        public Nullable<int> SourceFlag { get; set; }
        public int IsDel { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Revisor { get; set; }
        public Nullable<System.DateTime> ReviseTime { get; set; }
    }
}

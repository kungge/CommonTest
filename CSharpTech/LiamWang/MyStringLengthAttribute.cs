using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTech.LiamWang
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
    public class MyStringLengthAttribute:Attribute
    {
        public MyStringLengthAttribute(string displayName,int maxLength)
        {
            this.DisplayName = displayName;
            this.MaxLength = maxLength;
        }
        public string DisplayName { get; private set; }
        public int MaxLength { get; private set; }
        public string ErrorMsg { get; set; }
        public int MinLength { get; set; }
    }
}

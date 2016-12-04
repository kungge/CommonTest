using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTech.LiamWang
{
    public class Order
    {
        [MyStringLength("订单号",6,MinLength =3,ErrorMsg ="{0}长度必须在{1}和{2}之间，请重新输入！")]
        public string OrderId { get; set; }
    }
}

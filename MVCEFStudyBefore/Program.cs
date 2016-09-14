using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEFStudyBefore
{
    class Program
    {
        static void Main(string[] args)
        {
            //MyAdd1 m = new MyAdd1(Add1);//委托
            //int bb = m(2, 3);
            //int cc = Add3(m);
            //Console.WriteLine();

            int dd=Add3((a, b) => { return a + b; });//匿名方法
            Console.WriteLine(dd);
            
            Console.ReadKey();
        }
        public static int Add1(int a,int b)
        {
            return a+b;
        }
        public static int Add2(int a, int b)
        {
            return a*b;
        }
        static int Add3(MyAdd1 a)
        {
            return a(1,2);
        }
    }
    public static class MyTestExtension
    {
        public static int MyToInt(this string str)//扩展方法
        {
            return Convert.ToInt32(str);
        }

        public static void P1()
        {
            int bb="789".MyToInt();
            int cc=MyToInt("678");
        }

    }
    public delegate int MyAdd1(int a,int b);
}

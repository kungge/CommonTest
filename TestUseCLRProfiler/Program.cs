using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUseCLRProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
            Test2();
            Console.ReadKey();
        }
        protected static void Test1()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            string str = "";
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    str += "string append1= ";
                    str += i.ToString() + " ";
                    str += "string append2= ";
                    str += j.ToString() + " ";
                }
            }
            sp.Stop();
            Console.WriteLine("Test1 Time={0}", sp.Elapsed.ToString());
        }
        protected static void Test2()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    str.Append("string append1= ");
                    str.Append(i.ToString());
                    str.Append("string append2=");
                    str.Append(j.ToString());
                }
            }
            sp.Stop();
            Console.WriteLine("Test2 Time={0}", sp.Elapsed.ToString());
        }
    }
}

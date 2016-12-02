using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTech.LiamWang
{
    public class Person
    {
        public string Name { get; set; }
        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (value > 150 || value < 0)
                {
                    Console.WriteLine("age error");
                    return;
                }
                _age = value;
            }
        }
    }
}

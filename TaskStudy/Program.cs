using System;
using System.Threading.Tasks;

namespace TaskStudy
{
    class Program
    {
        //https://www.kekyo.net/2016/12/06/6186
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Start");

            var cls1 = new SomeClass1();
            Console.WriteLine($"Val1={cls1.Val}");
            Console.WriteLine($"Val1={cls1.Val}");
            Console.WriteLine($"Val1={cls1.Val}");


            var cls2 = new SomeClass2();
            Console.WriteLine($"Val2={cls2.Val}");
            Console.WriteLine($"Val2={cls2.Val}");
            Console.WriteLine($"Val2={cls2.Val}");

            var cls3 = new SomeClass3();
            Console.WriteLine($"Val3={cls3.Val}");
            Console.WriteLine($"Val3={cls3.Val}");
            Console.WriteLine($"Val3={cls3.Val}");

            var cls4 = new SomeClass4();
            Console.WriteLine($"Val4={cls4.Val}");
            Console.WriteLine($"Val4={cls4.Val}");
            Console.WriteLine($"Val4={cls4.Val}");

        }
    }
}

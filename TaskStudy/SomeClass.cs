using System;
using System.Threading.Tasks;
using ValueTaskSupplement;

namespace TaskStudy
{
    class SomeClass1
    {
        public int Val => _val.Result;
        private ValueTask<int> _val => HeavyMethod();

        private static async ValueTask<int> HeavyMethod()
        {
            Console.WriteLine("StartWait1");
            await Task.Delay(1000);
            return 1;
        }
    }

    class SomeClass2
    {
        public int Val => _val;
        private readonly static int _val = HeavyMethod().Result;

        private static async ValueTask<int> HeavyMethod()
        {
            Console.WriteLine("StartWait2");
            await Task.Delay(2000);
            return 2;
        }
    }

    class SomeClass3
    {
        public int Val => _val.Result;
        private readonly ValueTask<int> _val = ValueTaskEx.Lazy(async () =>
        {
            Console.WriteLine("StartWait3");
            await Task.Delay(2000);
            return 3;
        });
    }

    class SomeClass4
    {
        private int _val;

        public int Val
        {
            get
            {
                while (!taskFinished)
                {
                    WaitAsync().Wait();
                }
                return _val;
            }
        }

        private readonly Task myTask;
        private bool taskFinished;

        public SomeClass4()
        {
            myTask = HeavyMethod().ContinueWith(t =>
            {
                Console.WriteLine("Waiting Complete");
                taskFinished = true;
            });
        }

        private async Task HeavyMethod()
        {
            Console.WriteLine("StartWait4");
            await Task.Delay(2000);
            _val = 4;
        }

        private async Task WaitAsync()
        {
            Console.Write("w");
            await Task.Delay(500);
        }

    }

}

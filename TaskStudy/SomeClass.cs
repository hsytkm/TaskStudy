using System;
using System.Threading.Tasks;
using ValueTaskSupplement;

namespace TaskStudy
{
    /// <summary>プロパティを取得する度にHeavyMethodが実行される</summary>
    class SomeClass1
    {
        public int Val => GetValAsync.Result;
        private ValueTask<int> GetValAsync => HeavyMethod();

        private static async ValueTask<int> HeavyMethod()
        {
            Console.WriteLine("StartWait1");
            await Task.Delay(1000);
            return 1;
        }
    }

    /// <summary>プロパティ取得の初回のみHeavyMethodが実行される</summary>
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

    /// <summary>プロパティ取得の初回のみHeavyMethodが実行される(ValueTaskSupplement使用)</summary>
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

    /// <summary>プロパティ取得時に、値が生成されるまで待つ</summary>
    class SomeClass4
    {
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
        private int _val;

        private bool taskFinished;

        public SomeClass4()
        {
            // 戻り値のTask捨てるの良くない（内部で例外が発生すると永久に終わらない）
            _ = HeavyMethod().ContinueWith(t =>
            {
                _val = t.Result;
                Console.WriteLine("Value is prepared.");
                taskFinished = true;
            });
        }

        private static async Task<int> HeavyMethod()
        {
            Console.WriteLine("StartWait4");
            await Task.Delay(2000);
            return 4;
        }

        private static async Task WaitAsync()
        {
            Console.Write("w");
            await Task.Delay(500);
        }
    }

}

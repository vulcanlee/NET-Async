using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C008.使用執行緒做到非同步處理作業並取得執行結果
{
    class Program
    {
        static void Main(string[] args)
        {
            int shareData = 0;
            Console.WriteLine($"建立新執行緒物件");
            Thread thread1 = new Thread(() =>
            {
                shareData = 0;
                Console.WriteLine($"執行緒1 的 ID={Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(900);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100); Console.Write("X");
                    shareData += i;
                }
                Console.WriteLine();
            });
            Console.WriteLine($"啟動執行緒");

            thread1.Start();

            thread1.Join();
            Console.WriteLine($"執行緒1 執行完畢");
            Console.WriteLine($"執行緒1 執行結果為 {shareData}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}

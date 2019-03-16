﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D013.非同步程式碼執行順序
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteThreadId(1);
            Task fooTask = LogAsync("程式開始");

            WriteThreadId(5);
            Console.WriteLine($"模擬主程式正在忙碌中，約1秒鐘");
            //模擬主程式正在忙碌中
            Thread.Sleep(1000);

            await fooTask;

            WriteThreadId(6);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private async static Task LogAsync(string msg)
        {
            WriteThreadId(2);
            await Task.Run(() =>
            {
                WriteThreadId(3);
                Console.WriteLine($"模擬寫到資料庫內，約2秒鐘");
                Thread.Sleep(2000); // 模擬寫到資料庫內
            });
            WriteThreadId(4);
        }

        private static void WriteThreadId(int checkpoint)
        {
            Console.WriteLine("Checkpoint: {0}, thread: {1}", checkpoint, Thread.CurrentThread.ManagedThreadId);
        }
    }
}

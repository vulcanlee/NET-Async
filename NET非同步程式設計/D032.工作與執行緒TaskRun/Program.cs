using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace D032.工作與執行緒TaskRun
{
    class Program
    {
        static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        static bool runningLoop = true;
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static object locker = new object();
        static void Main(string[] args)
        {
            PrintThreadInformation("準備啟動非同步作業");
            Thread.Sleep(500);

            Task task = Task.Run(() =>
            {
                PrintThreadInformation("模擬非同步作業約3秒中");
                Thread.Sleep(3000);
                autoResetEvent.Set();
            });

            //ThreadPool.QueueUserWorkItem(_ =>
            //{
            //    PrintThreadInformation($"模擬非同步作業約3秒中");
            //    Thread.Sleep(3000);
            //    autoResetEvent.Set();
            //});

            PrintThreadInformation($"等待非同步作業中");
            Thread.Sleep(500);

            autoResetEvent.WaitOne();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
        }
        static void Output(string message)
        {
            //queue.Enqueue(message);
            Console.WriteLine(message);
        }
        //static void ShowThreadInformation()
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        while (runningLoop)
        //        {
        //            PrintThreadInformation();
        //            Thread.Sleep(500);
        //        }
        //    });
        //    thread.IsBackground = true;
        //    thread.Start();
        //}
        static void PrintThreadInformation(string message)
        {
            lock (locker)
            {
                Console.WriteLine(message);
                #region 處理程序使用的執行緒
                int processHasThreads = Process.GetCurrentProcess().Threads.Count;
                #endregion

                #region 執行緒集區使用的執行緒
                int workerAvailableThreads;
                int ioCompletionPortAvailableThreads;
                int workerMinThreads;
                int ioCompletionPortMinThreads;
                int workerMaxThreads;
                int ioCompletionPortMaxThreads;
                int threadCount;
                ThreadPool.GetAvailableThreads(out workerAvailableThreads, out ioCompletionPortAvailableThreads);
                ThreadPool.GetMinThreads(out workerMinThreads, out ioCompletionPortMinThreads);
                ThreadPool.GetMaxThreads(out workerMaxThreads, out ioCompletionPortMaxThreads);
                threadCount = ThreadPool.ThreadCount;
                Output($"   Process has Threads:{processHasThreads}");
                Output($"   ThreadPool exist Threads:{threadCount}");
                Output($"   Worker : Available {workerAvailableThreads} / " +
                    $"Min:{ workerMinThreads} / Max:{ workerMaxThreads}");
                Output($"   I/O    : Available {ioCompletionPortAvailableThreads} / " +
                    $"Min:{ ioCompletionPortMinThreads} / Max:{ ioCompletionPortMaxThreads}");
                Output($"");
                #endregion
            }
        }
    }
}

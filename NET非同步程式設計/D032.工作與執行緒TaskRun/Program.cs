//#define TaskDelay
//#define TaskRun
//#define ThreadPoolQueue
//#define HttpClientTask
//#define AsyncMethodForHttpClientTask
//#define AsyncMethodForHttpClientSync
//#define TaskCompletionSourceWithTaskRun
//#define TaskCompletionSourceWithTaskRunSync
#define NoAwaitAsyncMethod
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
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
        delegate Task MyMethodAsyncDel(string message);
        static void Main(string[] args)
        {
            PrintThreadInformation("準備啟動非同步作業");
            Thread.Sleep(500);

#if TaskDelay
            #region Task.Delay
            Task task = Task.Delay(3000);

            PrintThreadInformation($"等待非同步作業中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if TaskRun
            #region Task.Run
            Task task = Task.Run(() =>
            {
                PrintThreadInformation("模擬非同步作業約3秒中");
                Thread.Sleep(3000);
            });

            PrintThreadInformation($"等待非同步作業中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if ThreadPoolQueue
            #region ThreadPool.QueueUserWorkItem
            ThreadPool.QueueUserWorkItem(_ =>
            {
                PrintThreadInformation($"模擬非同步作業約3秒中");
                Thread.Sleep(3000);
                autoResetEvent.Set();
            });

            PrintThreadInformation($"等待非同步作業中");
            Thread.Sleep(500);

            autoResetEvent.WaitOne();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if HttpClientTask
            #region 使用 HttpClient 非同步工作
            var task = new HttpClient().GetStringAsync($"https://hyperfullstack.azurewebsites.net/" +
                $"api/HandOnLab/AddAsync/8/9/3");

            PrintThreadInformation($"等待非同步作業中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if AsyncMethodForHttpClientTask
            #region 在非同步方法內呼叫 HttpClient 非同步工作 
            MyMethodAsyncDel myMethodAsyncHandler ;
            myMethodAsyncHandler = async x =>
            {
                var task = new HttpClient().GetStringAsync($"https://hyperfullstack.azurewebsites.net/" +
                    $"api/HandOnLab/AddAsync/8/9/3");

                PrintThreadInformation($"等候取得 HttpClient 結果中");
                Thread.Sleep(500);

                await task;
            };
            var task = myMethodAsyncHandler("Hi");

            PrintThreadInformation($"等待非同步方法中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if AsyncMethodForHttpClientSync
            #region 在非同步方法內尚未執行到 await 就立即返回
            MyMethodAsyncDel myMethodAsyncHandler ;
            myMethodAsyncHandler = async x =>
            {
                if(string.IsNullOrEmpty(x))
                {
                    PrintThreadInformation($"引數不正確，立即結束");
                    return;
                }

                var task = new HttpClient().GetStringAsync($"https://hyperfullstack.azurewebsites.net/" +
                    $"api/HandOnLab/AddAsync/8/9/3");

                PrintThreadInformation($"等候取得 HttpClient 結果中");
                Thread.Sleep(500);

                await task;
            };
            var task = myMethodAsyncHandler("");

            PrintThreadInformation($"等待非同步方法中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if TaskCompletionSourceWithTaskRun
            #region 使用 TaskCompletionSource 建立非同步工作物件
            MyMethodAsyncDel myMethodAsyncHandler ;
            myMethodAsyncHandler = x =>
            {
                TaskCompletionSource tcs = new TaskCompletionSource();

                Task.Run(() =>
                {
                    PrintThreadInformation("模擬非同步作業約3秒中");
                    Thread.Sleep(3000);
                    tcs.TrySetResult();
                });

                return tcs.Task;
            };
            var task = myMethodAsyncHandler("Hi");

            PrintThreadInformation($"等待非同步方法中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if TaskCompletionSourceWithTaskRunSync
            #region 使用 TaskCompletionSource 建立非同步工作物件，尚未觸發非同步作業，就立即返回
            MyMethodAsyncDel myMethodAsyncHandler ;
            myMethodAsyncHandler = x =>
            {
                TaskCompletionSource tcs = new TaskCompletionSource();
                if (string.IsNullOrEmpty(x))
                {
                    PrintThreadInformation($"引數不正確，立即結束");
                    tcs.TrySetResult();
                    return tcs.Task;
                }

                Task.Run(() =>
                {
                    PrintThreadInformation("模擬非同步作業約3秒中");
                    Thread.Sleep(3000);
                    tcs.TrySetResult();
                });

                return tcs.Task;
            };
            var task = myMethodAsyncHandler("");

            PrintThreadInformation($"等待非同步方法中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif

#if NoAwaitAsyncMethod
            #region 使用 一個沒有 await 的非同步方法
            MyMethodAsyncDel myMethodAsyncHandler ;
            myMethodAsyncHandler = async x =>
            {
                PrintThreadInformation("模擬同步作業約3秒中");
                Thread.Sleep(3000);

                return ;
            };
            var task = myMethodAsyncHandler("");

            PrintThreadInformation($"等待非同步方法中");
            Thread.Sleep(500);

            task.Wait();

            PrintThreadInformation($"非同步作業完成");
            Thread.Sleep(500);
            #endregion
#endif
        }

        static void Output(string message)
        {
            //queue.Enqueue(message);
            Console.WriteLine(message);
        }
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

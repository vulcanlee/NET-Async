using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D011.工作啟動與狀態
{
    class 承諾類型的工作
    {
        static void Main(string[] args)
        {
            string lastStatus = "";
            Task monitorTask = new Task(() => Thread.Sleep(0));
            bool IsBegin = false;

            #region 監視工作狀態是否已經有變更，並且顯示出最新的狀態值
            new Thread(() =>
            {
                while (true)
                {
                    if (IsBegin == false) return;
                    var tmpStatus = monitorTask.Status;
                    if (tmpStatus.ToString() != lastStatus)
                    {
                        Console.WriteLine($"Status : {monitorTask.Status}");
                        Console.WriteLine($"IsCompleted : {monitorTask.IsCompleted}");
                        Console.WriteLine($"IsCanceled : {monitorTask.IsCanceled}");
                        Console.WriteLine($"IsFaulted : {monitorTask.IsFaulted}");
                        var exceptionStatusX = (monitorTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
                        Console.WriteLine($"Exception : {exceptionStatusX}");
                        Console.WriteLine();
                        lastStatus = tmpStatus.ToString();
                    }
                }
            })
            { IsBackground = false }.Start();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            monitorTask = tcs.Task;
            IsBegin = true;

            Thread.Sleep(1000);
            tcs.SetResult(null);
            //tcs.SetCanceled();
            //tcs.SetException(new Exception("喔喔，發生例外異常"));

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

            #endregion
        }
    }
}

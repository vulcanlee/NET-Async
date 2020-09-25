using System;
using System.Threading;

namespace D031實驗在執行緒內拋出例外異常可否捕捉到
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main Thread Id :" +
               $"{Thread.CurrentThread.ManagedThreadId}");
            try
            {
                throw new Exception("Capture Main Exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine($"New Thread Id :" +
                        $"{Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(3000);
                    throw new Exception("Oh Oh");
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}

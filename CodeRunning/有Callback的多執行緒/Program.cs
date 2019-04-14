using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 有Callback的多執行緒
{
    class MyAsyncClass
    {
        public EventHandler OnCompletion;
        public void DoRun()
        {
            Console.Write(5);
            ThreadPool.QueueUserWorkItem(x =>
            {
                Console.Write(6);
                OnCompletion?.Invoke(this, EventArgs.Empty);
                Console.Write(7);
            });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(1);
            MyAsyncClass myAsyncObject = new MyAsyncClass();
            myAsyncObject.OnCompletion += (s, e) =>
            {
                Console.Write(2);
                Thread.Sleep(1000);
            };
            Console.Write(3);
            myAsyncObject.DoRun();
            Console.Write(4);
            Console.ReadKey();
        }
    }
}

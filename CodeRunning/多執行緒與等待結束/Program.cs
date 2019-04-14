using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 多執行緒與等待結束
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(1);
            Thread thread1 = new Thread(() =>
            {
                Console.Write(2);
                Thread.Sleep(900);
                Console.Write(3);
            });
            Console.Write(4);
            thread1.Start();
            Console.Write(5);
            thread1.Join();
            Console.Write(6);
            Console.ReadKey();
        }
    }
}

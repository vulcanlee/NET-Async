using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 非同步方法與非同步工作
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(1);
            Task task = MainAsync();
            Console.Write(2);
            task.Wait();
            Console.Write(3);
            Console.ReadKey();
        }
        static async Task MainAsync()
        {
            Console.Write(4);
            await MyTaskAsync();
            Console.Write(5);

        }

        static Task MyTaskAsync()
        {
            Console.Write(6);
            var myTask = Task.Run(() =>
            {
                Console.Write(7);
            });
            Console.Write(8);
            return myTask;
        }
    }
}

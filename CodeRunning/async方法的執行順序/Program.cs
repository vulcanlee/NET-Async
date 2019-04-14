using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async方法的執行順序
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
            Console.Write(5);
        }
    }
}

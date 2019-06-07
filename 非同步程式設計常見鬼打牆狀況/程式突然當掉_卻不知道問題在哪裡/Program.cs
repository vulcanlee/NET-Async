using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 程式突然當掉_卻不知道問題在哪裡
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await MethodAsync();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static async Task MethodAsync()
        {
            await MethodTaskAsync();
            throw new ArgumentNullException("引數空值例外異常產生了");
        }
        static Task MethodTaskAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(2000);
                //throw new ArgumentNullException("引數空值例外異常產生了");
            });
        }
    }
}



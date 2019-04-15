using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 為何會拋出不同例外異常
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //await MethodAsync();
                MethodAsync().Wait(); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
            }
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static async Task MethodAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                throw new ArgumentNullException("引數空值例外異常產生了");
            });
        }
    }
}

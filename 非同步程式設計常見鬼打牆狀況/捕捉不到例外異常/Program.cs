using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 捕捉不到例外異常
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MethodAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static async void MethodAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                throw new ArgumentNullException("引數空值例外異常產生了");
            });
        }
    }
}

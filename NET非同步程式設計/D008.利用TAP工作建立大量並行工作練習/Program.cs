using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";

            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    HttpClient client = new HttpClient();
                    Console.WriteLine($"第 {i}-1 測試開始時間 {DateTime.Now}");
                    var result = client.GetStringAsync(url).Result;
                    Console.WriteLine($"第 {i}-1 測試結果內容 {result}");
                    Console.WriteLine($"第 {i}-1 測試結束時間 {DateTime.Now}");

                    Console.WriteLine($"第 {i}-2 測試開始時間 {DateTime.Now}");
                    result = client.GetStringAsync(url).Result;
                    Console.WriteLine($"第 {i}-2 測試結果內容 {result}");
                    Console.WriteLine($"第 {i}-2 測試結束時間 {DateTime.Now}");
                });
            }


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}

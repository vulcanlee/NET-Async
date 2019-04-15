using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace 程式不會打死結卡住了Console
{
    class Program
    {
static void Main(string[] args)
{
    var value = MainAsync().Result;
    Console.WriteLine(value);
}
static async Task<string> MainAsync()
{
    var sumTask = SumAsync(168, 89);
    var result = sumTask.Result;
    return result;
}
static async Task<string> SumAsync(int a, int b)
{
    var client = new HttpClient();
    var task = client.GetStringAsync(
        "https://lobworkshop.azurewebsites.net" +
        $"/api/RemoteSource/Add/{a}/{b}/2");
    var result = await task;
    return result;
}
    }
}

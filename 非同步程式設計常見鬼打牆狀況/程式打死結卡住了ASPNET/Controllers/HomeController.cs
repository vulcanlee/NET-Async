using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace 程式打死結卡住了ASPNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sumTask = SumAsync(168, 89);
            var result = sumTask.Result;
            return View();
        }
        //public async Task<ActionResult> Index()
        //{
        //    var sumTask = SumAsync(168, 89);
        //    var result = await sumTask;
        //    return View();
        //}
        async Task<string> SumAsync(int a, int b)
        {
            var client = new HttpClient();
            var task = client.GetStringAsync(
                "https://lobworkshop.azurewebsites.net" +
                $"/api/RemoteSource/Add/{a}/{b}/2");
            var result = await task;
            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
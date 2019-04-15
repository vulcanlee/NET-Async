using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace 無法正常更新物件ASPNET.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, e) =>
            {
                ViewBag.Name = "OK" + e.Result;
            };
            wc.DownloadStringAsync(new Uri("http://www.mocky.io/v2/5c9c774f3600006c56d97069"));
            while (wc.IsBusy)
            {
                Thread.Sleep(30);
            }
            return View();
        }
        //public async Task<ActionResult> Index()
        //{
        //    var sc = SynchronizationContext.Current;
        //    WebClient wc = new WebClient();
        //    wc.DownloadStringCompleted += (s, e) =>
        //    {
        //        sc.Post(x =>
        //        {
        //            ViewBag.Name = "OK" + e.Result;
        //        }, null);
        //    };
        //    wc.DownloadStringAsync(new Uri("http://www.mocky.io/v2/5c9c774f3600006c56d97069"));
        //    while (wc.IsBusy)
        //    {
        //        Thread.Sleep(30);
        //    }
        //    return View();
        //}

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
﻿using System;
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
        //public async Task<ActionResult> Index()
        //{
        //    WebClient wc = new WebClient();

        //    wc.DownloadStringCompleted += (s, e) =>
        //    {
        //        ViewBag.Name = "OK" + e.Result;
        //    };
        //    wc.DownloadStringAsync(new Uri("https://lobworkshop.azurewebsites.net" +
        //             $"/api/RemoteSource/Add/99/87/2"));
        //    while (wc.IsBusy)
        //    {
        //        Thread.Sleep(30);
        //    }

        //    Thread.Sleep(3000);
        //    return View();
        //}

        //public async Task<ActionResult> Index()
        //{
        //    WebClient wc = new WebClient();

        //    var task = wc.DownloadStringTaskAsync(new Uri("https://lobworkshop.azurewebsites.net" +
        //    $"/api/RemoteSource/Add/99/87/2"));
        //    string result = await task;
        //    //ViewBag.Name = "OK" + result;
        //    await Task.Run(() => ViewBag.Name = "OK" + result);

        //    return View();
        //}

        public async Task<ActionResult> Index()
        {
            WebClient wc = new WebClient();

            var task = wc.DownloadStringTaskAsync(new Uri("https://lobworkshop.azurewebsites.net" +
            $"/api/RemoteSource/Add/99/87/2"));
            task.Wait();
            string result = task.Result;
            ViewBag.Name = "OK" + result;

            return View();
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
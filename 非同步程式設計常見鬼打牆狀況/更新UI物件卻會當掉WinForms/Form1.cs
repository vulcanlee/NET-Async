using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 更新UI物件卻會當掉WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            await Task.Run(async () =>
             {
                 var client = new HttpClient();
                 var task = client.GetStringAsync(
                     "https://lobworkshop.azurewebsites.net" +
                     $"/api/RemoteSource/Add/99/87/2");
                 var result = await task;
                 label1.Text = result;
             });
        }

        //private async void Button1_Click(object sender, EventArgs e)
        //{
        //    Action<string> UpdateUI = x =>
        //    {
        //        label1.Text = x;
        //    };
        //    await Task.Run(async () =>
        //    {
        //        var client = new HttpClient();
        //        var task = client.GetStringAsync(
        //            "https://lobworkshop.azurewebsites.net" +
        //            $"/api/RemoteSource/Add/99/87/2");
        //        var result = await task;
        //        this.Invoke(UpdateUI, result);
        //    });
        //}
    }
}

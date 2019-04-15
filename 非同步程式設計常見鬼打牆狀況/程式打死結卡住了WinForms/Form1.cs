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

namespace 程式打死結卡住了WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            var sumTask = SumAsync(168, 89);
            var result = sumTask.Result;
            label1.Text = result;
        }
        async Task<string> SumAsync(int a, int b)
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

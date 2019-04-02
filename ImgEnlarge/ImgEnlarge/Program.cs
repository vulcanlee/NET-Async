using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgEnlarge
{
    public class ImageProcess
    {
        /// <summary>
        /// 刪除目的目錄下的所有檔案與目錄，清空
        /// </summary>
        /// <param name="destinationPath">產生目的目錄路徑</param>
        public void Clean(string destinationPath)
        {
            if (Directory.Exists(destinationPath))
            {
                var allImageFiles = Directory.GetFiles(destinationPath, "*", SearchOption.AllDirectories);
                foreach (var item in allImageFiles)
                {
                    File.Delete(item);
                }

                Directory.Delete(destinationPath, true);
            }
        }
        /// <summary>
        /// 根據指定縮放比例，在來源目錄下找出 .png / .jpeg 圖片檔案，進行圖片的縮放作業，並且產生在目的目錄下
        /// </summary>
        /// <param name="sourcePath">圖片來源目錄路徑</param>
        /// <param name="destinationPath">產生圖片目的目錄路徑</param>
        /// <param name="scale">縮放比例</param>
        public void ResizeImage(string sourcePath, string destinationPath, double scale)
        {
            var allFiles = SearchImages(sourcePath);
            foreach (var imageFile in allFiles)
            {
                Image imgPhoto = Image.FromFile(imageFile);

                int sourceWidth = imgPhoto.Width;
                int sourceHeight = imgPhoto.Height;
                int destionatonWidth = (int)(sourceWidth * scale);
                int destionatonHeight = (int)(sourceHeight * scale);
                Bitmap processedImage = Process((Bitmap)imgPhoto, sourceWidth, sourceHeight, destionatonWidth, destionatonHeight);

                string destinationFile = imageFile.Replace(sourcePath, destinationPath);
                string path = Path.GetDirectoryName(destinationFile);
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                processedImage.Save(destinationFile);
            }
        }
        /// <summary>
        /// 找出指定目錄下的 .png / .jpeg 圖片檔案名稱
        /// </summary>
        /// <param name="sourcePath">圖片來源目錄路徑</param>
        /// <returns></returns>
        List<string> SearchImages(string sourcePath)
        {
            var allPNGFiles = Directory.GetFiles(sourcePath, "*.png", SearchOption.AllDirectories);
            var allJPEGFiles = Directory.GetFiles(sourcePath, "*.jpeg", SearchOption.AllDirectories);
            List<string> allFiles = new List<string>();
            allFiles.AddRange(allPNGFiles);
            allFiles.AddRange(allJPEGFiles);
            return allFiles;
        }
        /// <summary>
        /// 針對指定圖片進行縮放作業
        /// </summary>
        /// <param name="originImage">圖片來源</param>
        /// <param name="oriwidth">原始寬度</param>
        /// <param name="oriheight">原始高度</param>
        /// <param name="width">新圖片的寬度</param>
        /// <param name="height">新圖片的高度</param>
        /// <returns></returns>
        Bitmap Process(Bitmap originImage, int oriwidth, int oriheight, int width, int height)
        {
            Bitmap resizedbitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(resizedbitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(originImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, oriwidth, oriheight), GraphicsUnit.Pixel);
            return resizedbitmap;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = @"D:\Vulcan\GitHub\Xamarin2018\XFNavi";
            string destinationPath = @"D:\Vulcan\XX";
            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageProcess.ResizeImage(@"D:\Vulcan\GitHub\Xamarin2018\XFNavi", @"D:\Vulcan\XX", 2.0);
            sw.Stop();
            Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}

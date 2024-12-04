using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media.Imaging;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Weather
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        WeatherDust Data;
        WeatherDustData WeatherDustData;
        private async void Init(){
            IntPtr WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var WindowID = Win32Interop.GetWindowIdFromWindow(WindowHandle);
            AppWindow AppWindow = AppWindow.GetFromWindowId(WindowID);
            AppWindow.Resize(new Windows.Graphics.SizeInt32(400, 400));
            string CsvPath = Windows.ApplicationModel.Package.Current.InstalledPath + "//"+  "Assets\\WeatherXY.csv";
            Data = new WeatherDust(CsvPath);
            await Task.Delay(1500);
            Data.GetData();
            await Task.Delay(500);
            WeatherDustData = Data.GetWeahterDust();
            Update();
        }
        private void Update()
        {
            Temperature.Text = WeatherDustData.Temperaure.ToString() + "¡ÆC";
            Humidity.Text = WeatherDustData.humidity.ToString() + "%";
            string url ="";
            switch (WeatherDustData.Dust) {
                case DustEnum.Good:
                    Dust.Text = "Good";
                    url = "Assets\\FaceLaugh.png";
                    break;
                case DustEnum.Nomal:
                    Dust.Text = "Nomal";
                    url = "Assets\\FaceMeh.png";
                    break;
                case DustEnum.Bad:
                    Dust.Text = "Bad";
                    url = "Assets\\FaceFrown.png";
                    break;
                case DustEnum.VeryBad:
                    Dust.Text = "VeryBad";
                    url = "Assets\\FaceTired.png";
                    break;
            }
            var bitmap = new BitmapImage(new Uri(Windows.ApplicationModel.Package.Current.InstalledPath + "//" + url));
            DustImage.Source = bitmap;
            switch (WeatherDustData.Weather)
            {
                case WeatherEnum.Sunny:
                    url = "Assets\\Sun.png";
                    break;
                case WeatherEnum.LittleClude:
                    url = "Assets\\CludSun.png";
                    break;
                case WeatherEnum.RangeClude:
                    url = "Assets\\Clude.png";
                    break;
                case WeatherEnum.Raniny:
                    url = "Assets\\RainClude.png";
                    break;
                case WeatherEnum.Snow:
                    url = "Assets\\SnowClude.png";
                    break;
                case WeatherEnum.Moon:
                    url = "Assets\\Moon.png";
                    break;
                case WeatherEnum.LiitleMoon:
                    url = "Assets\\CludeMoon.png";
                    break;
            }
            bitmap = new BitmapImage(new Uri(Windows.ApplicationModel.Package.Current.InstalledPath+"//" + url));
            WeatherImage.Source = bitmap;
        }
        public  MainWindow()
        {
            this.InitializeComponent();
            Init();
        }

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
            Data.GetData();
            await Task.Delay(500);
            WeatherDustData = Data.GetWeahterDust();
            Update();
        }
    }
}

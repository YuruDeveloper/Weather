using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Windowing;
using Microsoft.UI;
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
        public MainWindow()
        {
            this.InitializeComponent();
            IntPtr WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var WindowID = Win32Interop.GetWindowIdFromWindow(WindowHandle);
            AppWindow AppWindow = AppWindow.GetFromWindowId(WindowID);
            AppWindow.Resize(new Windows.Graphics.SizeInt32(400, 400));
            string CsvPath = Windows.ApplicationModel.Package.Current.InstalledPath + "\\Assets\\WeatherXY.csv";
            Data = new WeatherDust(CsvPath);
        }
    }
}

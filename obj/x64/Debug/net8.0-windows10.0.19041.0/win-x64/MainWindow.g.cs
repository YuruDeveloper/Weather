﻿#pragma checksum "C:\Users\Cecil\Desktop\Weather\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6DA822DDDD75ED4732C9F278CC13F7D04D56607C02D3D21298F3B37F063A8868"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weather
{
    partial class MainWindow : 
        global::Microsoft.UI.Xaml.Window, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainWindow.xaml line 28
                {
                    this.WeatherImage = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Image>(target);
                }
                break;
            case 3: // MainWindow.xaml line 47
                {
                    global::Microsoft.UI.Xaml.Controls.Button element3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element3).Click += this.Button_Click;
                }
                break;
            case 4: // MainWindow.xaml line 43
                {
                    this.Humidity = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 5: // MainWindow.xaml line 37
                {
                    this.DustImage = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Image>(target);
                }
                break;
            case 6: // MainWindow.xaml line 38
                {
                    this.Dust = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 7: // MainWindow.xaml line 32
                {
                    this.Temperature = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


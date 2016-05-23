using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyNhaSach
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //check app: can not open more times
            Bus.AppHandler.AppCantOpenMoreTimes();
            //auto update
            Bus.AppHandler.SetupAutoUpdaters();
            //run app
            base.OnStartup(e);
        }
    }
}

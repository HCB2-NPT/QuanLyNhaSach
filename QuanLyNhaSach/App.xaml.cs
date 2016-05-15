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
            var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length > 1)
                Managers.ErrorManager.Current.CantOpenAppMoreTimes.Call();
            //update: bills
            Adapters.BillAdapter.FixedBillsOverTime();
            //run app
            base.OnStartup(e);
        }
    }
}

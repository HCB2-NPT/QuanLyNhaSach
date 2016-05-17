using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuanLyNhaSach.Assets.Scripts
{
    public class AutoUpdate
    {
        public delegate void UpdateHandler(AutoUpdate updater);

        public UpdateHandler Updater { get; set; }

        public DispatcherTimer Timer { get; set; }

        public AutoUpdate(TimeSpan interval, UpdateHandler updater, bool runNow = true)
        {
            Updater = updater;
            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
            if (interval == null)
                Timer.Interval = new TimeSpan(0, 0, 1);
            else
                Timer.Interval = interval;
            Timer.Start();

            if (runNow)
                Updater(this);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Updater(this);
        }
    }
}

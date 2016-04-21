using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace QuanLyNhaSach.Assets.Scripts
{
    public class EventDoubleClick
    {
        public Window Window { get; private set; }

        public UIElement Control { get; private set; }

        public EventDoubleClickDelegate Action { get; set; }

        public delegate void EventDoubleClickDelegate(EventDoubleClick sender);

        private DispatcherTimer _timer = new DispatcherTimer();
        private int _times = 0;

        public EventDoubleClick(Window window, UIElement control, EventDoubleClickDelegate action, int time2reset = 400)
        {
            Window = window;
            Control = control;
            Control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
            Action = action;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, time2reset % 1000);
            _timer.IsEnabled = true;
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _times = 0;
            _timer.Stop();
        }

        void Control_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_times == 0)
                _timer.Start();
            _times++;
            if (_times == 2)
            {
                _times = 0;
                _timer.Stop();
                Action(this);
            }
        }
    }
}

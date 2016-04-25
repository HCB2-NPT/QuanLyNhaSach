using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyNhaSach.Assets.Scripts
{
    public class EventClickOut
    {
        public Window Window { get; private set; }

        public FrameworkElement Control { get; private set; }

        public List<FrameworkElement> Without { get; private set; }

        public EventClickOutDelegate Action { get; set; }

        public delegate void EventClickOutDelegate(EventClickOut sender);

        public EventClickOut(Window window, FrameworkElement control, EventClickOutDelegate action)
        {
            Window = window;
            Control = control;
            Without = new List<FrameworkElement>();
            Window.PreviewMouseDown += Window_PreviewMouseDown;
            Action = action;
        }

        public static System.Drawing.Point GetCursorPosition()
        {
            return System.Windows.Forms.Cursor.Position;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Drawing.Point p = GetCursorPosition();
            System.Windows.Point P;
            if (Without != null)
                foreach (var item in Without)
                {
                    P = item.PointToScreen(new Point(0, 0));
                    if (p.X >= P.X && p.X <= P.X + item.ActualWidth && p.Y >= P.Y && p.Y <= P.Y + item.ActualHeight)
                        return;
                }
            P = Control.PointToScreen(new Point(0, 0));
            if (p.X < P.X || p.X > P.X + Control.ActualWidth || p.Y < P.Y || p.Y > P.Y + Control.ActualHeight)
                Action(this);
        }
    }
}

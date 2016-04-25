using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyNhaSach.Assets.Scripts
{
    public class WindowsResizer
    {
        private UIElement _control;
        private Window _window;
        private Point _dragDelta;

        public delegate void Resize(WindowsResizer resizer, double wBef, double wAf, double hBef, double hAf);
        public event Resize WhenResize;

        public WindowsResizer(Window window, UIElement control)
        {
            _window = window;
            _control = control;

            _control.MouseLeftButtonDown += MouseLeftButtonDown;
            _control.MouseLeftButtonUp += MouseLeftButtonUp;
            _control.MouseEnter += _control_MouseEnter;
            _control.MouseLeave += _control_MouseLeave;
            _control.MouseMove += MouseMove;
        }

        void _control_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        void _control_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeAll;
        }

        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragDelta = e.GetPosition(_control);
            Mouse.Capture(_control);
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (Equals(_control, Mouse.Captured))
            {
                var w = _window.Width + e.GetPosition(_control).X - _dragDelta.X;
                var h = _window.Height + e.GetPosition(_control).Y - _dragDelta.Y;
                double wBefore = _window.Width, hBefore = _window.Height;
                if (w >= _window.MinWidth) _window.Width = w;
                if (h >= _window.MinHeight) _window.Height = h;
                if (WhenResize != null) WhenResize(this, wBefore, _window.Width, hBefore, _window.Height);
            }
        }

        void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Equals(_control, Mouse.Captured))
                Mouse.Capture(null);
        }

        public void Normal()
        {
            double wBefore = _window.Width, hBefore = _window.Height;
            _window.WindowState = WindowState.Normal;
            if (WhenResize != null) WhenResize(this, wBefore, _window.Width, hBefore, _window.Height);
        }

        public void Minimize()
        {
            double wBefore = _window.Width, hBefore = _window.Height;
            _window.WindowState = WindowState.Minimized;
            if (WhenResize != null) WhenResize(this, wBefore, _window.Width, hBefore, _window.Height);
        }

        public void Maximize()
        {
            double wBefore = _window.Width, hBefore = _window.Height;
            _window.WindowState = WindowState.Maximized;
            if (WhenResize != null) WhenResize(this, wBefore, _window.Width, hBefore, _window.Height);
        }
    }
}

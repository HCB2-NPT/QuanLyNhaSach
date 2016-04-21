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
    public class WindowsDragger
    {
        private readonly UIElement _panel;
        private readonly Window _window;
        private Point _dragDelta;

        public WindowsDragger(Window window, UIElement panel)
        {
            _panel = panel;
            _window = window;

            _panel.MouseLeftButtonDown += MouseLeftButtonDown;
            _panel.MouseLeftButtonUp += MouseLeftButtonUp;
            _panel.MouseMove += MouseMove;
			
        }

        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragDelta = e.GetPosition(_panel);
            Mouse.Capture(_panel);
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (Equals(_panel, Mouse.Captured))
            {
                var pos = _window.PointToScreen(e.GetPosition(_panel));
                var verifiedPos = CoerceWindowBound(pos - _dragDelta);
                _window.Left = verifiedPos.X;
                _window.Top = verifiedPos.Y;
            }
        }

        void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Equals(_panel, Mouse.Captured))
                Mouse.Capture(null);
        }

        private Vector CoerceWindowBound(Vector newPoint)
        {
            // Snap to the current desktop border
            var screen = WpfScreen.GetScreenFrom(_window);
            var wa = screen.WorkingArea;
            if (newPoint.X < wa.Top) newPoint.X = wa.Top;
            if (newPoint.Y < wa.Left) newPoint.Y = wa.Left;
            if (_window.Width + newPoint.X > wa.Right) newPoint.X = wa.Right - _window.Width;
            if (_window.Height + newPoint.Y > wa.Bottom) newPoint.Y = wa.Bottom - _window.Height;
            return newPoint;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public class DataManager : INotifyPropertyChanged
    {
        private static DataManager _current = null;
        public static DataManager Current
        {
            get
            {
                if (_current == null)
                    _current = new DataManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        private string _MainWindow_MaxMin = Manager.Current.Icon.WindowState_Maximize;
        public string MainWindow_MaxMin { get { return _MainWindow_MaxMin; } set { _MainWindow_MaxMin = value; NotifyPropertyChanged("MainWindow_MaxMin"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

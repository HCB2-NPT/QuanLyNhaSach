﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.Windows
{
    /// <summary>
    /// Interaction logic for TitleMainRightClick.xaml
    /// </summary>
    public partial class TitleMainRightClick : Window
    {
        #region Properties
        public Window Host { get; set; }
        #endregion

        #region Constructor
        public TitleMainRightClick(Window host)
        {
            InitializeComponent();
			DataContext = Managers.Manager.Current;
			Host = host;
            _Show();
        }
        #endregion
    }
}

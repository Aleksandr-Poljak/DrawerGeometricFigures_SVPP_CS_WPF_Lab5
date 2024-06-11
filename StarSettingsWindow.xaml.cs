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

namespace DrawerGeometricFigures
{
    /// <summary>
    /// The window for changing the shape parameters.
    /// </summary>
    public partial class StarSettingsWindow : Window
    {
        ShapeStar s;
        public StarSettingsWindow( ref ShapeStar star)         
        {
            InitializeComponent();
            //this.Background =((DockPanel) this.Owner.Content).Background;
            s = star;
            this.DataContext = s;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(s.ToString());
        }

    }
}
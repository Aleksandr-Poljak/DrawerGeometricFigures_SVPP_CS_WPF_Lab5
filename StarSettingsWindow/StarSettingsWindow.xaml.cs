using System;
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
using DrawerGeometricFigures.Shapes;

namespace DrawerGeometricFigures
{
    /// <summary>
    /// The window for changing the shape parameters.
    /// </summary>
    public partial class StarSettingsWindow : Window
    {
        private ShapeStar star;
        private ShapeStar backupStar;

        public StarSettingsWindow( ref ShapeStar star)         
        {
            InitializeComponent();
            //this.Background =((DockPanel) this.Owner.Content).Background;
            this.star = star; 
            DataContext = star;

            backupStar = (ShapeStar)star.Clone();
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            star.Background = backupStar.Background;
            star.Foreground = backupStar.Foreground;
            star.Height = backupStar.Height;
            star.Width = backupStar.Width;
            star.Thickness = backupStar.Thickness;
        }
    }
}

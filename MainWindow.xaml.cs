using System.Diagnostics.Metrics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawerGeometricFigures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeStar star = new();
        StarSettingsWindow? starSettingsWindow = null;
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Right-click handler . Displays a star shape on Canvas.
        /// </summary>
        private void MouseLeftBtnDown_Clik(object sender, MouseButtonEventArgs e)
        {           
            
            Polygon starP = star.ToPolygon(e.GetPosition(Canvas_WorkingArea));
            Canvas_WorkingArea.Children.Add(starP);

        }


        private void MenuItem_Shape_Click(object sender, RoutedEventArgs e)
        {
            if (starSettingsWindow is null)
            {
                starSettingsWindow = new StarSettingsWindow(ref star) { Owner=this };
                //Deletes an instance of the window after it is closed.
                starSettingsWindow.Closed += (object? sender, EventArgs e) => starSettingsWindow = null;

                starSettingsWindow.Show();
                
            }
            else { starSettingsWindow.Focus(); }

        }

    }
}
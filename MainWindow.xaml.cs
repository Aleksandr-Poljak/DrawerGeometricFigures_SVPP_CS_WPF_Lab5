using System.Diagnostics.Metrics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
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
            
            Polygon starP = star.GetPolygonFigure(e.GetPosition(Canvas_WorkingArea));
            starP.MouseRightButtonDown += StarP_MouseRightButtonDown;
            Canvas_WorkingArea.Children.Add(starP);

        }

        /// <summary>
        /// Handler for right-clicking on a star. 
        /// Creates the described ellipse on Canvas.
        /// </summary>
        private void StarP_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Polygon st = (Polygon)sender;
            ShapeStar star1 = ShapeStar.PolygonToStar(st);
            
            string s = $"Original:\n{star.ToString()}\nConvert:\n{star1.ToString()}";
            MessageBox.Show(s);


            Point clickPosition = e.GetPosition(Canvas_WorkingArea);
                               
            //MessageBox.Show($"{p.Count} - {p.ToString()}");

            //ShapeEllipse ellipse = new ShapeEllipse(star);
            //Ellipse el = ellipse.GetEllipseFigure();

            //Canvas.SetLeft(el, clickPosition.X - el.Width / 2);
            //Canvas.SetTop(el, clickPosition.Y - el.Height / 3);
            //Canvas_WorkingArea.Children.Add(el);

        }

        private void MenuItem_Shape_Click(object sender, RoutedEventArgs e)
        {
            if (starSettingsWindow is null)
            {
                starSettingsWindow = new StarSettingsWindow(ref star) { Owner=this };

                // Set the background of the main window to the child window.
                starSettingsWindow.Background = ((DockPanel)this.Content).Background;
                //Deletes an instance of the window after it is closed.
                starSettingsWindow.Closed += (object? sender, EventArgs e) => starSettingsWindow = null;

                starSettingsWindow.Show();
                
            }
            else { starSettingsWindow.Focus(); }

        }

    }
}
using System.Diagnostics.Metrics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

        Random rd = new();
        private double canvasWidth;
        private double canvasHeight;

        int countStar = 0;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        /// <summary>
        /// Window loading event handler.
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            canvasWidth = Canvas_WorkingArea.ActualWidth;
            canvasHeight = Canvas_WorkingArea.ActualHeight;
        }

        /// <summary>
        /// Right-click handler . Displays a star shape on Canvas.
        /// </summary>
        private void MouseLeftBtnDown_Clik(object sender, MouseButtonEventArgs e)
        {           
            
            Polygon starP = star.GetPolygonFigure(e.GetPosition(Canvas_WorkingArea));
            starP.MouseRightButtonDown += StarP_MouseRightButtonDown;
            Canvas_WorkingArea.Children.Add(starP);

            countStar++;
            TextBlock_FooterCountStar.Text = $"Star={countStar}";

        }

        /// <summary>
        /// Handler for right-clicking on a star. 
        /// Creates the described ellipse on Canvas.
        /// </summary>
        private void StarP_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Polygon starP = (Polygon)sender; // Get UI element star figure
            ShapeStar starS = ShapeStar.PolygonToStar(starP); // Get star ShapeStar from star polygon
            Point clickPosition = e.GetPosition(Canvas_WorkingArea); // Getposiotion                            

            //Get an ellipse object the same size as the star
            ShapeEllipse ellipse = new ShapeEllipse(starS);
            // Get ellipse UI element 
            Ellipse el = ellipse.GetEllipseFigure();

            // Display an ellipse on top of a star
            Canvas.SetLeft(el, clickPosition.X - el.Width / 2);
            Canvas.SetTop(el, clickPosition.Y - el.Height / 3);
            Canvas_WorkingArea.Children.Add(el);

            animateEllipse(el);

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

        /// <summary>
        /// Creates an animation of an ellipse. The ellipse moves in a random direction.
        /// </summary>
        /// <param name="el"></param>
        private void animateEllipse(Ellipse el)
        {
            // New ellipse posiotion
            double newX = rd.NextDouble() * (canvasWidth - el.ActualWidth);
            double newY = rd.NextDouble() * (canvasHeight - el.ActualHeight);

            //current ellipse posiotion
            double currentX = Canvas.GetLeft(el);
            double currentY = Canvas.GetTop(el);

            // horizontal motion animation
            DoubleAnimation xAnimation = new() 
            { 
                From=currentX,
                To=newX,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            // vertical motion animation
            DoubleAnimation yAnimation = new()
            {
                From = currentY,
                To = newY,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            // Бесприрывная анимация.
            xAnimation.Completed += (s, e) => animateEllipse(el);
            yAnimation.Completed += (s, e) => animateEllipse(el);

            el.BeginAnimation(Canvas.LeftProperty, xAnimation);
            el.BeginAnimation(Canvas.TopProperty, yAnimation); 
        }

        private void MouseMove_Motion(object sender, MouseEventArgs e)
        {
            Point mousePoint = e.GetPosition(Canvas_WorkingArea);
            string text = $"X={mousePoint.X:F1} Y={mousePoint.Y:F1}";
            TextBlock_FooterMausePosition.Text = text;


        }
    }
}
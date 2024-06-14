using DrawerGeometricFigures.Shapes;
using Microsoft.Win32;
using System.CodeDom;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    public class WindowCommands
    {
        static WindowCommands()
        {
            Exit = new RoutedCommand("Exit", typeof(MainWindow));
            Load = new RoutedCommand("Load", typeof(MainWindow),
                new InputGestureCollection() { new KeyGesture(Key.L, ModifierKeys.Control) });
            Clear = new RoutedCommand("Clear", typeof (MainWindow),
                new InputGestureCollection() { new KeyGesture(Key.D, ModifierKeys.Control) });
        }
        public static RoutedCommand Exit { get;set; }
        public static RoutedCommand Load { get; set; }
        public static RoutedCommand Clear { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeStar star = new();

        StarSettingsWindow? starSettingsWindow = null;
        HelpWindow? helpWindow = null;

        Random rd = new();
        private double canvasWidth;
        private double canvasHeight;

        int countStar = 0;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            CommandBnd_Save.CanExecute += HasElement;
            CommandBnd_Clear.CanExecute += HasElement;

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

        /// <summary>
        /// Handler for the help command.
        /// </summary>
        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (helpWindow is null)
            {
                helpWindow = new HelpWindow() { Owner = this };
                helpWindow.Background = ((DockPanel)this.Content).Background;
                helpWindow.Closed += (object? sender, EventArgs e) => helpWindow = null;
                helpWindow.Show();
            }
            else { helpWindow.Focus(); }
        }

        /// <summary>
        /// Handler for the Exit command.
        /// </summary>
        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handler for the Load command.
        /// </summary>
        private void Load_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы (JSON)|*.json";
            var result = ofd.ShowDialog();
            
            if(result == true)
            {
                try
                {
                    string jsonStr = "";
                    using(StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                    
                    ShapeStar? starTemp = JsonSerializer.Deserialize<ShapeStar>(jsonStr);
                    if (starTemp is not null) star = starTemp;
                    else { throw new JsonException(); }                   
                }
                catch { MessageBox.Show("Не удалось прочитать данные", "Error"); }
            }

        }

        /// <summary>
        /// Handler for the Save command.
        /// </summary>
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sfd =  new SaveFileDialog();
            sfd.Filter = "Файлы (JSON)|*.json";
            var result = sfd.ShowDialog();
            
            if(result == true)
            {               
                try
                {
                    string jsonStr = JsonSerializer.Serialize(star);
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.Write(jsonStr);
                    }                    
                }
                catch (Exception ex) 
                { MessageBox.Show("Ошибка при сохранении файла", "Error" );}                             
            }
        }

        /// <summary>
        /// The handler for the Save command.
        /// Checks the condition that the command can be executed.
        /// </summary>
        private void HasElement(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Canvas_WorkingArea.Children.Count > 0;
        }

        /// <summary>
        /// Handler for the Clear command.
        /// </summary>
        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Canvas_WorkingArea.Children.Clear();
            countStar = 0;
            TextBlock_FooterCountStar.Text = $"Star={countStar}";
        }
    }
}
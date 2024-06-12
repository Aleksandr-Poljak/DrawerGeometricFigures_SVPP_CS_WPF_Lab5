using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawerGeometricFigures
{
    public class ShapeStar : INotifyPropertyChanged, ICloneable
    {
        public static int MinWidth { get; } = 10;
        public static int MaxWidth { get; } = 300;
        public static int MinHeight { get; } = 10;
        public static int MaxHeight { get; } = 300;
        public static int MinTickness { get; } = 1;
        public static int MaxTickness { get; } = 10;

        int width;
        int height;
        int tickness;
        Color foreground;
        Color background;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Width
        {
            get => width;
            set
            {
                if (value < MinWidth || value > MaxWidth) 
                    throw new ArgumentException($"The argument must be between {MinWidth} and {MaxWidth}");
                width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public int Height 
        {   get => height;
            set
            {
                if (value < MinHeight || value > MaxHeight) 
                    throw new ArgumentException($"The argument must be between {MinHeight} and {MaxHeight}");
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public int Tickness 
        { 
            get => tickness;
            set
            {
                if (value < MinTickness || value > MaxTickness)
                    throw new ArgumentException($"The argument must be between {MinTickness} and {MaxTickness}");
                tickness = value;
                OnPropertyChanged(nameof(Tickness));
            }
        }
        public Color Foreground 
        {   get => foreground;
            set 
            {
                foreground = value;
                OnPropertyChanged(nameof(Foreground));
            } 
        }
        public Color Background 
        { 
            get => background;
            set
            { 
                background = value;
                OnPropertyChanged(nameof(Background));        
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShapeStar()
        {
            Height = 50;
            Width = 50;
            Tickness = 2;
            Background = Colors.Black;
            Foreground = Colors.DarkRed;
        }

        public ShapeStar(int width, int height, int tickness, Color foreground, Color background):
            this()
        {
            Width = width;
            Height = height;
            Tickness = tickness;
            Foreground = foreground;
            Background = background;
        }

        public override string ToString()
        {
            return $"<Star: Width- {Width}, Height- {Height}, Tickness- {Tickness}," +
                $" Background- {Background.ToString()}, Foreground {Foreground.ToString()}>";
        }

        public void OnPropertyChanged([CallerMemberName] string prop="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Returns a Star Polygon object.
        /// </summary>
        /// <param name="top">Vertex coordinates</param>
        /// <returns>Polygon objects</returns>
        public Polygon ToPolygon(Point top)
        {
            PointCollection points = new PointCollection();

            // The radius of the incircle and circumcircle
            double outerRadiusX = Width / 2;
            double outerRadiusY = Height / 2;
            double innerRadiusX = outerRadiusX * 0.5;
            double innerRadiusY = outerRadiusY * 0.5;

            // Calculate the offset to move the star so that the top point is at (top.X, top.Y)
            double cx = top.X;
            double cy = top.Y + outerRadiusY * Math.Sin(18 * Math.PI / 180);

            for (int i = 0; i < 10; i++)
            {
                // Rotate by -90 degrees to align the top vertex
                double angle = (i * 36 - 90) * Math.PI / 180;
                double radiusX = (i % 2 == 0) ? outerRadiusX : innerRadiusX;
                double radiusY = (i % 2 == 0) ? outerRadiusY : innerRadiusY;

                double px = cx + radiusX * Math.Cos(angle);
                double py = cy + radiusY * Math.Sin(angle);

                points.Add(new Point(px, py));
            }

            Polygon star = new();
            star.Fill = new SolidColorBrush(background);
            star.Stroke = new SolidColorBrush(foreground);
            star.StrokeThickness = Tickness;
            star.Points = points;

            return star;
        }

        /// <summary>
        /// Returns the star shape as a JSON string
        /// </summary>
        /// <returns>Figure in JSON.</returns>
        public string ToJSON()
        {
            return "";
        }

        public object Clone()
        {
            return new ShapeStar(Width, Height, Tickness, Foreground, Background);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawerGeometricFigures.Shapes
{
    public class ShapeEllipse
    {
        static private List<Color> allCollors = new List<Color>();
        static private Random rd = new Random();

        int width;
        int height;
        int thickness;
        Color foreground;
        LinearGradientBrush background;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Thickness { get => thickness; set => thickness = value; }
        public Color Foreground { get => foreground; set => foreground = value; }
        public LinearGradientBrush Background { get => background; set => background = value; }

        public ShapeEllipse()
        {
            Width = 50;
            Height = 50;
            Thickness = 5;
            Foreground = allCollors[rd.Next(allCollors.Count)];
            Background = _getRandomLenearGradientBrush();
        }

        public ShapeEllipse(int width, int height, int tickness,
            Color? foreground = null, LinearGradientBrush? background = null) : this()
        {
            Width = width;
            Height = height;
            Thickness = tickness;
            if (foreground is not null) { Foreground = (Color)foreground; }
            if (background is not null) { Background = background; }
        }

        /// <summary>
        /// Constructs an ellipse based on the star shape.
        /// The ellipse will be described around the star shape.
        /// </summary>
        /// <param name="star"></param>
        public ShapeEllipse(ShapeStar star,
            LinearGradientBrush? background = null, Color? foreground = null) : this()
        {
            Width = star.Width;
            Height = star.Height;
            Thickness = star.Thickness;
            if (foreground is not null) { Foreground = (Color)foreground; }
            if (background is not null) { Background = background; }
        }

        /// <summary>
        /// Sets a random gradient color for the fill of the ellipse.
        /// </summary>
        /// <param name="countColors">Number of colors</param>
        /// <returns></returns>
        private LinearGradientBrush _getRandomLenearGradientBrush(int countColors = 3)
        {
            GradientStopCollection gradientStops = new GradientStopCollection();

            for (int i = 0; i < countColors; i++)
            {
                // Random color     
                gradientStops.Add(new GradientStop(allCollors[rd.Next(allCollors.Count)],
                    (double)i / (countColors - 1)));
            }

            return new LinearGradientBrush(gradientStops, 0);
        }

        /// <summary>
        /// Returns a graphical representation of the object.
        /// </summary>
        public Ellipse GetEllipseFigure()
        {
            Ellipse el = new Ellipse();
            el.Width = Width;
            el.Height = Height;
            el.StrokeThickness = Thickness;
            el.Stroke = new SolidColorBrush(foreground);
            el.Fill = Background;
            el.Opacity = 0.7 + (1.0 - 0.7) * rd.NextDouble();
            return el;
        }

        public override string ToString()
        {
            return $"<Ellipse: Width- {Width}, Height- {Height}, Tickness- {Thickness}," +
                $" Background- {Background.ToString()}, Foreground {Foreground.ToString()}>";
        }

        /// <summary>
        /// The static constructor populates a collection of colors 
        /// for the last random color selection.
        /// </summary>
        static ShapeEllipse()
        {
            PropertyInfo[] colorProperties = typeof(Colors).GetProperties(
                BindingFlags.Static | BindingFlags.Public);

            foreach (PropertyInfo prop in colorProperties)
            {
                if (prop.PropertyType == typeof(Color))
                    allCollors.Add((Color)prop.GetValue(null));

            }
            allCollors.TrimExcess();
        }

    }
}

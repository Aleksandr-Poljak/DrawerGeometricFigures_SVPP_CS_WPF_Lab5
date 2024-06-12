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
    public class ShapeEllipse
    {
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
            Foreground = Colors.Red;
            Background = _getRandomLenearGradientBrush();
        }
   
        public ShapeEllipse(int width, int height, int tickness,
            Color? foreground = null, LinearGradientBrush? background = null) :this()
        {
            Width = width;
            Height = height;
            Thickness = tickness;
            if (foreground is not null) { Foreground = (Color) foreground; }
            if (background is not null) { Background = (LinearGradientBrush)background; }
        }

        /// <summary>
        /// Constructs an ellipse based on the star shape.
        /// The ellipse will be described around the star shape.
        /// </summary>
        /// <param name="star"></param>
        public ShapeEllipse(ShapeStar star,
            LinearGradientBrush? background = null, Color? foreground = null) :this()
        {
            Width = star.Width;
            Height = star.Height;
            Thickness = star.Thickness;
            if (foreground is not null) { Foreground = (Color)foreground; }
            if (background is not null) { Background = (LinearGradientBrush)background; }
        }

        /// <summary>
        /// Sets a random gradient color for the fill of the ellipse.
        /// </summary>
        /// <param name="countColors">Number of colors</param>
        /// <returns></returns>
        private LinearGradientBrush _getRandomLenearGradientBrush(int countColors = 3)
        {
            Random rd = new Random();
            GradientStopCollection gradientStops = new GradientStopCollection();

            for (int i = 0; i < countColors; i++)
            {
                // Random color name
                int numberColor = rd.Next(Enum.GetNames(typeof(Colors)).Length);
                string randomColorName = Enum.GetNames(typeof(Colors))[numberColor];

                Color randomColor = (Color)ColorConverter.ConvertFromString(randomColorName);

                gradientStops.Add(new GradientStop(randomColor, (double)i / (countColors - 1)));
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
            return el;
        }

        public override string ToString()
        {
            return $"<Ellipse: Width- {Width}, Height- {Height}, Tickness- {Thickness}," +
                $" Background- {Background.ToString()}, Foreground {Foreground.ToString()}>";
        }


    }
}

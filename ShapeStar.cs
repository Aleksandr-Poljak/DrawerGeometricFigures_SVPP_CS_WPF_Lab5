using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawerGeometricFigures
{
    public class ShapeStar
    {
        int width;
        int height;
        int tickness;
        Color foreground;
        Color background;
       
        public int Width
        {
            get => width;
            set
            {
                if (value < 10 || value > 200) 
                    throw new ArgumentException("The argument must be between 10 and 200");
                width = value;
            }
        }
        public int Height 
        {   get => height;
            set
            {
                if (value < 10 || value > 200) 
                    throw new ArgumentException("The argument must be between 10 and 200");
                height = value;
            }
        }
        public int Tickness 
        { 
            get => tickness;
            set
            {
                if (value < 1 || value > 20)
                    throw new ArgumentException("The argument must be between 1 and 20");
                tickness = value;
            }
        }
        public Color Foreground { get => foreground; set => foreground = value; }
        public Color Background { get => background; set => background = value; }

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

        /// <summary>
        /// Returns a Star Polygon object.
        /// </summary>
        /// <param name="top">Vertex coordinates</param>
        /// <returns>Polygon objects</returns>
        public Polygon GetPolygonStar(Point top)
        {
            PointCollection points = new PointCollection();

            // The radius of the incircle and circumcircle
            double outerRadius = height / 2;
            double innerRadius = outerRadius * 0.5;

            // Calculate the offset to move the star so that the top point is at (top.X, top.Y)
            double cx = top.X;
            double cy = top.Y + outerRadius * Math.Sin(18 * Math.PI / 180);

            for (int i = 0; i < 10; i++)
            {
                // Rotate by -90 degrees to align the top vertex
                double angle = (i * 36 - 90) * Math.PI / 180; 
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;

                double px = cx + radius * Math.Cos(angle);
                double py = cy + radius * Math.Sin(angle);

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
        /// Displays a star on Canvas.
        /// </summary>
        /// <param name="canvas">Canvas object</param>
        /// <param name="top">Vertex coordinates</param>
        public void Draw( ref Canvas canvas, Point top)
        {
             Polygon star = GetPolygonStar(top);
             canvas.Children.Add(star);           
        }



    }
}

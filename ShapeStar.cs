using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
            Height = 20;
            Width = 20;
            Tickness = 5;
            Background = Color.Black;
            Foreground = Color.White;
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

        public void Draw( ref Canvas canvas, Point point)
        {

        }



    }
}

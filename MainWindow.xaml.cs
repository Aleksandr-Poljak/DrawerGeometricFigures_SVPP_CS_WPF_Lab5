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
        public MainWindow()
        {
            InitializeComponent();

        }

        private void MouseLeftBtnDown_Clik(object sender, MouseButtonEventArgs e)
        {
            
            star.Draw(ref Canvas_WorkingArea, e.GetPosition(Canvas_WorkingArea));
        }
    }
}
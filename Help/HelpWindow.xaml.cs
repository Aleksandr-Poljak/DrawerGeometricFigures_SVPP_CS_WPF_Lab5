using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace DrawerGeometricFigures
{
    /// <summary>
    /// Логика взаимодействия для HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            initHelpText();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string readHelpText()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Help", "help.txt");
            string helpTxt = "";

            using (StreamReader tr = new StreamReader(filePath))
            {
                helpTxt = tr.ReadToEnd();
            }
            return helpTxt;
        }

        private void initHelpText()
        {
            string txt = readHelpText();
            if(txt != "") TextBlock_Text.Text = txt;    
        }
    }
}

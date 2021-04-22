using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dpIzborDatuma.SelectedDate = DateTime.Today;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnIzborPrognozirana_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odabir XML prognozirane potrošnje";
            op.Filter = "XML | *.xml";
            if (op.ShowDialog().Value)
                lblPrognozirana.Content = op.SafeFileName;
        }

        private void btnIzborOstvarena_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odabir XML ostvarene potrošnje";
            op.Filter = "XML | *.xml";
            if (op.ShowDialog().Value)
                lblOstvarena.Content = op.SafeFileName;
        }
    }
}

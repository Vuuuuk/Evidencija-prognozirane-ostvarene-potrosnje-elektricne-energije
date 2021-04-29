using GUI.DemoImplementacija;
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
        Deserijalizator deserijalizator = new Deserijalizator();
        Validator validator = new Validator();
        private string ostvarenaFlag = "Empty";
        private string prognoziranaFlag = "Empty";

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
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
        
        private void btnIzborOstvarena_Click(object sender, RoutedEventArgs e)
        {
            ostvarenaFlag = deserijalizator.IzborXMLOstvarena();
            if (ostvarenaFlag.Contains("Error"))
                lblOstvarena.Content = ostvarenaFlag.Split('_')[1];
            if (ostvarenaFlag.Equals("Empty"))
                lblOstvarena.Content = "Odaberite XML ostvarene potrošnje";
            else
                lblOstvarena.Content = ostvarenaFlag;
        }

        private void btnIzborPrognozirana_Click(object sender, RoutedEventArgs e)
        {
            prognoziranaFlag = deserijalizator.IzborXMLPrognozirana();
            if (prognoziranaFlag.Contains("Error"))
                lblPrognozirana.Content = prognoziranaFlag.Split('_')[1];
            if (prognoziranaFlag.Equals("Empty"))
                lblPrognozirana.Content = "Odaberite XML prognozirane potrošnje";
            else
                lblPrognozirana.Content = prognoziranaFlag;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(ostvarenaFlag.Contains("Error") || prognoziranaFlag.Contains("Error"))
            {
                MessageBox.Show("Izabrali ste pogrešan TIP fajla, potrebno je odabrati .XML!", "Greška", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(ostvarenaFlag.Equals("Empty") || prognoziranaFlag.Equals("Empty"))
            {
                MessageBox.Show("Niste odabrali potrebne fajlove!", "Greška", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                deserijalizator.LoadXMLOstvarena();
                deserijalizator.LoadXMLPrognozirana();
                deserijalizator.ParsiranjeXMLOstvarena();
                deserijalizator.ParsiranjeXMLPrognozirana();

                //Test
                Console.WriteLine(validator.ValidacijaPodatakaOstvarena(deserijalizator.Op));
                Console.WriteLine(validator.ValidacijaPodatakaPrognozirana(deserijalizator.Pp));
            }
        }
    }
}

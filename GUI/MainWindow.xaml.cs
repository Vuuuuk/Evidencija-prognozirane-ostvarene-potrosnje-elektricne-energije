using Microsoft.Win32;
using Servis;
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

        ValidatorTipaFajla validatorTipa = new ValidatorTipaFajla();
        ValidatorPodataka validatorPodataka = new ValidatorPodataka();
        Deserijalizacija deserijalizator = new Deserijalizacija();
        OpenFileDialog ostvarena;
        OpenFileDialog prognozirana;

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
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
            ostvarena = OpenFile();
            if (ostvarena == null)
                lblOstvarena.Content = "Odaberite XML ostvarene potrošnje";
            else
            {
                if (!validatorTipa.ValidatorTipa(ostvarena.SafeFileName))
                    lblOstvarena.Content = "Pogrešan tip fajla!";
                else
                    lblOstvarena.Content = ostvarena.SafeFileName;
            }
        }

        private void btnIzborPrognozirana_Click(object sender, RoutedEventArgs e)
        {
            prognozirana = OpenFile();
            if (prognozirana == null)
                lblPrognozirana.Content = "Odaberite XML prognozirane potrošnje";
            else
            {
                if (!validatorTipa.ValidatorTipa(prognozirana.SafeFileName))
                    lblPrognozirana.Content = "Pogrešan tip fajla!";
                else
                    lblPrognozirana.Content = prognozirana.SafeFileName;
            }
        }

        private OpenFileDialog OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Fajlovi (*.xml) | *.xml| Svi Fajlovi (*.*) | *.*";
            ofd.FilterIndex = 0;
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog().Value)
            {
                return ofd;
            }
            return null;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (ostvarena != null && prognozirana != null)
            {
                deserijalizator.LoadXMLOstvarena(ostvarena);
                deserijalizator.LoadXMLPrognozirana(prognozirana);
                deserijalizator.ParsiranjeXMLOstvarena();
                deserijalizator.ParsiranjeXMLPrognozirana();
                
                if (validatorPodataka.Validator(deserijalizator.OstvarenaPotrosnja) && validatorPodataka.Validator(deserijalizator.PrognoziranaPotrosnja))
                {
                    // TODO : GOOD FILES
                    Console.WriteLine(deserijalizator.ParseDatum(ostvarena.SafeFileName).ToShortDateString());
                }
            }
            else
            {
                MessageBox.Show("Unesite potrebne fajlove!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            /*Console.WriteLine("OSTVARENA");
            foreach (Potrosnja p in deserijalizator.OstvarenaPotrosnja)
                Console.WriteLine(p);

            Console.WriteLine("PROGNOZIRANA");
            foreach (Potrosnja p in deserijalizator.PrognoziranaPotrosnja)
                Console.WriteLine(p);
            */
        }
    }
}

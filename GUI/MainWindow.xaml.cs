using BazaPodataka;
using Common.Models;
using Microsoft.Win32;
using Servis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

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

        Baza baza = new Baza();
        Connection connection = new Connection();

        Ekstraktor ekstraktor = new Ekstraktor();

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
            connection.ZatvoriKonekciju();
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
            bool valid = true;
            bool ostv = false, prog = false; // ne postoje u bazi
            string message = "";

            if (ostvarena != null && prognozirana != null && connection.ProveriKonekciju())
            {
                if(baza.FajlUcitan(ostvarena.SafeFileName))
                {
                    message += "[INFO] Fajl \"" + ostvarena.SafeFileName + "\" već postoji u bazi podataka!" + Environment.NewLine;
                    valid = false;
                    ostv = true;
                }
                if (baza.FajlUcitan(prognozirana.SafeFileName))
                {
                    message += "[INFO] Fajl \"" + prognozirana.SafeFileName + "\" već postoji u bazi podataka!" + Environment.NewLine;
                    valid = false;
                    prog = true;
                }

                deserijalizator.LoadXMLOstvarena(ostvarena);
                deserijalizator.LoadXMLPrognozirana(prognozirana);
                deserijalizator.ParsiranjeXMLOstvarena();
                deserijalizator.ParsiranjeXMLPrognozirana();

                // Validato podataka OSTVARENE potrosnje i upis u bazu
                if (validatorPodataka.Validator(deserijalizator.OstvarenaPotrosnja))
                {
                    if(!ostv)
                    {
                        foreach (Potrosnja p in deserijalizator.OstvarenaPotrosnja)
                            baza.UpisPotrosnje(DateTime.Now, ostvarena, p, deserijalizator.ParseDatum(ostvarena.SafeFileName), "EvidencijaOstvarenePotrosnje");
                        message += "[INFO] Fajl \"" + ostvarena.SafeFileName + "\" uspešno upisan u bazu!" + Environment.NewLine;
                    }
                }
                else
                {
                    baza.UpisNevalidnogFajla(DateTime.Now, ostvarena, deserijalizator.BrojRedova(ostvarena));
                    message += "[ERROR] Fajl \"" + ostvarena.SafeFileName + "\" nema validne podatke! [EVIDENTIRANO]" + Environment.NewLine;
                    valid = false;
                }

                // Validato podataka PROGNOZIRANE potrosnje i upis u bazu
                if (validatorPodataka.Validator(deserijalizator.PrognoziranaPotrosnja))
                {
                    if (!prog)
                    {
                        foreach (Potrosnja p in deserijalizator.PrognoziranaPotrosnja)
                            baza.UpisPotrosnje(DateTime.Now, prognozirana, p, deserijalizator.ParseDatum(prognozirana.SafeFileName), "EvidencijaPrognoziranePotrosnje");
                        message += "[INFO] Fajl \"" + prognozirana.SafeFileName + "\" uspešno upisan u bazu!" + Environment.NewLine;
                    }
                }
                else
                {
                    baza.UpisNevalidnogFajla(DateTime.Now, prognozirana, deserijalizator.BrojRedova(prognozirana));
                    message += "[ERROR] Fajl \"" + prognozirana.SafeFileName + "\" nema validne podatke! [EVIDENTIRANO]" + Environment.NewLine;
                    valid = false;
                }

                cbOdabirGeoOblasti.ItemsSource = baza.GeoLokacije();

            }
            else
            {
                message += "Unesite potrebne fajlove i/ili proverite konekciju sa bazom!" + Environment.NewLine;
                valid = false;
            }

            if (valid)
                MessageBox.Show(message, "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(message, "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnPrikazi_Click(object sender, RoutedEventArgs e)
        {
            if(cbOdabirGeoOblasti.SelectedItem != null && dpIzborDatuma.SelectedDate.Value != null)
            {
                List<RelativnoOdstupanje> lista = baza.ProracunOdstupanja(cbOdabirGeoOblasti.SelectedItem.ToString().Trim(), dpIzborDatuma.SelectedDate.Value.ToShortDateString());
                dgPrikazPodataka.ItemsSource = lista;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            cbOdabirGeoOblasti.ItemsSource = null;
            dpIzborDatuma.SelectedDate = null;
            dgPrikazPodataka.ItemsSource = null;
            dgGeografskaPodrucja.ItemsSource = null;
            Logovanje logovanje = new Logovanje();
            logovanje.ShowDialog();
            if (connection.ProveriKonekciju())
                cbOdabirGeoOblasti.ItemsSource = baza.GeoLokacije();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (connection.ProveriKonekciju())
            {
                baza.IsprazniBazu();
                cbOdabirGeoOblasti.ItemsSource = null;
                dpIzborDatuma.SelectedDate = null;
                dgPrikazPodataka.ItemsSource = null;
                MessageBox.Show("Baza uspešno obrisana!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Proverite konekciju sa bazom!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (!dgPrikazPodataka.Items.Count.Equals(0))
            {
                System.Windows.Forms.DialogResult dialogResult =
                (System.Windows.Forms.DialogResult)MessageBox.Show("Da li ste sigurni da zelite da sačuvate podatke?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(dialogResult.Equals(System.Windows.Forms.DialogResult.Yes))
                {
                    string podaci = ekstraktor.CuvanjePodatakaCSV(dgPrikazPodataka);
                    if(!podaci.Equals(string.Empty))
                    {
                        string ime = podaci.Split('_')[0];
                        string lokacija = podaci.Split('_')[1];
                        MessageBox.Show(ime + " uspešno kreiran na lokaciji " + lokacija + " !", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
                MessageBox.Show("Podaci za ekstrakciju nisu dostupni!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

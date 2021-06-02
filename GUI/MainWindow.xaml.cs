using Common.Interface;
using Common.Models;
using Microsoft.Win32;
using Servis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        DPO.Deserijalizator deserijalizator = new DPO.Deserijalizator();
        DAO.PristupPodacima pristup = new DAO.PristupPodacima();
        Evidentiranje evidentiranje = new Evidentiranje(DAO.PristupPodacima.InitBaza()); //init baze za constructor injection
        Proracun proracun = new Proracun(DAO.PristupPodacima.InitBaza());

        OpenFileDialog ostvarena;
        OpenFileDialog prognozirana;

        DPO.Ekstraktor ekstraktor = new DPO.Ekstraktor();

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
            pristup.ZatvaranjeKonekcije();
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

            if (ostvarena != null && prognozirana != null && pristup.ProveraKonekcije())
            {
                if(pristup.FajlUcitan(ostvarena.SafeFileName))
                {
                    message += "[INFO] Fajl \"" + ostvarena.SafeFileName + "\" već postoji u bazi podataka!" + Environment.NewLine;
                    valid = false;
                    ostv = true;
                }
                if (pristup.FajlUcitan(prognozirana.SafeFileName))
                {
                    message += "[INFO] Fajl \"" + prognozirana.SafeFileName + "\" već postoji u bazi podataka!" + Environment.NewLine;
                    valid = false;
                    prog = true;
                }

                //OSTVARENA POTROSNJA LOAD & PARSE
                using (StreamReader sr = new StreamReader(ostvarena.OpenFile()))
                {
                    string text = sr.ReadToEnd();
                    byte[] niz = System.Text.Encoding.UTF8.GetBytes(text);
                    using (MemoryStream ms = new MemoryStream(niz))
                    {
                        deserijalizator.LoadXMLOstvarena(ms);
                        deserijalizator.ParsiranjeXMLOstvarena();
                    }
                }

                //PROGNOZIRANA POTROSNJA LOAD & PARSE
                using (StreamReader sr = new StreamReader(prognozirana.OpenFile()))
                {
                    string text = sr.ReadToEnd();
                    byte[] niz = System.Text.Encoding.UTF8.GetBytes(text);
                    using (MemoryStream ms = new MemoryStream(niz))
                    {
                        deserijalizator.LoadXMLPrognozirana(ms);
                        deserijalizator.ParsiranjeXMLPrognozirana();
                    }
                }

                // Validator podataka OSTVARENE potrosnje i upis u bazu
                if (validatorPodataka.Validator(deserijalizator.ParseDatum(ostvarena.SafeFileName), deserijalizator.OstvarenaPotrosnja))
                {
                    if(!ostv)
                    {
                        evidentiranje.EvidentirajOblasti(deserijalizator.OstvarenaPotrosnja);
                        foreach (Potrosnja p in deserijalizator.OstvarenaPotrosnja)
                            pristup.UpisPotrosnje(DateTime.Now, ostvarena.SafeFileName, ostvarena.FileName.ToString(), p, deserijalizator.ParseDatum(ostvarena.SafeFileName), "EvidencijaOstvarenePotrosnje");
                        pristup.IzvrsiUpisSvihPodataka();
                        message += "[INFO] Fajl \"" + ostvarena.SafeFileName + "\" uspešno upisan u bazu!" + Environment.NewLine;
                    }
                }
                else
                {

                    pristup.UpisNevalidnogFajla(DateTime.Now, ostvarena.SafeFileName, ostvarena.FileName.ToString(), System.IO.File.ReadAllLines(ostvarena.FileName).Length);
                    message += "[ERROR] Fajl \"" + ostvarena.SafeFileName + "\" nema validne podatke! [EVIDENTIRANO]" + Environment.NewLine;
                    valid = false;
                }

                // Validato podataka PROGNOZIRANE potrosnje i upis u bazu
                if (validatorPodataka.Validator(deserijalizator.ParseDatum(prognozirana.SafeFileName), deserijalizator.PrognoziranaPotrosnja))
                {
                    if (!prog)
                    {
                        evidentiranje.EvidentirajOblasti(deserijalizator.PrognoziranaPotrosnja);
                        foreach (Potrosnja p in deserijalizator.PrognoziranaPotrosnja)
                            pristup.UpisPotrosnje(DateTime.Now, prognozirana.SafeFileName, prognozirana.FileName.ToString(), p, deserijalizator.ParseDatum(prognozirana.SafeFileName), "EvidencijaPrognoziranePotrosnje");
                        pristup.IzvrsiUpisSvihPodataka();
                        message += "[INFO] Fajl \"" + prognozirana.SafeFileName + "\" uspešno upisan u bazu!" + Environment.NewLine;
                    }
                }
                else
                {
                    pristup.UpisNevalidnogFajla(DateTime.Now, prognozirana.SafeFileName, prognozirana.FileName.ToString(), System.IO.File.ReadAllLines(prognozirana.FileName).Length);
                    message += "[ERROR] Fajl \"" + prognozirana.SafeFileName + "\" nema validne podatke! [EVIDENTIRANO]" + Environment.NewLine;
                    valid = false;
                }

                cbOdabirGeoOblasti.ItemsSource = pristup.GeoLokacije();

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

            if(pristup.ProveraKonekcije())
                dgGeografskaPodrucja.ItemsSource = pristup.GeoLokacije();
        }

        private void btnPrikazi_Click(object sender, RoutedEventArgs e)
        {
            if(cbOdabirGeoOblasti.SelectedItem != null && dpIzborDatuma.SelectedDate.Value != null)
            {
                List<IPotrosnja> ostvarenaLista = 
                    proracun.PopuniListuPotrosnje("EvidencijaOstvarenePotrosnje", 
                    cbOdabirGeoOblasti.SelectedItem.ToString().Trim(), 
                    dpIzborDatuma.SelectedDate.Value.ToShortDateString());
                List<IPotrosnja> prognoziranaLista =
                    proracun.PopuniListuPotrosnje("EvidencijaPrognoziranePotrosnje", 
                    cbOdabirGeoOblasti.SelectedItem.ToString().Trim(), 
                    dpIzborDatuma.SelectedDate.Value.ToShortDateString());

                dgPrikazPodataka.ItemsSource = proracun.IzracunajOdstupanje(ostvarenaLista, prognoziranaLista);
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
            if (pristup.ProveraKonekcije())
            {
                cbOdabirGeoOblasti.ItemsSource = pristup.GeoLokacije();
                dgGeografskaPodrucja.ItemsSource = pristup.GeoLokacije();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (pristup.ProveraKonekcije())
            {
                pristup.IsprazniBazu();
                cbOdabirGeoOblasti.ItemsSource = null;
                dpIzborDatuma.SelectedDate = null;
                dgPrikazPodataka.ItemsSource = null;
                dgGeografskaPodrucja.ItemsSource = pristup.GeoLokacije();
                MessageBox.Show("Baza uspešno obrisana!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Proverite konekciju sa bazom!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

            List<IRelativnoOdstupanje> list = new List<IRelativnoOdstupanje>();

            if (!dgPrikazPodataka.Items.Count.Equals(0))
            {
                System.Windows.Forms.DialogResult dialogResult =
                (System.Windows.Forms.DialogResult)MessageBox.Show("Da li ste sigurni da zelite da sačuvate podatke?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(dialogResult.Equals(System.Windows.Forms.DialogResult.Yes))
                {

                    foreach (var item in dgPrikazPodataka.Items.OfType<RelativnoOdstupanje>())
                    {
                        var sat = item.sat;
                        var ostvarena = item.ostvarenaPotrosnja;
                        var prognozirana = item.prognoziranaPotrosnja;
                        var odstupanje = item.odstupanje;
                        list.Add(new RelativnoOdstupanje(sat, ostvarena, prognozirana, odstupanje));
                    }

                    string podaci = ekstraktor.CuvanjePodatakaCSV(list);
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

using BazaPodataka;
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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Logovanje.xaml
    /// </summary>
    public partial class Logovanje : Window
    {
        Connection connection = new Connection();
        public Logovanje()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            rbLocal.IsChecked = false;
            rbRemote.IsChecked = false;
            tbPassword.IsEnabled = false;
            tbUsername.IsEnabled = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(rbRemote.IsChecked == true)
            {
                if (!connection.OtvoriRemoteKonekciju(tbUsername.Text.Trim(), tbPassword.Password))
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBox.Show("Konekcija uspešno otvorena!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else
            {
                if (!connection.OtvoriLocalKonekciju())
                    MessageBox.Show("Neuspela konekcija sa bazom, molimo pokušajte ponovo!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBox.Show("Konekcija uspešno otvorena!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
        }

        private void rbLocal_Checked(object sender, RoutedEventArgs e)
        {
            tbUsername.IsEnabled = false;
            tbPassword.IsEnabled = false;
        }

        private void rbRemote_Checked(object sender, RoutedEventArgs e)
        {
            tbUsername.IsEnabled = true;
            tbPassword.IsEnabled = true;
        }
    }
}

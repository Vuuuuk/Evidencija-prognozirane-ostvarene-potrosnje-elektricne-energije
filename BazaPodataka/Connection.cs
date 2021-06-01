using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaPodataka
{
    public class Connection
    {
        //User ID=Vuk Radunovic;Password=vuki! -> Account1
        //User ID=Milan Stevanovic;Password=resprojekat123 -> Account2
        //private static string baseConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Evidencija Potrosnje;Integrated Security=True"; -> LocalDB
        //private static string baseConnectionString = @"Data Source=79.175.67.179;Initial Catalog=Evidencija Potrosnje;User ID=Vuk Radunovic;Password=vuki!"; -> RemoteDB
        //private string ip = "188.2.117.185"; // Milan Remote
        private string ip = "77.105.61.149"; // Vuk Remote fallback
        private static SqlConnection sqlConnection = new SqlConnection();
        public SqlConnection SqlConnection { get => sqlConnection; set => sqlConnection = value; }

        public bool OtvoriRemoteKonekciju(string username, string password)
        {
            ZatvoriKonekciju();
            bool izlaz = true;
            string baseConnectionString = @"Data Source=" + ip + ";Initial Catalog=Evidencija Potrosnje;";
            if (username.Equals(string.Empty) || password.Equals(string.Empty))
                izlaz = false;

            else
            {
                string connectionString = baseConnectionString + "User ID=" + username + ";Password=" + password;
                try
                {
                    SqlConnection.ConnectionString = connectionString;
                    SqlConnection.Open();
                }
                catch (System.Data.SqlClient.SqlException ex) //dodati nas exception i raditi sa njim
                {
                    Console.WriteLine(ex.Message);
                    izlaz = false;
                }
            }
            return izlaz;
        }

        public bool OtvoriLocalKonekciju() //Samo kod Mikija
        {
            ZatvoriKonekciju();
            bool izlaz = true;
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Evidencija Potrosnje;Integrated Security=True";
            try
            {
                SqlConnection.ConnectionString = connectionString;
                SqlConnection.Open();
            }
            catch (System.Data.SqlClient.SqlException ex) //dodati nas exception i raditi sa njim
            {
                Console.WriteLine(ex.Message);
                izlaz = false;
            }
            return izlaz;
        }

        public void ZatvoriKonekciju()
        {
            if (!SqlConnection.Equals(null) && !SqlConnection.State.Equals(System.Data.ConnectionState.Closed))
                SqlConnection.Close();
        }

        public bool ProveriKonekciju()
        {
            bool izlaz = true;
            if (SqlConnection.State.Equals(System.Data.ConnectionState.Closed))
                izlaz = false;
            return izlaz;
        }

    }
}

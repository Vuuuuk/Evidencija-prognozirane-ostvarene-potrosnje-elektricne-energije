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
        //User ID=Vuk Radunovic;Password=vuki!
        //User ID=Milan Stevanovic;Password=resprojekat123
        private static string baseConnectionString = @"Data Source=79.175.67.179;Initial Catalog=Evidencija Potrosnje;User ID=Vuk Radunovic;Password=vuki!";
        //private static string baseConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Evidencija Potrosnje;Integrated Security=True";
        private SqlConnection sqlConnection = new SqlConnection(baseConnectionString);

        public Connection()
        {
            sqlConnection.Open();
        }

        public SqlConnection SqlConnection { get => sqlConnection; set => sqlConnection = value; }

        public void promenaKorisnika(string username)
        {
            if (username.Equals("Vuk") || username.Equals(string.Empty))
                baseConnectionString = @"Data Source=79.175.67.179;Initial Catalog=Evidencija Potrosnje;User ID=Vuk Radunovic;Password=vuki!";
            if (username.Equals("Milan"))
                baseConnectionString = @"Data Source=79.175.67.179;Initial Catalog=Evidencija Potrosnje;User ID=Milan Stevanovic;Password=resprojekat123";

            SqlConnection.Close();
            SqlConnection.ConnectionString = baseConnectionString;
            SqlConnection.Open();
        }

    }
}

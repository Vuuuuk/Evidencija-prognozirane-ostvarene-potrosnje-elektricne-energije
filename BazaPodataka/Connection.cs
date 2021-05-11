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
        //User ID=Vuk Radunović;Password=vuki!
        //User ID=Milan Stevanović;Password=resprojekat123
        private static string baseConnectionString = @"Data Source=79.175.71.245;Initial Catalog=Evidencija Potrosnje;User ID=Vuk Radunović;Password=vuki!";
        private SqlConnection sqlConnection = new SqlConnection(baseConnectionString);

        public Connection()
        {
            sqlConnection.Open();
        }

        public SqlConnection SqlConnection { get => sqlConnection; set => sqlConnection = value; }

        public void promenaKorisnika(string username)
        {
            if (username.Equals("Vuk") || username.Equals(string.Empty))
                baseConnectionString = @"Data Source=79.175.71.245;Initial Catalog=Evidencija Potrosnje;User ID=Vuk Radunović;Password=vuki!";
            if (username.Equals("Milan"))
                baseConnectionString = @"Data Source=79.175.71.245;Initial Catalog=Evidencija Potrosnje;User ID=Milan Stevanović;Password=resprojekat123";

            SqlConnection.Close();
            SqlConnection.ConnectionString = baseConnectionString;
            SqlConnection.Open();
        }

    }
}

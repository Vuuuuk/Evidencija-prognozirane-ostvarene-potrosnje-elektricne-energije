using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaPodataka
{
    public class Baza
    {
        private Connection connection = new Connection();
        public void UpisPotrosnje(DateTime vreme, OpenFileDialog file, Potrosnja potrosnja, DateTime datum, string tabela)
        {
            SqlCommand command = new SqlCommand(String.Format("INSERT INTO {0} VALUES (@vreme, @ime, @lokacija, @sat, @load, @oblast, @datum);", tabela), connection.SqlConnection);

            command.Parameters.AddWithValue("@vreme", vreme.ToString("HH:mm"));
            command.Parameters.AddWithValue("@ime", file.SafeFileName);
            command.Parameters.AddWithValue("@lokacija", file.FileName.ToString());
            command.Parameters.AddWithValue("@sat", potrosnja.Sat);
            command.Parameters.AddWithValue("@load", potrosnja.Load);
            command.Parameters.AddWithValue("@oblast", potrosnja.Oblast);
            command.Parameters.AddWithValue("@datum", datum.ToShortDateString());

            command.ExecuteNonQuery();

            Console.WriteLine(command.ToString());
        }
    }
}

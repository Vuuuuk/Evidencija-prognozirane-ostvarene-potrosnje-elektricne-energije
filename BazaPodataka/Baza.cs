using Common.Interface;
using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaPodataka
{
    public class Baza : IBaza
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
        }

        public void UpisNevalidnogFajla(DateTime vreme, OpenFileDialog file, int brojRedova)
        {
            SqlCommand command = new SqlCommand("INSERT INTO EvidencijaNevalidnihFajlova VALUES (@vreme, @ime, @lokacija, @redovi);", connection.SqlConnection);

            command.Parameters.AddWithValue("@vreme", vreme.ToString("HH:mm"));
            command.Parameters.AddWithValue("@ime", file.SafeFileName);
            command.Parameters.AddWithValue("@lokacija", file.FileName.ToString());
            command.Parameters.AddWithValue("@redovi", brojRedova);

            command.ExecuteNonQuery();
        }

        public List<string> GeoLokacije()
        {
            List<string> lista = new List<string>();
            SqlCommand command = new SqlCommand("SELECT DISTINCT(NAZIV) FROM EvidencijaGeoPodrucja;", connection.SqlConnection);
            IDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                lista.Add(reader.GetString(0));
            }
            reader.Close();

            return lista;
        }

        public void EvidentirajGeoLokaciju(string oblast)
        {
            SqlCommand command = new SqlCommand("INSERT INTO EvidencijaGeoPodrucja VALUES (@oblast, @oblast);", connection.SqlConnection);

            command.Parameters.AddWithValue("@oblast", oblast);

            command.ExecuteNonQuery();
        }

        public bool FajlUcitan(string imeFajla)
        {
            List<string> lista = new List<string>();
            SqlCommand command = new SqlCommand("SELECT DISTINCT(imeFajla) FROM EvidencijaOstvarenePotrosnje " +
                                                "UNION " +
                                                "SELECT DISTINCT(imeFajla) FROM EvidencijaPrognoziranePotrosnje; ", connection.SqlConnection);

            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    lista.Add(reader.GetString(0));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            reader.Close();

            if (lista.Contains(imeFajla))
                return true;
            return false;
        }

        public void IsprazniBazu()
        {
            SqlCommand command = new SqlCommand("DELETE FROM EvidencijaGeoPodrucja; " +
                                                "DELETE FROM EvidencijaNevalidnihFajlova;" +
                                                "DELETE FROM EvidencijaOstvarenePotrosnje; " +
                                                "DELETE FROM EvidencijaPrognoziranePotrosnje;", connection.SqlConnection);
            command.ExecuteNonQuery();
        }

        public List<Potrosnja> VratiPotrosnju(string ime, string lokacija, string datum)
        {
            List<Potrosnja> lista = new List<Potrosnja>();

            SqlCommand command = new SqlCommand(String.Format("SELECT sat, load, oblast FROM {0} " +
                                                              "WHERE datum = @datum " +
                                                              "AND oblast LIKE @lokacija " +
                                                              "ORDER BY sat;", ime), connection.SqlConnection);

            command.Parameters.AddWithValue("@lokacija", lokacija);
            command.Parameters.AddWithValue("@datum", datum);

            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    lista.Add(new Potrosnja(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            reader.Close();

            return lista;
        }
    }
}

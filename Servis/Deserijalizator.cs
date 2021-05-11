using Common.Interface;
using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Servis
{
    public class Deserijalizacija : IDeserijalizator
    {
        private XmlDocument xmlOstvarena;
        private XmlDocument xmlPrognozirana;

        private List<Potrosnja> ostvarenaPotrosnja = new List<Potrosnja>();
        private List<Potrosnja> prognoziranaPotrosnja = new List<Potrosnja>();

        public List<Potrosnja> OstvarenaPotrosnja
        {
            get { return ostvarenaPotrosnja; }
            set { ostvarenaPotrosnja = value; }
        }

        public List<Potrosnja> PrognoziranaPotrosnja
        {
            get { return prognoziranaPotrosnja; }
            set { prognoziranaPotrosnja = value; }
        }

        public void LoadXMLOstvarena(OpenFileDialog ofdOstvarena)
        {
            using (Stream stream = ofdOstvarena.OpenFile())
            {
                xmlOstvarena = new XmlDocument();
                xmlOstvarena.Load(stream);
            }
        }

        public void LoadXMLPrognozirana(OpenFileDialog ofdPrognozirana)
        {
            using (Stream stream = ofdPrognozirana.OpenFile())
            {
                xmlPrognozirana = new XmlDocument();
                xmlPrognozirana.Load(stream);
            }
        }

        public void ParsiranjeXMLOstvarena()
        {
            ostvarenaPotrosnja.Clear();
            foreach (XmlNode node in xmlOstvarena.DocumentElement)
                ostvarenaPotrosnja.Add(new Potrosnja(Int32.Parse(node["SAT"].InnerText), Int32.Parse(node["LOAD"].InnerText), node["OBLAST"].InnerText));

        }

        public void ParsiranjeXMLPrognozirana()
        {
            prognoziranaPotrosnja.Clear();
            foreach (XmlNode node in xmlPrognozirana.DocumentElement)
                prognoziranaPotrosnja.Add(new Potrosnja(Int32.Parse(node["SAT"].InnerText), Int32.Parse(node["LOAD"].InnerText), node["OBLAST"].InnerText));

        }


        // Funkcija koja parsira datum iz prosledjenoh imena fajla
        public DateTime ParseDatum(string filename)
        {
            char[] splitChar = { '_', '.' };
            DateTime datum = DateTime.Parse(filename.Split(splitChar)[3] + "." + filename.Split(splitChar)[2] + "." + filename.Split(splitChar)[1]);
            return datum;
        }
    }
}

using GUI.DemoImplementacija.Podaci;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GUI.DemoImplementacija
{
    public class Deserijalizator
    {
        private XmlDocument xmlOstvarena;
        private XmlDocument xmlPrognozirana;
        private OpenFileDialog ofdOstvarena = new OpenFileDialog();
        private OpenFileDialog ofdPrognozirana = new OpenFileDialog();

        private List<OstvarenaPotrosnja> op = new List<OstvarenaPotrosnja>();
        private List<PrognoziranaPotrosnja> pp = new List<PrognoziranaPotrosnja>();

        public List<OstvarenaPotrosnja> Op { get => op; set => op = value; }
        public List<PrognoziranaPotrosnja> Pp { get => pp; set => pp = value; }

        public string IzborXMLOstvarena()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Fajlovi (*.xml) | *.xml| Svi Fajlovi (*.*) | *.*";
            ofd.FilterIndex = 0;
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog().Value)
            {
                if (!String.Equals(Path.GetExtension(ofd.FileName), ".xml", StringComparison.OrdinalIgnoreCase))
                    return "Error_" + ofd.SafeFileName;
                else
                {
                    ofdOstvarena = ofd;
                    return ofd.SafeFileName;
                }
            }
            return "Empty";
        }

        public string IzborXMLPrognozirana()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Fajlovi (*.xml) | *.xml| Svi Fajlovi (*.*) | *.*";
            ofd.FilterIndex = 0;
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog().Value)
            {
                if (!String.Equals(Path.GetExtension(ofd.FileName), ".xml", StringComparison.OrdinalIgnoreCase))
                    return "Error_" + ofd.SafeFileName;
                else
                {
                    ofdPrognozirana = ofd;                    
                    return ofd.SafeFileName;
                }
            }
            return "Empty";
        }

        // TODO: Spojiti kod za parser datuma sa ostalim funkcionalnim kodom i dodati upis u bazu
        public DateTime ParserDatuma(string filename)
        {
            char[] splitChar = { '_', '.' };
            DateTime datum = DateTime.Parse(filename.Split(splitChar)[3] + "." + filename.Split(splitChar)[2] + "." + filename.Split(splitChar)[1]);
            return datum;
        }

        public void LoadXMLOstvarena()
        {
            using (Stream stream = ofdOstvarena.OpenFile())
            {
                xmlOstvarena = new XmlDocument();
                xmlOstvarena.Load(stream);
            }
        }

        public void LoadXMLPrognozirana()
        {
            using (Stream stream = ofdPrognozirana.OpenFile())
            {
                xmlPrognozirana = new XmlDocument();
                xmlPrognozirana.Load(stream);
            }
        }

        public void ParsiranjeXMLOstvarena()
        {
            Op.Clear();
            foreach (XmlNode node in xmlOstvarena.DocumentElement)
                Op.Add(new OstvarenaPotrosnja(Int32.Parse(node["SAT"].InnerText), Int32.Parse(node["LOAD"].InnerText), node["OBLAST"].InnerText));

        }

        public void ParsiranjeXMLPrognozirana()
        {
            Pp.Clear();
            foreach (XmlNode node in xmlPrognozirana.DocumentElement)
                Pp.Add(new PrognoziranaPotrosnja(Int32.Parse(node["SAT"].InnerText), Int32.Parse(node["LOAD"].InnerText), node["OBLAST"].InnerText));

        }
    }
}

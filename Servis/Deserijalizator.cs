﻿using Common.Interface;
using Common.Models;
using Microsoft.Win32;
using Servis.Exceptions;
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
        public XmlDocument xmlOstvarena;
        public XmlDocument xmlPrognozirana;

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

        public void LoadXMLOstvarena(MemoryStream ms)
        {

            //EXCEPTION
            if (ms.Length.Equals(0))
                throw new PrazanArgumentException();

            StreamReader sr = new StreamReader(ms);
            xmlOstvarena = new XmlDocument();
            xmlOstvarena.Load(sr);
        }

        public void LoadXMLPrognozirana(MemoryStream ms)
        {

            //EXCEPTION
            if (ms.Length.Equals(0))
                throw new PrazanArgumentException();

            StreamReader sr = new StreamReader(ms);
            xmlPrognozirana = new XmlDocument();
            xmlPrognozirana.Load(sr);
        }

        public void ParsiranjeXMLOstvarena()
        {

            //EXCEPTION
            if (xmlOstvarena.ChildNodes.Count.Equals(0))
                throw new PrazanXMLException();

            ostvarenaPotrosnja.Clear();
            foreach (XmlNode node in xmlOstvarena.DocumentElement)
                ostvarenaPotrosnja.Add(new Potrosnja(Int32.Parse(node["SAT"].InnerText), Int32.Parse(node["LOAD"].InnerText), node["OBLAST"].InnerText));

        }

        public void ParsiranjeXMLPrognozirana()
        {

            //EXCEPTION
            if (xmlPrognozirana.ChildNodes.Count.Equals(0))
                throw new PrazanXMLException();

            prognoziranaPotrosnja.Clear();
            foreach (XmlNode node in xmlPrognozirana.DocumentElement)
                prognoziranaPotrosnja.Add(new Potrosnja(Int32.Parse(node["SAT"].InnerText), Int32.Parse(node["LOAD"].InnerText), node["OBLAST"].InnerText));

        }


        // Funkcija koja parsira datum iz prosledjenoh imena fajla
        public DateTime ParseDatum(string filename)
        {

            //EXCEPTION
            if (filename.Equals(string.Empty) || filename.Equals(null))
                throw new PrazanArgumentException();

            char[] splitChar = { '_', '.' };
            DateTime datum = DateTime.Parse(filename.Split(splitChar)[3] + "." + filename.Split(splitChar)[2] + "." + filename.Split(splitChar)[1]);
            return datum;
        }

        public int BrojRedova(OpenFileDialog ofd)
        {

            //EXCEPTION
            if (ofd.FileName.Equals(null) || ofd.FileName.Equals(string.Empty))
                throw new PrazanArgumentException();

            string[] readText = System.IO.File.ReadAllLines(ofd.FileName);
            return readText.Length;
        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servis.Exceptions;
using System.IO;
using System.Xml;

namespace ServisTest
{
    [TestFixture]
    public class DeserijalizatorTest
    {
        private Servis.Deserijalizacija deserijalizatorTestObjekat;
        MemoryStream ms; 
        
        [SetUp]
        public void Kreiranje()
        {
            deserijalizatorTestObjekat = new Servis.Deserijalizacija();
            ms = new MemoryStream();
        }

        [TearDown]
        public void Brisanje()
        {
            ms.Close();
        }

        [Test]
        public void LoadXMLOstvarenaPrazanArgument()
        {
            Assert.Throws<PrazanArgumentException>(() => deserijalizatorTestObjekat.LoadXMLOstvarena(ms));
        }

        [Test]
        public void LoadXMLPrognoziranaPrazanArgument()
        {
            Assert.Throws<PrazanArgumentException>(() => deserijalizatorTestObjekat.LoadXMLPrognozirana(ms));
        }

        [Test]
        public void ParsiranjeXMLOstvarenaPrazanXML()
        {
            XmlDocument xmlOstvarena = new XmlDocument();
            deserijalizatorTestObjekat.xmlOstvarena = xmlOstvarena;
            Assert.Throws<PrazanXMLException>(() => deserijalizatorTestObjekat.ParsiranjeXMLOstvarena());
        }

        [Test]
        public void ParsiranjeXMLPrognoziranaPrazanXML()
        {
            XmlDocument xmlPrognozirana = new XmlDocument();
            deserijalizatorTestObjekat.xmlPrognozirana = xmlPrognozirana;
            Assert.Throws<PrazanXMLException>(() => deserijalizatorTestObjekat.ParsiranjeXMLPrognozirana());
        }

        [Test]
        public void ParseDatumPrazanArgument()
        {
            Assert.Throws<PrazanArgumentException>(() => deserijalizatorTestObjekat.ParseDatum(string.Empty));
        }

        [Test]
        public void ParseDatumProsledjenArgument()
        {
            List<string> datumTest = new List<string> { "ostv_2020_05_07", "prog_2020_05_07" };
            foreach (string s in datumTest)
                Assert.DoesNotThrow(() => deserijalizatorTestObjekat.ParseDatum(s));
        }

    }
}

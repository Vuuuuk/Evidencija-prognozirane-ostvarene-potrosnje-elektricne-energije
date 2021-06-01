using Common.Exceptions;
using Common.Interface;
using Common.Models;
using Moq;
using NUnit.Framework;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisTest
{
    [TestFixture]
    class ProracunTest
    {
        Mock<IBaza> bazaMoq;
        IProracun proracunTestObjekat;

        [SetUp]
        public void SetUpMethod()
        {
            bazaMoq = new Mock<IBaza>();
        }

        [TearDown]
        public void TearDownMethod()
        {
            bazaMoq = null;
            proracunTestObjekat = null;
        }

        [Test]
        public void PopuniListuPotrosnjeReturnTest()
        {
            bazaMoq.Setup(_ => _.VratiPotrosnju("EvidencijaOstvarenePotrosnje", "VOJ", "28.03.2021.")).Returns(new List<IPotrosnja>() { new Potrosnja(1, 2000, "VOJ") });
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            string ime = "EvidencijaOstvarenePotrosnje";
            string lokacija = "VOJ";
            string datum = "28.03.2021.";
            Assert.AreEqual(proracunTestObjekat.PopuniListuPotrosnje(ime, lokacija, datum), new List<IPotrosnja>() { new Potrosnja(1, 2000, "VOJ") });
        }

        [Test]
        public void PopuniListuPotrosnjePogresanReturnTest()
        {
            bazaMoq.Setup(_ => _.VratiPotrosnju("EvidencijaOstvarenePotrosnje", "VOJ", "28.03.2021.")).Returns(new List<IPotrosnja>() { new Potrosnja(1, 2000, "VOJ") });
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            string ime = "EvidencijaOstvarenePotrosnje";
            string lokacija = "VOJ";
            string datum = "28.03.2021.";
            Assert.AreNotEqual(proracunTestObjekat.PopuniListuPotrosnje(ime, lokacija, datum), new List<IPotrosnja>() { new Potrosnja(1, 2000, "BGD") });
        }

        [Test]
        public void PopuniListuPotrosnjePrazanArgumentTest()
        {
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            Assert.Throws<PrazanArgumentException>(() => proracunTestObjekat.PopuniListuPotrosnje(String.Empty, String.Empty, String.Empty));
        }

        [Test]
        public void PopuniListuPotrosnjeDobarArgumentTest()
        {
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            Assert.DoesNotThrow(() => proracunTestObjekat.PopuniListuPotrosnje("EvidencijaOstvarenePotrosnje", "VOJ", "28.03.2021."));
        }

        [Test]
        public void IzracunajOdstupanjePrazanArgumentTest()
        {
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            Assert.Throws<PrazanArgumentException>(() => proracunTestObjekat.IzracunajOdstupanje(new List<IPotrosnja>(), new List<IPotrosnja>()));
        }

        [Test]
        public void IzracunajOdstupanjeDobarArgumentTest()
        {
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            List<IPotrosnja> ostvarena = new List<IPotrosnja>();
            List<IPotrosnja> prognozirana = new List<IPotrosnja>();
            ostvarena.Add(new Potrosnja(1, 2000, "VOJ"));
            prognozirana.Add(new Potrosnja(1, 3000, "VOJ"));

            Assert.DoesNotThrow(() => proracunTestObjekat.IzracunajOdstupanje(ostvarena, prognozirana));
        }

        [Test]
        public void IzracunajOdstupanjeReturnTest()
        {
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            List<IPotrosnja> ostvarena = new List<IPotrosnja>();
            List<IPotrosnja> prognozirana = new List<IPotrosnja>();
            ostvarena.Add(new Potrosnja(1, 2000, "VOJ"));
            prognozirana.Add(new Potrosnja(1, 2000, "VOJ"));

            List<IRelativnoOdstupanje> returnList = new List<IRelativnoOdstupanje>();
            returnList.Add(new RelativnoOdstupanje(1, 2000, 2000, 0));

            Assert.AreEqual(proracunTestObjekat.IzracunajOdstupanje(ostvarena, prognozirana), returnList);
        }

        [Test]
        public void IzracunajOdstupanjePogresanReturnTest()
        {
            proracunTestObjekat = new Proracun(bazaMoq.Object);
            List<IPotrosnja> ostvarena = new List<IPotrosnja>();
            List<IPotrosnja> prognozirana = new List<IPotrosnja>();
            ostvarena.Add(new Potrosnja(1, 2000, "VOJ"));
            prognozirana.Add(new Potrosnja(1, 3000, "VOJ"));

            List<IRelativnoOdstupanje> returnList = new List<IRelativnoOdstupanje>();
            returnList.Add(new RelativnoOdstupanje(1, 2000, 3000, 30));

            Assert.AreNotEqual(proracunTestObjekat.IzracunajOdstupanje(ostvarena, prognozirana), returnList);
        }
    }
}

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
    class EvidentiranjeTest
    {
        Mock<IBaza> bazaMoq;
        IEvidentiranje evidentiranjeTestObjekat;

        [SetUp]
        public void SetUpMethod()
        {
            bazaMoq = new Mock<IBaza>();
        }

        [TearDown]
        public void TearDownMethod()
        {
            bazaMoq = null;
            evidentiranjeTestObjekat = null;
        }

        [Test]
        public void EvidentirajOblastiPrazanArgument()
        {
            evidentiranjeTestObjekat = new Evidentiranje(bazaMoq.Object);
            Assert.Throws<PrazanArgumentException>(() => evidentiranjeTestObjekat.EvidentirajOblasti(new List<IPotrosnja>()));
        }

        [Test]
        public void EvidentirajOblastiPraznaLista()
        {
            List<IPotrosnja> potrosnja = new List<IPotrosnja>();

            bazaMoq.Setup(_ => _.GeoLokacije()).Returns(new List<string>() { "VOJ", "BGD" });

            IPotrosnja p1 = new Potrosnja();
            p1.sat = 1;
            p1.load = 3000;
            p1.oblast = "VOJ";

            potrosnja.Add(p1);

            evidentiranjeTestObjekat = new Evidentiranje(bazaMoq.Object);

            Assert.Throws<PraznaListaException>(() => evidentiranjeTestObjekat.EvidentirajOblasti(potrosnja));
        }

        [Test]
        public void EvidentirajOblastiDobarArgument()
        {
            List<IPotrosnja> potrosnja = new List<IPotrosnja>();

            bazaMoq.Setup(_ => _.GeoLokacije()).Returns(new List<string>() { "VOJ", "BGD" });

            IPotrosnja p1 = new Potrosnja();
            p1.sat = 1;
            p1.load = 3000;
            p1.oblast = "SUB";

            potrosnja.Add(p1);

            bazaMoq.Setup(_ => _.EvidentirajGeoLokaciju(p1.oblast));
            evidentiranjeTestObjekat = new Evidentiranje(bazaMoq.Object);

            Assert.DoesNotThrow(() => evidentiranjeTestObjekat.EvidentirajOblasti(potrosnja));
        }

        [Test]
        public void EvidentirajOblastiReturnTest()
        {
            bazaMoq.Setup(_ => _.GeoLokacije()).Returns(new List<string>() { "VOJ", "BGD" });
            List<IPotrosnja> potrosnja = new List<IPotrosnja>();

            IPotrosnja p1 = new Potrosnja();
            p1.sat = 1;
            p1.load = 3000;
            p1.oblast = "VOJ";

            IPotrosnja p2 = new Potrosnja();
            p2.sat = 2;
            p2.load = 4000;
            p2.oblast = "BGD";

            IPotrosnja p3 = new Potrosnja();
            p3.sat = 1;
            p3.load = 5000;
            p3.oblast = "SUB";

            potrosnja.Add(p1);
            potrosnja.Add(p2);
            potrosnja.Add(p3);

            foreach (IPotrosnja p in potrosnja)
                bazaMoq.Setup(_ => _.EvidentirajGeoLokaciju(p.oblast));

            evidentiranjeTestObjekat = new Evidentiranje(bazaMoq.Object);

            Assert.AreEqual(evidentiranjeTestObjekat.EvidentirajOblasti(potrosnja), new List<string>() {"SUB"});
        }

        [Test]
        public void EvidentirajOblastiPogresanReturnTest()
        {
            bazaMoq.Setup(_ => _.GeoLokacije()).Returns(new List<string>() { "VOJ", "BGD" });
            List<IPotrosnja> potrosnja = new List<IPotrosnja>();

            IPotrosnja p1 = new Potrosnja();
            p1.sat = 1;
            p1.load = 3000;
            p1.oblast = "VOJ";

            IPotrosnja p2 = new Potrosnja();
            p2.sat = 2;
            p2.load = 4000;
            p2.oblast = "BGD";

            IPotrosnja p3 = new Potrosnja();
            p3.sat = 1;
            p3.load = 5000;
            p3.oblast = "SUB";

            potrosnja.Add(p1);
            potrosnja.Add(p2);
            potrosnja.Add(p3);

            foreach (IPotrosnja p in potrosnja)
                bazaMoq.Setup(_ => _.EvidentirajGeoLokaciju(p.oblast));

            evidentiranjeTestObjekat = new Evidentiranje(bazaMoq.Object);

            Assert.AreNotEqual(evidentiranjeTestObjekat.EvidentirajOblasti(potrosnja), new List<string>() { "BGD" });
        }
    }
}

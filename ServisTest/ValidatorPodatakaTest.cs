using Common.Exceptions;
using Common.Interface;
using Common.Models;
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
    class ValidatorPodatakaTest
    {
        IValidatorPodataka validatorPodatakaTestObjekat;

        [SetUp]
        public void SetUpMethod()
        {
            validatorPodatakaTestObjekat = new ValidatorPodataka();
        }

        [TearDown]
        public void TearDownMethod()
        {
            validatorPodatakaTestObjekat = null;
        }

        [Test]
        public void ValidatorPodatakaPrazanArgumentTest()
        {
            Assert.Throws<PrazanArgumentException>(() => validatorPodatakaTestObjekat.Validator(DateTime.MinValue, new List<IPotrosnja>()));
        }

        [Test]
        public void ValidatorPodatakaDobarArgumentTest()
        {
            Assert.DoesNotThrow(() => validatorPodatakaTestObjekat.Validator(DateTime.Now, new List<IPotrosnja>() { new Potrosnja(1, 2000, "VOJ")}));
        }


        // Testovi kada dan ima 23 sata
        [Test]
        public void ValidatorPodatakaReturn23Test()
        {
            List<IPotrosnja> list = new List<IPotrosnja>();
            for (int i = 0; i < 23; i++)
            {
                list.Add(new Potrosnja(i + 1, 2000 + 20 * i, "VOJ"));
            }
            // 28. mart 2021. godine ima 23 sata!
            Assert.IsTrue(validatorPodatakaTestObjekat.Validator(new DateTime(2021, 03, 28), list));
        }

        [Test]
        public void ValidatorPodatakaReturn23PogresanTest()
        {
            List<IPotrosnja> list = new List<IPotrosnja>();
            for (int i = 0; i < 23; i++)
            {
                list.Add(new Potrosnja(i + 1, 2000 + 20 * i, "VOJ"));
            }

            Assert.IsFalse(validatorPodatakaTestObjekat.Validator(new DateTime(2021, 06, 01), list));
        }

        // Testovi kada dan ima 24 sata
        [Test]
        public void ValidatorPodatakaReturn24Test()
        {
            List<IPotrosnja> list = new List<IPotrosnja>();
            for (int i = 0; i < 24; i++)
            {
                list.Add(new Potrosnja(i + 1, 2000 + 20 * i, "VOJ"));
            }

            Assert.IsTrue(validatorPodatakaTestObjekat.Validator(new DateTime(2021, 06, 01), list));
        }

        [Test]
        public void ValidatorPodatakaReturn24PogresanTest()
        {
            List<IPotrosnja> list = new List<IPotrosnja>();
            for (int i = 0; i < 24; i++)
            {
                list.Add(new Potrosnja(i + 1, 2000 + 20 * i, "VOJ"));
            }
            // 28. mart 2021. godine ima 23 sata!
            Assert.IsFalse(validatorPodatakaTestObjekat.Validator(new DateTime(2021, 03, 28), list));
        }

        // Testovi kada dan ima 25 sata
        [Test]
        public void ValidatorPodatakaReturn25Test()
        {
            List<IPotrosnja> list = new List<IPotrosnja>();
            for (int i = 0; i < 25; i++)
            {
                list.Add(new Potrosnja(i + 1, 2000 + 20 * i, "VOJ"));
            }
            // 31. oktobar 2021. godine ima 25 sata!
            Assert.IsTrue(validatorPodatakaTestObjekat.Validator(new DateTime(2021, 10, 31), list));
        }

        [Test]
        public void ValidatorPodatakaReturn25PogresanTest()
        {
            List<IPotrosnja> list = new List<IPotrosnja>();
            for (int i = 0; i < 25; i++)
            {
                list.Add(new Potrosnja(i + 1, 2000 + 20 * i, "VOJ"));
            }

            Assert.IsFalse(validatorPodatakaTestObjekat.Validator(new DateTime(2021, 06, 01), list));
        }
    }
}

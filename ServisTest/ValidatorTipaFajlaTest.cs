using Common.Exceptions;
using Common.Interface;
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
    class ValidatorTipaFajlaTest
    {
        IValidatorTipaFajla validatorTipaFajlaTestObjekat;

        [SetUp]
        public void SetUpMethod()
        {
            validatorTipaFajlaTestObjekat = new ValidatorTipaFajla();
        }

        [TearDown]
        public void TearDownMethod()
        {
            validatorTipaFajlaTestObjekat = null;
        }

        [Test]
        public void ValidatorTipaPrazanArgumentTest()
        {
            Assert.Throws<PrazanArgumentException>(() => validatorTipaFajlaTestObjekat.ValidatorTipa(String.Empty));
        }

        [Test]
        public void ValidatorTipaDobarArgument()
        {
            Assert.DoesNotThrow(() => validatorTipaFajlaTestObjekat.ValidatorTipa("ostv_2021_28_03.xml"));
        }

        [Test]
        public void ValidatorTipaReturnTest()
        {
            Assert.IsTrue(validatorTipaFajlaTestObjekat.ValidatorTipa("ostv_2021_28_03.xml"));
        }

        [Test]
        public void ValidatorTipaPogresanReturnTest()
        {
            Assert.IsFalse(validatorTipaFajlaTestObjekat.ValidatorTipa("ostv_2021_28_03.csv"));
        }

        [Test]
        public void ValidatorTipaPogresanReturn2Test()
        {
            Assert.IsFalse(validatorTipaFajlaTestObjekat.ValidatorTipa("ostv_2021_28_03"));
        }
    }
}

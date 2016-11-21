using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarCodeLib.Tests
{
    [TestClass]
    public class Ean13Tests
    {
        [TestMethod]
        public void CanCalculateCheckDigit()
        {
            int check = Ean13.CalculateCheckDigit("890400021003");

            Assert.AreEqual(7, check);

            check = Ean13.CalculateCheckDigit("201000450000");
            Assert.AreEqual(8, check);

            check = Ean13.CalculateCheckDigit("978020113447");
            Assert.AreEqual(6, check);
        }

        [TestMethod]
        public void CanGeneratedCodedEan13String()
        {
            var result = Ean13.GetEan13EncodedString("3596710216079");

            Assert.AreEqual("3FJQRLA*cbgahj+", result);
        }

        [TestMethod]
        public void CanRetrieveCheckDigitFromCode()
        {
            var check = Ean13.GetCheckDigitFromEan13("2010004500008");
            Assert.AreEqual("8", check);
        }

        [TestMethod]
        public void CanRetrieveCodePart()
        {
            var code = Ean13.GetCodePartFromEan13("2010004500008");
            Assert.AreEqual("201000450000", code);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Luhn = Syncthing.Protocol.Luhn;

namespace Syncthing.Tests.Protocol
{
    [TestClass]
    public class LuhnAlphabetTest
    {
        [TestMethod]
        public void TestGenerateBase6()
        {
            // Base 6 Luhn
            var a = new Luhn.Alphabet("abcdef");
            var c = Convert.ToChar(a.Generate("abcdef"));

            Assert.AreEqual(c, 'e', String.Format("Incorrect check digit {0} != e", c));
        }

        [TestMethod]
        public void TestGenerateBase10()
        {
             // Base 10 Luhn
            var a = new Luhn.Alphabet("0123456789");
            var c = Convert.ToChar(a.Generate("7992739871"));

              Assert.AreEqual(c, '3', String.Format("Incorrect check digit {0} != 3", c));
        }

        [TestMethod]
        [ExpectedException(typeof(Luhn.LuhnAlphabetException))]
        public void TestInvalidInitializationString()
        {
            var a = new Luhn.Alphabet("");
        }

        [TestMethod]
        [ExpectedException(typeof(Luhn.LuhnAlphabetException))]
        public void TestInvalidString()
        {
            var a = new Luhn.Alphabet("ABC");
            var c = a.Generate("7992739871");
        }

        [TestMethod]
        [ExpectedException(typeof(Luhn.LuhnAlphabetException))]
        public void TestBadAlphabet()
        {
            var a = new Luhn.Alphabet("01234566789");
            var c = a.Generate("7992739871");
        }

        [TestMethod]
        public void TestValidateTrue()
        {
            var a = new Luhn.Alphabet("abcdef");

            Assert.IsTrue(a.Validate("abcdefe"), "Incorrect validation response for abcdefe");
        }

        [TestMethod]
        public void TestValidateFalse()
        {
            var a = new Luhn.Alphabet("abcdef");

            Assert.IsFalse(a.Validate("abcdefd"), "Incorrect validation response for abcdefd");
        }
    }
}

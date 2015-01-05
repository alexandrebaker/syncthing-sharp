using System;
using NUnit.Framework;
using Luhn = Syncthing.Protocol.v1.Luhn;

namespace Syncthing.Tests.Protocol
{
    [TestFixture]
    public class LuhnAlphabetTest
    {
        [Test]
        public void TestGenerateBase6()
        {
            // Base 6 Luhn
            var a = new Luhn.Formula("abcdef");
            var c = Convert.ToChar(a.Generate("abcdef"));

            Assert.AreEqual(c, 'e', String.Format("Incorrect check digit {0} != e", c));
        }

        [Test]
        public void TestGenerateBase10()
        {
            // Base 10 Luhn
            var a = new Luhn.Formula("0123456789");
            var c = Convert.ToChar(a.Generate("7992739871"));

            Assert.AreEqual(c, '3', String.Format("Incorrect check digit {0} != 3", c));
        }

        [Test]
        [ExpectedException(typeof(Luhn.LuhnFormulaException))]
        public void TestInvalidInitializationString()
        {
            new Luhn.Formula("");
        }

        [Test]
        [ExpectedException(typeof(Luhn.LuhnFormulaException))]
        public void TestInvalidString()
        {
            var a = new Luhn.Formula("ABC");
            a.Generate("7992739871");
        }

        [Test]
        [ExpectedException(typeof(Luhn.LuhnFormulaException))]
        public void TestBadAlphabet()
        {
            var a = new Luhn.Formula("01234566789");
           	a.Generate("7992739871");
        }

        [Test]
        public void TestValidateTrue()
        {
            var a = new Luhn.Formula("abcdef");

            Assert.IsTrue(a.Validate("abcdefe"), "Incorrect validation response for abcdefe");
        }

        [Test]
        public void TestValidateFalse()
        {
            var a = new Luhn.Formula("abcdef");

            Assert.IsFalse(a.Validate("abcdefd"), "Incorrect validation response for abcdefd");
        }
    }
}

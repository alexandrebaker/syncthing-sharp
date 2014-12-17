using System;
using NUnit.Framework;
using Syncthing.Protocol.v1.Messages;
using Syncthing.IO.Xdr;
using Syncthing.Protocol.v1;
using System.Text;

namespace Syncthing.Tests.Protocol
{
    [TestFixture]
    public class MessageTest
    {
        [Test]
        [ExpectedException(typeof(XdrElementSizeExceeded))]
        public void IMessageExtensionValidateLengthTest()
        {
            BlockInfo b = new BlockInfo();
            b.Hash = Encoding.UTF8.GetBytes("This is a long string representing a very long path to a folder that should get the validate to throw an exception");
            b.ValidateLength();
        }
    }
}


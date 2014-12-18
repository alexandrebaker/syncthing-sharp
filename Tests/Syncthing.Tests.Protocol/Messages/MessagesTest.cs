using System;
using NUnit.Framework;
using Syncthing.Protocol.v1.Messages;
using Syncthing.IO.Xdr;
using Syncthing.Protocol.v1;
using System.Text;

namespace Syncthing.Tests.Protocol
{
    [TestFixture]
    public class MessagesTest
    {
        [Test]
        [ExpectedException(typeof(XdrElementSizeExceeded))]
        public void MessageExtensionValidateBytesArrayLengthTest()
        {
            BlockInfo b = new BlockInfo
            {
                Hash =
                    Encoding.UTF8.GetBytes(
                        "This is a long string representing a very long path to a folder that should get the validate to throw an exception")
            };
            b.ValidateLength();
        }

        [Test]
        [ExpectedException(typeof(XdrElementSizeExceeded))]
        public void MessageExtensionValidateStringArrayLengthTest()
        {
            IndexMessage b = new IndexMessage()
            {
                Folder =
                       "This is a long string representing a very long path to a folder that should get the validate to throw an exception"
            };
            b.ValidateLength();
        }

        [Test]
        [ExpectedException(typeof(XdrElementSizeExceeded))]
        public void MessageExtensionValidateObjectArrayLengthTest()
        {
            ClusterConfigMessage b = new ClusterConfigMessage()
            {
                Folders = new Folder[65]
            };
            b.ValidateLength();
        }
    }
}


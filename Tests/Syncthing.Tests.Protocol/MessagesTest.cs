using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncthing.Protocol.Messages;


namespace Syncthing.Tests.Protocol
{
    [TestClass]
    public class MessagesTest
    {
        [TestMethod]
        public void TestHeaderFunctions()
        {
            Func<int, int, int, bool> func = (ver, id, typ) =>
            {
                ver = ver%16;
                id = id%4096;
                typ = typ%256;

                var h0 = new Header
                {
                    Version = ver,
                    MessageId = id,
                    MessageType = typ
                };

                var h1 = Header.Decode(Header.Encode(h0));

                return h0.Equals(h1);
            };
            // test the Decode and Encode for header 100 time with random values.
            for (int i = 0; i <= 100; i++)
            {
                Random rnd = new Random(DateTime.Now.Millisecond*DateTime.UtcNow.Millisecond);
                Assert.IsTrue(func(rnd.Next(), rnd.Next(), rnd.Next()));
            }
        }

        [TestMethod]
        public void TestHeaderLayout4Bits()
        {
            // Version are the first four bits
            uint e = 0xf0000000;
            uint a = Header.Encode(new Header {Version = 0xf});
            Assert.AreEqual(e, a, String.Format("Header layout incorrect; {0:X} != {1:X}", a, e));
        }

        [TestMethod]
        public void TestHeaderLayout8Bits()
        {
            // Version are the first four bits
            uint e = 0x0000ff00;
            uint a = Header.Encode(new Header { MessageType = 0xff });
            Assert.AreEqual(e, a, String.Format("Header layout incorrect; {0:X} != {1:X}", a, e));
        }

        [TestMethod]
        public void TestHeaderLayout12Bits()
        {
            // Version are the first four bits
            uint e = 0x0fff0000;
            uint a = Header.Encode(new Header { MessageId = 0xfff });
            Assert.AreEqual(e, a, String.Format("Header layout incorrect; {0:X} != {1:X}", a, e));
        }
    }
}

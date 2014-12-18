using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Syncthing.IO;
using Syncthing.IO.Xdr;
using Syncthing.Protocol.v1;
using Syncthing.Protocol.v1.Messages;
using FileInfo = Syncthing.Protocol.v1.Messages.FileInfo;

namespace Syncthing.Tests.Protocol.Messages
{
    [TestFixture]
    public class MessagesEncodeDecodeRoudTripTest
    {
        private void AssertPtopertiesAreEqual(object o1, object o2)
        {
            Assert.AreEqual(o1.GetType(), o2.GetType());

            var props = from p in o1.GetType().GetProperties()
                        let attr = p.GetCustomAttributes(typeof(IgnoreEncodeAttribute), true)
                        where attr == null || attr.Length == 0
                        select p;

            foreach (var p in props)
                Assert.AreEqual(p.GetValue(o1), p.GetValue(o2));

        }

        private void AssertFolder(Folder f1, Folder f2)
        {
            Assert.AreEqual(f1.ID, f2.ID);
            Assert.AreEqual(f1.Devices.Length, f2.Devices.Length);
            for (int i = 0; i < f1.Devices.Length; i++)
                AssertPtopertiesAreEqual(f1.Devices[i], f2.Devices[i]);
        }

        private void AssertBaseFileInfo(BaseFileInfo b1, BaseFileInfo b2)
        {
            Assert.AreEqual(b1.Name, b2.Name);
            Assert.AreEqual(b1.Flags, b2.Flags);
            Assert.AreEqual(b1.Modified, b2.Modified);
            Assert.AreEqual(b1.Version, b2.Version);
            Assert.AreEqual(b1.LocalVersion, b2.LocalVersion);
        }

        private void AssertFileInfo(FileInfo f1, FileInfo f2)
        {
            AssertBaseFileInfo(f1, f2);
            Assert.AreEqual(f1.Blocks.Length, f2.Blocks.Length);
            for (int i = 0; i < f1.Blocks.Length; i++)
                AssertPtopertiesAreEqual(f1.Blocks[i], f2.Blocks[i]);
        }


        private Stream GetStream()
        {
            return new MemoryStream();
        }

        private byte[] GetRandomBytes(int count)
        {
            var bytes = new byte[count];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            return bytes;
        }

        private Device GetDevice()
        {
            int seed = BitConverter.ToInt32(GetRandomBytes(4), 0);
            Random rnd = new Random(seed);

            var device = new Device
            {
                ID = new byte[32],
                Flags = (uint)rnd.Next(0, int.MaxValue),
                MaxLocalVersion = (ulong)rnd.Next(0, int.MaxValue),
            };

            device.ID = GetRandomBytes(32);

            return device;
        }

        private Folder GetFolder(byte innerDevice)
        {
            var devices = new Device[innerDevice];
            for (int i = innerDevice - 1; i >= 0; i--)
                devices[i] = GetDevice();

            return new Folder
            {
                ID = innerDevice + " - this is an acceptable id",
                Devices = devices
            };
        }

        private Option GetOption()
        {
            return new Option
            {
                Key = "this is an acceptable key",
                Value = Guid.NewGuid().ToString()
            };
        }

        private BlockInfo GetBlockInfo()
        {
            return new BlockInfo
            {
                Hash = GetRandomBytes(64),
                Offset = 123456,
                Size = 9876543
            };
        }

        private FileInfo GetFileInfo(byte innerBlock)
        {
            var blocks = new BlockInfo[innerBlock];
            for (int i = innerBlock - 1; i >= 0; i--)
                blocks[i] = GetBlockInfo();

            BaseFileInfo fi = new FileInfo
            {
                Blocks = blocks
            };
            SetBaseInfo(fi);

            return (FileInfo)fi;
        }
        private FileInfoTruncated GetFileTruncatedInfo()
        {
            int seed = BitConverter.ToInt32(GetRandomBytes(4), 0);
            Random rnd = new Random(seed);

            BaseFileInfo fi = new FileInfoTruncated
            {
                NumBlocks = (uint) rnd.Next(0, int.MaxValue)
            };
            SetBaseInfo(fi);

            return (FileInfoTruncated)fi;
        }

        private void SetBaseInfo([In, Out] BaseFileInfo fi)
        {
            int seed = BitConverter.ToInt32(GetRandomBytes(4), 0);
            Random rnd = new Random(seed);

            fi.Name = @"name with special char )(*&^%$#@\""-" + Guid.NewGuid();
            fi.Version = (ulong) rnd.Next(0, int.MaxValue);
            fi.Flags = (uint) rnd.Next(0, int.MaxValue);
            fi.LocalVersion = (ulong) rnd.Next(0, int.MaxValue);
            fi.Modified = rnd.Next(0, int.MaxValue);
        }










        [Test]
        public void EmptyMessageTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            EmptyMessage msg1 = new EmptyMessage();

            msg1.EncodeXdr(w);

            XdrReader r = new XdrReader(localStream);
            EmptyMessage msg2 = new EmptyMessage();

            msg2.DecodeXdr(r);
        }

        [Test]
        public void OptionTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            Option msg1 = GetOption();

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            Option msg2 = new Option();

            msg2.DecodeXdr(r);

            AssertPtopertiesAreEqual(msg1, msg2);
        }

        [Test]
        public void DeviceTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            Device msg1 = GetDevice();

            msg1.EncodeXdr(w);


            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            Device msg2 = new Device();

            msg2.DecodeXdr(r);

            AssertPtopertiesAreEqual(msg1, msg2);
        }

        [Test]
        public void BlockInfoTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            BlockInfo msg1 = GetBlockInfo();

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            BlockInfo msg2 = new BlockInfo();

            msg2.DecodeXdr(r);

            AssertPtopertiesAreEqual(msg1, msg2);
        }

        [Test]
        public void CloseMessageTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            CloseMessage msg1 = new CloseMessage
            {
                Reason = "this is an acceptable reason."
            };

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            CloseMessage msg2 = new CloseMessage();

            msg2.DecodeXdr(r);

            AssertPtopertiesAreEqual(msg1, msg2);
        }

        [Test]
        public void FolderTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            Folder msg1 = GetFolder(3);

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            Folder msg2 = new Folder();

            msg2.DecodeXdr(r);

            AssertFolder(msg1, msg2);
        }

        [Test]
        public void ResponseMessageTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            ResponseMessage msg1 = new ResponseMessage
            {
                Data = GetRandomBytes(100)
            };

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            ResponseMessage msg2 = new ResponseMessage();

            msg2.DecodeXdr(r);

            AssertPtopertiesAreEqual(msg1, msg2);
        }

        [Test]
        public void RequestMessageTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            RequestMessage msg1 = new RequestMessage
            {
                Folder = @"this is an acceptable string with special char |?>\)(*&^%",
                Name = @"name with special char )(*&^%$#@\""",
                Size = 12335676,
                Offset = 8765 ^ 123 % 5
            };

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            RequestMessage msg2 = new RequestMessage();

            msg2.DecodeXdr(r);

            AssertPtopertiesAreEqual(msg1, msg2);
        }

        [Test]
        public void ClusterConfigMessageTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            ClusterConfigMessage msg1 = new ClusterConfigMessage
            {
                ClientName = @"this is an acceptable string with special char |?>\)(*&^%",
                ClientVersion = @"version.with.special.char.)(*&^%$#@\""",
                Folders = new[]
                {
                    GetFolder(1),
                    GetFolder(3),
                    GetFolder(10)
                },
                Options = new[]
                {
                    GetOption(),
                    GetOption()
                }

            };

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            ClusterConfigMessage msg2 = new ClusterConfigMessage();

            msg2.DecodeXdr(r);

            Assert.AreEqual(msg1.ClientName, msg2.ClientName);
            Assert.AreEqual(msg1.ClientVersion, msg2.ClientVersion);

            Assert.AreEqual(msg1.Folders.Length, msg2.Folders.Length);
            for (int i = 0; i < msg1.Folders.Length; i++)
                AssertFolder(msg1.Folders[i], msg2.Folders[i]);

            Assert.AreEqual(msg1.Options.Length, msg2.Options.Length);
            for (int i = 0; i < msg1.Options.Length; i++)
                AssertPtopertiesAreEqual(msg1.Options[i], msg2.Options[i]);
        }

        [Test]
        public void FileInfoTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            FileInfo msg1 = GetFileInfo(7);

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            FileInfo msg2 = new FileInfo();

            msg2.DecodeXdr(r);

            AssertFileInfo(msg1, msg2);
        }

        [Test]
        public void FileInfoTruncatedTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            FileInfoTruncated msg1 = GetFileTruncatedInfo();

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            FileInfoTruncated msg2 = new FileInfoTruncated();

            msg2.DecodeXdr(r);

            AssertBaseFileInfo(msg1, msg2);
            Assert.AreEqual(msg1.NumBlocks, msg2.NumBlocks);
        }

        [Test]
        public void IndexMessageTest()
        {
            var localStream = GetStream();

            XdrWriter w = new XdrWriter(localStream);
            IndexMessage msg1 = new IndexMessage
            {
                Folder = @"this is a acceptable message \\\///",
                Files = new []
                {
                    GetFileInfo(4),
                    GetFileInfo(1),
                    GetFileInfo(9)
                }
            };

            msg1.EncodeXdr(w);

            // Put the localStream to start
            localStream.Position = 0;

            XdrReader r = new XdrReader(localStream);
            IndexMessage msg2 = new IndexMessage();

            msg2.DecodeXdr(r);


            Assert.AreEqual(msg1.Folder, msg2.Folder);
            Assert.AreEqual(msg1.Files.Length, msg2.Files.Length);
            for (int i = 0; i < msg1.Files.Length; i++)
                AssertFileInfo(msg1.Files[i], msg2.Files[i]);
        }
    }
}

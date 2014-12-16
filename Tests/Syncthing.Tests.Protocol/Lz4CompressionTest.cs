using System;
using System.Text;
using LZ4;
using NUnit.Framework;
using Syncthing.Protocol.Utils;

namespace Syncthing.Tests.Protocol
{
    [TestFixture]
    public class Lz4CompressionTest
    {
        private const string TextForTest =
            "This is a simple text, use it to make round trip compression with Lz4 algorithm.";


        [Test()]
        public void Lz4Peek4Test()
        {
            byte[] raw = Encoding.UTF8.GetBytes(TextForTest);
            byte[] compressed = Lz4Compression.Compress(raw);

            uint rawLength = (uint)raw.Length;
            uint rawLengthFromcompressedData = Lz4Compression.Peek4(compressed, 0);

            Assert.AreEqual(rawLength, rawLengthFromcompressedData, "Unable to peek the length of the raw data from the compressed data.");
        }

        [Test()]
        public void Lz4RoundTripTest()
        {
            byte[] raw = Encoding.UTF8.GetBytes(TextForTest);

            byte[] compressed = Lz4Compression.Compress(raw);
            byte[] uncompressed = Lz4Compression.Decompress(compressed);
            string roundTripStr = Encoding.UTF8.GetString(uncompressed);
            Assert.AreEqual(TextForTest, roundTripStr, "Lz4 round trip not working properly.");

        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Lz4CompressionErrorTest()
        {
            byte[] raw = new byte[0];
            Lz4Compression.Compress(raw);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Lz4DecompressionErrorTest()
        {
            byte[] compressed = null;
            Lz4Compression.Decompress(compressed);
        }

        [Test()]
        [ExpectedException(typeof(Lz4Compression.DataTooLargeException))]
        public void Lz4CompressionLimitTest()
        {
            const int size = Lz4Compression.MaxBufferLength + 1;
            byte[] raw = new byte[size];

            Lz4Compression.Compress(raw);
        }

        [Test()]
        [ExpectedException(typeof(Lz4Compression.DataTooLargeException))]
        public void Lz4DecompressionLimitTest()
        {
            const int size = Lz4Compression.MaxBufferLength + 1;
            byte[] raw = new byte[size];
            byte[] compressed = LZ4Codec.Wrap(raw, 0, raw.Length);

            Lz4Compression.Decompress(compressed);
        }
    }
}

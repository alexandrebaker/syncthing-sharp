using System;
using LZ4;

namespace Syncthing.Protocol.Utils
{
    /// <summary>
    /// Handle LZ4 compression and decompression
    /// </summary>
    public class Lz4Compression
    {
        private const int MaxBufferLength = 1024 * 1024 * 8;

        /// <summary>
        /// Compress data to LZ4/
        /// </summary>
        /// <param name="uncompressed"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] uncompressed)
        {
            if (uncompressed == null || uncompressed.Length == 0)
                throw new ArgumentException("uncompressed data is null or size is 0");

            return LZ4Codec.Wrap(uncompressed, 0, uncompressed.Length);
        }

        /// <summary>
        /// Decompress LZ4 data/
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            long decompressedLength = Peek4(data, 0);
            if (decompressedLength > MaxBufferLength)
                throw new DecompressedDataTooLargeException();

            try
            {
                return LZ4Codec.Unwrap(data);
            }
            catch (Exception exception)
            {
                throw new InvalidLz4DataExcetion(exception);
            }
        }

        /// <summary>
        /// Peek the 4 first bytes/
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static uint Peek4(byte[] buffer, int offset)
        {
            return (uint)((int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24);
        }

        /// <summary>
        /// Exception if the data is bigger than the alowed buffer/
        /// </summary>
        public class DecompressedDataTooLargeException : Exception
        {
            public DecompressedDataTooLargeException() : base("Decompressed data exceeds maximum size") { }
        }

        /// <summary>
        /// Exception to wrap around any error enconter during LZ4 decompression.
        /// </summary>
        public class InvalidLz4DataExcetion : Exception
        {
            public InvalidLz4DataExcetion(Exception inner) : base("Decompressed data exceeds maximum size", inner) { }
        }
    }
}

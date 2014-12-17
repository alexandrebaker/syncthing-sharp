using System;
using LZ4;

namespace Syncthing.Protocol.v1.Utils
{
    /// <summary>
    /// Handle LZ4 compression and decompression
    /// </summary>
    public class Lz4Compression
    {
        /// <summary>
        /// The length of the max buffer.
        /// </summary>
        public const int MaxBufferLength = 1024 * 1024 * 8;

        /// <summary>
        /// Compress the specified uncompressed data into Lz4.
        /// </summary>
        /// <param name="uncompressed">Uncompressed.</param>
        public static byte[] Compress(byte[] uncompressed)
        {
            if (uncompressed == null || uncompressed.Length == 0)
                throw new ArgumentException("Uncompressed data is null or size is 0.");

            if (uncompressed.Length > MaxBufferLength)
                throw new DataTooLargeException();

            return LZ4Codec.Wrap(uncompressed, 0, uncompressed.Length);
        }

        /// <summary>
        /// Decompress the specified compressed bytes.
        /// </summary>
        /// <param name="compressed">Compressed.</param>
        public static byte[] Decompress(byte[] compressed)
        {
            if (compressed == null || compressed.Length == 0)
                throw new ArgumentException("Compressed data is null or size is 0.");

            long decompressedLength = Peek4(compressed, 0);

            if (decompressedLength > MaxBufferLength)
                throw new DataTooLargeException();

            try
            {
                return LZ4Codec.Unwrap(compressed);
            }
            catch (Exception exception)
            {
                throw new InvalidLz4DataExcetion(exception);
            }
        }

        /// <summary>
        /// Peek the 4 first bytes.
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        /// <param name="offset">Offset.</param>
        public static uint Peek4(byte[] buffer, int offset)
        {
            return (uint)(buffer[offset] |
            buffer[offset + 1] << 8 |
            buffer[offset + 2] << 16 |
            buffer[offset + 3] << 24);
        }

        /// <summary>
        /// Exception if the data is bigger than the alowed buffer.
        /// </summary>
        public class DataTooLargeException : Exception
        {
            public DataTooLargeException()
                : base("Data exceeds maximum size")
            {
            }
        }

        /// <summary>
        /// Exception to wrap around any error enconter during LZ4 decompression.
        /// </summary>
        public class InvalidLz4DataExcetion : Exception
        {
            public InvalidLz4DataExcetion(Exception inner)
                : base("Error occur during Lz4 decompression process.", inner)
            {
            }
        }
    }
}

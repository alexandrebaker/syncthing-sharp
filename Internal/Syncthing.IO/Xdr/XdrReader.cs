using System;
using System.IO;
using System.Text;

namespace Syncthing.IO.Xdr
{
    /// <summary>
    /// Xdr reader.
    /// </summary>
    public class XdrReader
    {
        /// <summary>
        /// Gets or sets the inner reader.
        /// </summary>
        /// <value>The inner reader.</value>
        private BinaryReader InnerReader { get; set; }


        public XdrReader(Stream reader)
        {
            InnerReader = new BinaryReader(reader);
        }

        /// <summary>
        /// Reads the raw bytes.
        /// </summary>
        /// <returns>The raw bytes.</returns>
        public byte[] ReadRaw()
        {
            int length = (int)ReadUInt();
            byte[] utf8Bytes = new byte[length];
            utf8Bytes = InnerReader.ReadBytes(length);

            int amountOfPadding = 4 - (length % 4);
            for (int i = 0; i < amountOfPadding; i++)
            {
                InnerReader.ReadByte();
            }

            return utf8Bytes;
        }

        /// <summary>
        /// Reads a string.
        /// </summary>
        /// <returns>The string.</returns>
        public string ReadString()
        {
            return Encoding.UTF8.GetString(ReadRaw());
        }

        /// <summary>
        /// Reads a int.
        /// </summary>
        /// <returns>The int.</returns>
        public int ReadInt()
        {
            var b = InnerReader.ReadBytes(4);
            return BitConverter.ToInt32(b, 0);
        }

        /// <summary>
        /// Reads a uint.
        /// </summary>
        /// <returns>The uint.</returns>
        public uint ReadUInt()
        {
            var b = InnerReader.ReadBytes(4);
            return BitConverter.ToUInt32(b, 0);
        }

        /// <summary>
        /// Reads a int64.
        /// </summary>
        /// <returns>The int64.</returns>
        public long ReadInt64()
        {
            var b = InnerReader.ReadBytes(8);
            return BitConverter.ToInt64(b, 0);
        }

        /// <summary>
        /// Reads a uint64.
        /// </summary>
        /// <returns>The uint64.</returns>
        public ulong ReadUInt64()
        {
            var b = InnerReader.ReadBytes(8);
            return BitConverter.ToUInt64(b, 0);
        }

        /// <summary>
        /// Reads a bool.
        /// </summary>
        /// <returns>The bool.</returns>
        public bool ReadBool()
        {
            var b = InnerReader.ReadBytes(1);
            return BitConverter.ToBoolean(b, 0);
        }

        /// <summary>
        /// Reads a int16.
        /// </summary>
        /// <returns>The int16.</returns>
        public short ReadInt16()
        {
            var b = InnerReader.ReadBytes(2);
            return BitConverter.ToInt16(b, 0);
        }

        /// <summary>
        /// Reads a uint16.
        /// </summary>
        /// <returns>The uint16.</returns>
        public ushort ReadUInt16()
        {
            var b = InnerReader.ReadBytes(2);
            return BitConverter.ToUInt16(b, 0);
        }

        /// <summary>
        /// Reads a int16.
        /// </summary>
        /// <returns>The int16.</returns>
        public byte ReadByte()
        {
            var b = InnerReader.ReadByte();
            return b;
        }

        #region IDisposable implementation

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}


using System;
using System.IO;
using System.Text;

namespace Syncthing.IO.Xdr
{
    /// <summary>
    /// Xdr writer.
    /// </summary>
    public sealed class XdrWriter : IDisposable
    {
        /// <summary>
        /// Gets or sets the inner writer.
        /// </summary>
        /// <value>The inner writer.</value>
        private BinaryWriter InnerWriter { get; set; }

        public int Total { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XdrWriter"/> class.
        /// </summary>
        /// <param name="innerWriter">Inner writer.</param>
        public XdrWriter(Stream innerWriter)
        {
            InnerWriter = new BinaryWriter(innerWriter);
        }

        /// <summary>
        /// Writes the raw bytes.
        /// </summary>
        /// <returns>The raw.</returns>
        /// <param name="bs">Bs.</param>
        public void WriteRaw(byte[] bs)
        {
            InnerWriter.Write(bs, 0, bs.Length);
        }

        /// <summary>
        /// Writes a string.
        /// </summary>
        /// <param name="s">S.</param>
        public void WriteString(string s)
        {
            WriteBytes(Encoding.UTF8.GetBytes(s));
        }

        /// <summary>
        /// Writes a bytes array.
        /// </summary>
        /// <param name="bs">Bs.</param>
        public void WriteBytes(byte[] bs)
        {
            // Length of the bytes sequence
            WriteUInt((uint)bs.Length);

            InnerWriter.Write(bs, 0, bs.Length);
            int amountOfPadding = 4 - (bs.Length % 4);
            for (int i = 0; i < amountOfPadding; i++)
                InnerWriter.Write((byte)0);
        }

        /// <summary>
        /// Writes a uint.
        /// </summary>
        /// <returns>The uint.</returns>
        /// <param name="v">V.</param>
        public void WriteUInt(uint v)
        {
            var b = BitConverter.GetBytes(v);
            WriteRaw(b);
        }

        /// <summary>
        /// Writes a int.
        /// </summary>
        /// <returns>The int.</returns>
        /// <param name="v">V.</param>
        public void WriteInt(int v)
        {
            var b = BitConverter.GetBytes(v);
            WriteRaw(b);
        }

        /// <summary>
        /// Writes a int64.
        /// </summary>
        /// <param name="v">V.</param>
        public void WriteInt64(long v)
        {
            var b = BitConverter.GetBytes(v);
            WriteRaw(b);
        }

        /// <summary>
        /// Writes a uint64.
        /// </summary>
        /// <param name="v">V.</param>
        public void WriteUInt64(ulong v)
        {
            var b = BitConverter.GetBytes(v);
            WriteRaw(b);
        }

        /// <summary>
        /// Writes a uint16.
        /// </summary>
        /// <param name="v">V.</param>
        public void WriteUInt16(ushort v)
        {
            var b = BitConverter.GetBytes(v);
            WriteRaw(b);
        }

        /// <summary>
        /// Writes a int16.
        /// </summary>
        /// <param name="v">V.</param>
        public void WriteInt16(short v)
        {
            var b = BitConverter.GetBytes(v);
            WriteRaw(b);
        }

        /// <summary>
        /// Writes a byte.
        /// </summary>
        /// <param name="v">V.</param>
        public void WriteByte(byte v)
        {
            InnerWriter.Write(v);
        }

        #region IDisposable implementation

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}


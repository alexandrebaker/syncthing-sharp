using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.IO.Xdr
{
    /// <summary>
    /// Marshalable.
    /// </summary>
    public abstract class Marshalable : IXdrEncodable, IXdrDecodable
    {
        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public abstract void EncodeXdr([In, Out] XdrWriter writer);

        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public abstract void DecodeXdr([In, Out] XdrReader reader);

        /// <summary>
        /// Appends the xdr.
        /// </summary>
        /// <returns>The xdr.</returns>
        /// <param name="bs">Bs.</param>
        public byte[] AppendXdr(byte[] bs)
        {
            Stream s = new MemoryStream(bs, true);
            var w = new XdrWriter(s);
            this.EncodeXdr(w);
            return bs;
        }

        /// <summary>
        /// Marshals the xdr.
        /// </summary>
        /// <returns>The xdr.</returns>
        public byte[] MarshalXdr()
        {
            return this.AppendXdr(new byte[128]);
        }

        /// <summary>
        /// Unmarshals the xdr.
        /// </summary>
        /// <param name="bs">Bs.</param>
        public void UnmarshalXdr(byte[] bs)
        {
            Stream s = new MemoryStream(bs, true);
            var r = new XdrReader(s);
            this.DecodeXdr(r);
        }

    }
}

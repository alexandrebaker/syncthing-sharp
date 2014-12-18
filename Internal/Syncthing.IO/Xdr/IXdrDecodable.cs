using System;
using System.Runtime.InteropServices;

namespace Syncthing.IO.Xdr
{
    /// <summary>
    /// IXdrDecodable definition.
    /// </summary>
    public interface IXdrDecodable
    {
        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        void DecodeXdr([In, Out]XdrReader reader);
    }
}


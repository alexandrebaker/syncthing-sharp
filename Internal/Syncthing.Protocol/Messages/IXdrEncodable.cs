using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;

namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// IXdrEncodable definition.
    /// </summary>
    public interface IXdrEncodable
    {
        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        void EncodeXdr([In, Out]XdrWriter writer);
    }
}

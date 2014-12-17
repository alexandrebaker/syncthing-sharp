using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;


namespace Syncthing.Protocol.v1.Messages
{
    /*
    EmptyMessage Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

    */

    /// <summary>
    /// Empty message.
    /// </summary>
    public class EmptyMessage : IXdrEncodable
    {
        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In, Out]XdrWriter writer)
        {
            // Empty Message don't write anything into the writer.
        }
    }
}

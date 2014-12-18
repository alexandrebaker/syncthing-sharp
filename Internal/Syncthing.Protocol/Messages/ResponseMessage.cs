using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;


namespace Syncthing.Protocol.v1.Messages
{
    /*
    ResponseMessage Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                        Length of Data                         |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                    Data (variable length)                     \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

    */

    /// <summary>
    /// Response message.
    /// </summary>
    public class ResponseMessage : IXdrEncodable, IXdrDecodable
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public byte[] Data { get; set; }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In,Out]XdrWriter writer)
        {
            writer.WriteBytes(Data);
        }

        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public void DecodeXdr([In, Out] XdrReader reader)
        {
            this.Data = reader.ReadBytes();
        }
    }
}

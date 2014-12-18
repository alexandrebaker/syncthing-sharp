using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;

namespace Syncthing.Protocol.v1.Messages
{
    /*
    BlockInfo Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                             Size                              |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                        Length of Hash                         |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                    Hash (variable length)                     \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
 
    */

    /// <summary>
    /// Block info.
    /// </summary>
    public class BlockInfo : IXdrEncodable, IXdrDecodable
    {
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public uint Size { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance hash.
        /// </summary>
        /// <value><c>true</c> if this instance hash; otherwise, <c>false</c>.</value>
        [MaxLength(64)]
        public byte[] Hash { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Block[{0}|{1}|{2}]", Offset, Size, Hash);
        }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <returns>The xdr.</returns>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In, Out]XdrWriter writer)
        {
            this.ValidateLength();

            writer.WriteUInt(Size);
            writer.WriteBytes(Hash);
        }

        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public void DecodeXdr([In, Out] XdrReader reader)
        {
            this.Size = reader.ReadUInt();
            this.Hash = reader.ReadBytesMax(64);
        }
    }
}

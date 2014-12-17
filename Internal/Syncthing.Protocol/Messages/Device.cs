using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;


namespace Syncthing.Protocol.v1.Messages
{
    /*
    Device Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                         Length of ID                          |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                     ID (variable length)                      \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                             Flags                             |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                                                               |
    +                  Max Local Version (64 bits)                  +
    |                                                               |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

    */
    /// <summary>
    /// Device.
    /// </summary>
    public class Device : IXdrEncodable
    {
        /// <summary>
        /// Gets or sets the I.
        /// </summary>
        /// <value>The I.</value>
        [MaxLength(32)]
        public byte[] ID { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        /// <value>The flags.</value>
        public uint Flags { get; set; }

        /// <summary>
        /// Gets or sets the max local version.
        /// </summary>
        /// <value>The max local version.</value>
        public ulong MaxLocalVersion { get; set; }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In, Out]XdrWriter writer)
        {
            this.ValidateLength();

            writer.WriteBytes(ID);
            writer.WriteUInt(Flags);
            writer.WriteUInt64(MaxLocalVersion);
        }
            
    }
}

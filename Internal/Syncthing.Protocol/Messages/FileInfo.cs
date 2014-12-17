using System.Linq;
using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;

namespace Syncthing.Protocol.v1.Messages
{
    /*

    FileInfo Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                        Length of Name                         |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                    Name (variable length)                     \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                             Flags                             |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                                                               |
    +                      Modified (64 bits)                       +
    |                                                               |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                                                               |
    +                       Version (64 bits)                       +
    |                                                               |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                                                               |
    +                    Local Version (64 bits)                    +
    |                                                               |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                       Number of Blocks                        |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \               Zero or more BlockInfo Structures               \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

    */

    /// <summary>
    /// File info.
    /// </summary>
    public class FileInfo : BaseFileInfo, IXdrEncodable
    {
        /// <summary>
        /// Gets or sets the blocks.
        /// </summary>
        /// <value>The blocks.</value>
        public BlockInfo[] Blocks { get; set; }

        /// <summary>
        /// Size this instance.
        /// </summary>
        public override long Size()
        {
            if (IsDeleted() || IsDirectory())
                return 128;

            return Blocks.Aggregate<BlockInfo, long>(0, (current, b) => current + b.Size);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("File(Name:{0}, Flags:0{1}, Modified:{2}, Version:{3}, Size:{4}, Blocks:{5})", Name,
                Flags, Modified, Version, Size(), Blocks);
        }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In, Out]XdrWriter writer)
        {
            this.ValidateLength();

            writer.WriteString(Name);
            writer.WriteUInt(Flags);
            writer.WriteUInt64((ulong)Modified);
            writer.WriteUInt64(Version);
            writer.WriteUInt64(LocalVersion);
            writer.WriteUInt((uint)Blocks.Length);

            foreach (var b in Blocks)
                b.EncodeXdr(writer);
        }
    }
}

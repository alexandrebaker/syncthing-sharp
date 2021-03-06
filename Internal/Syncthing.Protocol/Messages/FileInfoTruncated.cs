﻿using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;


namespace Syncthing.Protocol.v1.Messages
{
    /*
    FileInfoTruncated Structure:
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
    |                          Num Blocks                           |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
 
    */

    /// <summary>
    /// File info truncated.
    /// </summary>
    public class FileInfoTruncated : BaseFileInfo
    {
        /// <summary>
        /// Gets or sets the number blocks.
        /// </summary>
        /// <value>The number blocks.</value>
        public uint NumBlocks { get; set; }

        /// <summary>
        /// Size this instance.
        /// </summary>
        public override long Size()
        {
            if (IsDeleted() || IsDirectory())
                return 128;

            return BlocksToSize(NumBlocks);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("File(Name:{0}, Flags:0{1}, Modified:{2}, Version:{3}, Size:{4}, NumBlocks:{5})", Name,
                Flags, Modified, Version, Size(), NumBlocks);
        }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public override void EncodeXdr([In, Out]XdrWriter writer)
        {
            this.ValidateLength();

            writer.WriteString(Name);
            writer.WriteUInt(Flags);
            writer.WriteUInt64((ulong)Modified);
            writer.WriteUInt64(Version);
            writer.WriteUInt64(LocalVersion);
            writer.WriteUInt(NumBlocks);
        }

        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public override void DecodeXdr([In, Out] XdrReader reader)
        {
            this.Name = reader.ReadStringMax(8192);
            this.Flags = reader.ReadUInt();
            this.Modified = (long)reader.ReadUInt64();
            this.Version = reader.ReadUInt64();
            this.LocalVersion = reader.ReadUInt64();
            this.NumBlocks = reader.ReadUInt();
        }
    }
}

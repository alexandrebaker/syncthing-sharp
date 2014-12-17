using System.IO;
using Syncthing.Protocol.Messages;
using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;

namespace Syncthing.Protocol.Messages
{
    /*
       
    IndexMessage Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                       Length of Folder                        |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                   Folder (variable length)                    \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                        Number of Files                        |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \               Zero or more FileInfo Structures                \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

    */

    /// <summary>
    /// Index message.
    /// </summary>
    public class IndexMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the folder.
        /// </summary>
        /// <value>The folder.</value>
        [MaxLength(64)]
        public string Folder { get; set; }
        // Max 64 char.

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public FileInfo[] Files { get; set; }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <returns>The xdr.</returns>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In, Out] XdrWriter writer)
        {
            this.ValidateLength();               
    
            writer.WriteString(Folder);
            writer.WriteUInt((uint)Files.Length);
            foreach (var f in Files)
            {
                f.EncodeXdr(writer);
            }
        }
    }
}

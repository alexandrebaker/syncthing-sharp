using System.IO;
using Syncthing.Protocol.Messages;

namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Index message.
    /// </summary>
    public class IndexMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the folder.
        /// </summary>
        /// <value>The folder.</value>
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
        public int EncodeXdr(ref Stream writer)
        {
            return 0;

        }
    }
}

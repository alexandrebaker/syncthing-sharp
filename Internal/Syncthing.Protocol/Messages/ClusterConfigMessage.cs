using System.Linq;
using System.Runtime.InteropServices;
using Syncthing.IO.Xdr;
using Syncthing.Protocol.v1.Messages;

namespace Syncthing.Protocol.v1
{
    /*
    ClusterConfigMessage Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                     Length of Client Name                     |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                 Client Name (variable length)                 \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                   Length of Client Version                    |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \               Client Version (variable length)                \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                       Number of Folders                       |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                Zero or more Folder Structures                 \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                       Number of Options                       |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                Zero or more Option Structures                 \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

    */
    /// <summary>
    /// Cluster config message.
    /// </summary>
    public class ClusterConfigMessage : IXdrEncodable, IXdrDecodable
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        [MaxLength(64)]
        public string ClientName { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the client version.
        /// </summary>
        /// <value>The client version.</value>
        [MaxLength(64)]
        public string ClientVersion { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        /// <value>The folders.</value>
        [MaxLength(64)]
        public Folder[] Folders { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        [MaxLength(64)]
        public Option[] Options { get; set; }
        // max:64

        /// <summary>
        /// Gets the option.
        /// </summary>
        /// <returns>The option.</returns>
        /// <param name="key">Key.</param>
        public string GetOption(string key)
        {
            foreach (var option in Options.Where(option => option.Key == key))
                return option.Value;
            return "";
        }

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In,Out]XdrWriter writer)
        {
            this.ValidateLength();
           
            writer.WriteString(ClientName);
            writer.WriteString(ClientVersion);

            writer.WriteUInt((uint)Folders.Length);
            foreach (var f in Folders)
                f.EncodeXdr(writer);

            writer.WriteUInt((uint)Options.Length);
            foreach (var o in Options)
                o.EncodeXdr(writer);
        }

        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public void DecodeXdr([In, Out] XdrReader reader)
        {
            this.ClientName = reader.ReadStringMax(64);
            this.ClientVersion = reader.ReadStringMax(64);

            // Folders
            int foldersSize = (int)reader.ReadUInt();
            if (foldersSize > 64)
                throw new XdrElementSizeExceeded("Folders", foldersSize, 64);

            this.Folders = new Folder[foldersSize];
            foreach (var f in Folders)
                f.DecodeXdr(reader);
    
            // Options
            int optionsSize = (int)reader.ReadUInt();
            if (optionsSize > 64)
                throw new XdrElementSizeExceeded("Options", optionsSize, 64);

            this.Options = new Option[optionsSize];
            foreach (var o in Options)
                o.DecodeXdr(reader);
        }
    }
}

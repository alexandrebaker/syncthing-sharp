using System.Linq;
using System.Runtime.InteropServices;
using Syncthing.IO.Xdr;

namespace Syncthing.Protocol.Messages
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
    public class ClusterConfigMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the client version.
        /// </summary>
        /// <value>The client version.</value>
        public string ClientVersion { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        /// <value>The folders.</value>
        public Folder[] Folders { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
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
    }
}

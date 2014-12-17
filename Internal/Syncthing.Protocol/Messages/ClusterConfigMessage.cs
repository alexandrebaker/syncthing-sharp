using System.Linq;

namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Cluster config message.
    /// </summary>
    public class ClusterConfigMessage //: IMessage
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
    }
}

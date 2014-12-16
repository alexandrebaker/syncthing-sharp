
namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Folder.
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// Gets or sets the I.
        /// </summary>
        /// <value>The I.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>The devices.</value>
        public Device[] Devices { get; set; }
    }
}

using Syncthing.Protocol.v1.Messages;

namespace Syncthing.Protocol.v1
{
    /// <summary>
    /// Connection.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public DeviceId Id { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.v1.Connection"/> class.
        /// </summary>
        /// <param name="deviceId">Device identifier.</param>
        /// <param name="name">Name.</param>
        public Connection(DeviceId deviceId, string name)
        {
            Id = deviceId;
            Name = name;

        }

        /// <summary>
        /// Index the specified folder and files.
        /// </summary>
        /// <param name="folder">Folder.</param>
        /// <param name="files">Files.</param>
        public void Index(string folder, FileInfo[] files)
        {
        }

        /// <summary>
        /// Indexs the update.
        /// </summary>
        /// <param name="folder">Folder.</param>
        /// <param name="files">Files.</param>
        public void IndexUpdate(string folder, FileInfo[] files)
        {
        }

        /// <summary>
        /// Request the specified folder, name, offset and size.
        /// </summary>
        /// <param name="folder">Folder.</param>
        /// <param name="name">Name.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="size">Size.</param>
        public byte[] Request(string folder, string name, long offset, int size)
        {
        }

        /// <summary>
        /// Custers the config.
        /// </summary>
        /// <param name="config">Config.</param>
        public void CusterConfig(ClusterConfigMessage config)
        {
        }

        /// <summary>
        /// Statistics this instance.
        /// </summary>
        public Statistics Statistics()
        {
        }
    }
}

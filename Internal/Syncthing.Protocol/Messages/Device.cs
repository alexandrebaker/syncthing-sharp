
namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Device.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the I.
        /// </summary>
        /// <value>The I.</value>
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
    }
}

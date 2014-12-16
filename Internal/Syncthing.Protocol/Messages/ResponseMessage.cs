
namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Response message.
    /// </summary>
    public class ResponseMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public byte[] Data { get; set; }
    }
}

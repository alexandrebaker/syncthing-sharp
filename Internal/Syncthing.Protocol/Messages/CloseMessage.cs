
namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Close message.
    /// </summary>
    public class CloseMessage// : IMessage
    {
        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason { get; set; }
    }
}


namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Block info.
    /// </summary>
    public class BlockInfo
    {
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public uint Size { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance hash.
        /// </summary>
        /// <value><c>true</c> if this instance hash; otherwise, <c>false</c>.</value>
        public byte[] Hash { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Block[{0}|{1}|{2}]", Offset, Size, Hash);
        }
    }
}

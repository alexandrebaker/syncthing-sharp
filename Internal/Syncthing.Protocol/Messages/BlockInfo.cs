
namespace Syncthing.Protocol.Messages
{
    public class BlockInfo
    {
        public long Offset { get; set; }
        public uint Size { get; set; }
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

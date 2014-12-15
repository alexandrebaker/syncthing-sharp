
namespace Syncthing.Protocol.Messages
{
    public class FileInfoTruncated : BaseFileInfo
    {
        public uint NumBlocks { get; set; }

        public override long Size()
        {
            if (IsDeleted() || IsDirectory())
                return 128;

            return BlocksToSize(NumBlocks);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("File(Name:{0}, Flags:0{1}, Modified:{2}, Version:{3}, Size:{4}, NumBlocks:{5})", Name,
                Flags, Modified, Version, Size(), NumBlocks);
        }
    }
}

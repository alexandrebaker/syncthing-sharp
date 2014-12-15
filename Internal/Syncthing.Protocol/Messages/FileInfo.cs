using System.Linq;

namespace Syncthing.Protocol.Messages
{
    public class FileInfo : BaseFileInfo
    {
        public BlockInfo[] Blocks { get; set; }

        public override long Size()
        {
            if (IsDeleted() || IsDirectory())
                return 128;

            return Blocks.Aggregate<BlockInfo, long>(0, (current, b) => current + b.Size);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("File(Name:{0}, Flags:0{1}, Modified:{2}, Version:{3}, Size:{4}, Blocks:{5})", Name,
                Flags, Modified, Version, Size(), Blocks);
        }
        
    }
}


using Syncthing.IO.Xdr;

namespace Syncthing.Protocol.v1.Messages
{
    /// <summary>
    /// Base file info.
    /// </summary>
    public abstract partial class BaseFileInfo : Marshalable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [MaxLength(8192)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        /// <value>The flags.</value>
        public uint Flags { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>The modified.</value>
        public long Modified { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public ulong Version { get; set; }

        /// <summary>
        /// Gets or sets the local version.
        /// </summary>
        /// <value>The local version.</value>
        public ulong LocalVersion { get; set; }

        /// <summary>
        /// Determines whether this instance is deleted.
        /// </summary>
        /// <returns><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</returns>
        public bool IsDeleted()
        {
            return (Flags & Deleted) != 0;
        }

        /// <summary>
        /// Determines whether this instance is directory.
        /// </summary>
        /// <returns><c>true</c> if this instance is directory; otherwise, <c>false</c>.</returns>
        public bool IsDirectory()
        {
            return (Flags & Directory) != 0;
        }

        /// <summary>
        /// Determines whether this instance is invalid.
        /// </summary>
        /// <returns><c>true</c> if this instance is invalid; otherwise, <c>false</c>.</returns>
        public bool IsInvalid()
        {
            return (Flags & Invalid) != 0;
        }

        /// <summary>
        /// Determines whether this instance is symlink.
        /// </summary>
        /// <returns><c>true</c> if this instance is symlink; otherwise, <c>false</c>.</returns>
        public bool IsSymlink()
        {
            return (Flags & Symlink) != 0;
        }

        /// <summary>
        /// Determines whether this instance has permission bits.
        /// </summary>
        /// <returns><c>true</c> if this instance has permission bits; otherwise, <c>false</c>.</returns>
        public bool HasPermissionBits()
        {
            return (Flags & NoPermBits) == 0;
        }

        /// <summary>
        /// Blocks to size.
        /// </summary>
        /// <returns>The to size.</returns>
        /// <param name="num">Number.</param>
        public long BlocksToSize(uint num)
        {
            if (num < 2)
                return BlockSize / 2;

            return (num - 1) * BlockSize + BlockSize / 2;
        }

        /// <summary>
        /// Size this instance.
        /// </summary>
        public abstract long Size();
    }
}

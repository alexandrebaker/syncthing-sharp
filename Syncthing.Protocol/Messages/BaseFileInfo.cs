
namespace Syncthing.Protocol.Messages
{
    public abstract partial class BaseFileInfo
    {
        public string Name { get; set; }

        public uint Flags { get; set; }

        public long Modified { get; set; }

        public ulong Version { get; set; }

        public ulong LocalVersion { get; set; }

        public bool IsDeleted()
        {
            return (Flags & Deleted) != 0;
        }

        public bool IsDirectory()
        {
            return (Flags & Directory) != 0;
        }

        public bool IsInvalid()
        {
            return (Flags & Invalid) != 0;
        }

        public bool IsSymlink()
        {
            return (Flags & Symlink) != 0;
        }

        public bool HasPermissionBits()
        {
            return (Flags & NoPermBits) == 0;
        }

        public long BlocksToSize(uint num)
        {
            if (num < 2)
                return BlockSize / 2;

            return (num - 1) * BlockSize + BlockSize / 2;
        }

        public abstract long Size();
    }
}

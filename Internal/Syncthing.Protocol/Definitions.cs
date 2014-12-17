using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.v1
{
    /// <summary>
    /// Message type.
    /// </summary>
    public enum MessageType
    {
        ClusterConfig = 0,
        TypeIndex = 1,
        TypeRequest = 2,
        TypeResponse = 3,
        TypePing = 4,
        TypePong = 5,
        TypeIndexUpdate = 6,
        TypeClose = 7
    }

    /// <summary>
    /// State.
    /// </summary>
    public enum State
    {
        Initial = 1,
        CCRcvd = 2,
        IdxRcvd = 3
    }

    /// <summary>
    /// Communication flags.
    /// </summary>
    [Flags]
    public enum CommunicationFlags : uint
    {
        ShareTrusted = 1 << 0,
        ShareReadOnly = 1 << 1,
        Introducer = 1 << 2,
        ShareBits = 0x000000ff,
    }
}

namespace Syncthing.Protocol.v1.Messages
{
    /// <summary>
    /// Base file info.
    /// </summary>
    public abstract partial class BaseFileInfo
    {
        public const long BlockSize = 128 * 1024;
  
        public const uint Deleted = 1 << 12;
        public const uint Invalid = 1 << 13;
        public const uint Directory = 1 << 14;
        public const uint NoPermBits = 1 << 15;
        public const uint Symlink = 1 << 16;
        public const uint SymlinkMissingTarget = 1 << 17;
        public const uint SymlinkTypeMask = Directory | SymlinkMissingTarget;
    }
}

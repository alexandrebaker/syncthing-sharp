using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol
{
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

    public enum State
    {
        Initial = 1,
        CCRcvd = 2,
        IdxRcvd = 3
    }

    [Flags]
    public enum Asdf
    {
        ShareTrusted = 1 << 0,
        ShareReadOnly = 1 << 1,
        Introducer = 1 << 2,
        ShareBits = 0x000000ff,

        Deleted = 1 << 12,
        Invalid = 1 << 13,
        Directory = 1 << 14,
        NoPermBits = 1 << 15,
    }
}

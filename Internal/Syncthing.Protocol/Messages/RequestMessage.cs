using System;

namespace Syncthing.Protocol.Messages
{
    public class RequestMessage : IMessage
    {
        public string Folder { get; set; } // Max 64 char.

        public string Name { get; set; }

        public ulong Offset { get; set; }

        public uint Size { get; set; }
    }
}

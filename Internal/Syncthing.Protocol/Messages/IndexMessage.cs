using Syncthing.Protocol.Messages;

namespace Syncthing.Protocol.Messages
{
    public class IndexMessage : IMessage
    {
        public string Folder { get; set; } // Max 64 char.

        public FileInfo[] Files { get; set; }
    }
}

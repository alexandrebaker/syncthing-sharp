using System;
using System.Threading.Tasks;

namespace Syncthing.Net
{
    public interface IBroadcast
    {
        bool Send(BroadcastRequest request);
        BroadcastResponse Recieve();
    }
}


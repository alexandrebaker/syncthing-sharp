using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Net
{
    public class Broadcast : IBroadcast
    {
        const int GroupPort = 15000;

        public Broadcast()
        {
        }

        public bool Send(BroadcastRequest request)
        {
            throw new NotImplementedException();
        }

        public BroadcastResponse Recieve()
        {
            throw new NotImplementedException();
        }
            
    }
}


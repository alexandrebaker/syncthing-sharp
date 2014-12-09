using System;
using System.Net;

namespace Syncthing.Net
{
    public class BroadcastRequest
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public string Message { get; set; }

    }
}


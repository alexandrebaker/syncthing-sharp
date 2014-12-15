using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.Messages
{
    public class ResponseMessage : IMessage
    {
        public byte[] Data { get; set; }
    }
}

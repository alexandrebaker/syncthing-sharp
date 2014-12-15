using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.Messages
{
    public class CloseMessage : IMessage
    {
        public string Reason { get; set; }
    }
}

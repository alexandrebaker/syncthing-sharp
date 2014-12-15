using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.Messages
{
    public class Device
    {
        public byte[] ID { get; set; }

        public uint Flags { get; set; }

        public ulong MaxLocalVersion { get; set; }
    }
}

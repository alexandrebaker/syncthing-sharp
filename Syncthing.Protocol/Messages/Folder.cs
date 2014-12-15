using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.Messages
{
    public class Folder
    {
        public string ID { get; set; }

        public Device[] Devices { get; set; }
     }
}

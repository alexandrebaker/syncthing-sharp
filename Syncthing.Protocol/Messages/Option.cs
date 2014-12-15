using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.Messages
{
    public class Option
    {
        public string Key { get; set; } // max:64
        public string Value { get; set; } //max:1024
    }
}

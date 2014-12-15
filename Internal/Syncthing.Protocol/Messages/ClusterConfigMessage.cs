using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.Messages
{
    public class ClusterConfigMessage : IMessage
    {
        public string ClientName { get; set; } // max:64

        public string ClientVersion { get; set; } // max:64

        public Folder[] Folders { get; set; } // max:64

        public Option[] Options { get; set; } // max:64


        public string GetOption(string key)
        {
            foreach (var option in Options.Where(option => option.Key == key))
                return option.Value;
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncthing.Protocol.v1
{
    /// <summary>
    /// Statistics.
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// Gets Time At.
        /// </summary>
        /// <value>At.</value>
        public DateTime At { get; private set; }

        /// <summary>
        /// Gets the in bytes total.
        /// </summary>
        /// <value>The in bytes total.</value>
        public ulong InBytesTotal { get; private set; }

        /// <summary>
        /// Gets the out bytes total.
        /// </summary>
        /// <value>The out bytes total.</value>
        public ulong OutBytesTotal { get; private set; }
    }
}

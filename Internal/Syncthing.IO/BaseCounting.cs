using System;
using System.Threading;

namespace Syncthing.IO
{
    /// <summary>
    /// Base counting.
    /// </summary>
    public abstract class BaseCounting
    {
        /// <summary>
        /// The total incoming bytes.
        /// </summary>
        protected int totalIncoming;

        /// <summary>
        /// The total outcoming bytes.
        /// </summary>
        protected int totalOutcoming;

        /// <summary>
        /// The total bytes.
        /// </summary>
        protected int total;

        /// <summary>
        /// Gets the total bytes.
        /// </summary>
        /// <value>The total.</value>
        public int Total
        {
            get
            {
                int val = Interlocked.CompareExchange(ref total, 0, 0);
                return val;
            }
        }

        /// <summary>
        /// The last access.
        /// </summary>
        protected long last;

        /// <summary>
        /// Gets the last access.
        /// </summary>
        /// <value>The last.</value>
        public DateTime Last
        {
            get
            {
                long lastHit = Interlocked.CompareExchange(ref last, 0, 0);
                return DateTime.FromBinary(lastHit);
            }
        }
    }
}

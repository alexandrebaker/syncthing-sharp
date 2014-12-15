using System;
using System.Threading;

namespace Syncthing.Protocol
{
    public abstract class BaseCounting
    {
        protected int totalIncoming;
        protected int totalOutcoming;

        protected int total;
        public int Total
        {
            get
            {
                int val = Interlocked.CompareExchange(ref total, 0, 0);
                return val;
            }
        }

        protected long last;
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

using System;
using System.IO;
using System.Threading;

namespace Syncthing.Protocol
{
    /// <summary>
    /// Writer that count bytes written.
    /// </summary>
    public class CountingWriter : BaseCounting
    {
        /// <summary>
        /// Initialize the Counting Writer
        /// </summary>
        /// <param name="reader"></param>
        public CountingWriter(Stream reader)
        {
            Writer = reader;
        }

        protected Stream Writer { get; set; }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Write(byte[] data)
        {
            var n = data.Length;
            Writer.Write(data,0, n);
            Interlocked.Add(ref total, n);
            Interlocked.Add(ref totalOutcoming, n);
            Interlocked.Exchange(ref last, DateTime.Now.ToBinary());
            return data.Length;
        }
    }
}

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Syncthing.Protocol
{
    /// <summary>
    /// Reader that count bytes red
    /// </summary>
    public class CountingReader : BaseCounting
    {
        /// <summary>
        /// Initialize the Counting Reader
        /// </summary>
        /// <param name="reader"></param>
        public CountingReader(Stream reader)
        {
            Reader = reader;
        }


        protected Stream Reader { get; set; }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Read([In, Out] byte[] data)
        {
            var n = data.Length;
            Reader.Read(data, 0, n);
            Interlocked.Add(ref total, n);
            Interlocked.Add(ref totalIncoming, n);
            Interlocked.Exchange(ref last, DateTime.Now.ToBinary());
            return data.Length;
        }
    }
}

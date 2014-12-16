using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Syncthing.IO
{
    /// <summary>
    /// Counting reader.
    /// </summary>
    public class CountingReader : BaseCounting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.IO.CountingReader"/> class.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public CountingReader(Stream reader)
        {
            InnerReader = reader;
        }

        /// <summary>
        /// Gets or sets the inner reader.
        /// </summary>
        /// <value>The reader.</value>
        protected Stream InnerReader { get; set; }

        /// <summary>
        /// Read the specified data.
        /// </summary>
        /// <param name="data">Data.</param>
        public int Read([In, Out] byte[] data)
        {
            var n = data.Length;
            InnerReader.Read(data, 0, n);
            Interlocked.Add(ref total, n);
            Interlocked.Add(ref totalIncoming, n);
            Interlocked.Exchange(ref last, DateTime.Now.ToBinary());
            return data.Length;
        }
    }
}

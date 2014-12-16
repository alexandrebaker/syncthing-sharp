using System;
using System.IO;
using System.Threading;

namespace Syncthing.IO
{
    /// <summary>
    /// Counting writer.
    /// </summary>
    public class CountingWriter : BaseCounting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.IO.CountingWriter"/> class.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public CountingWriter(Stream reader)
        {
            InnerWriter = reader;
        }

        /// <summary>
        /// Gets or sets the inner writer.
        /// </summary>
        /// <value>The inner writer.</value>
        protected Stream InnerWriter { get; set; }

        /// <summary>
        /// Write the specified data.
        /// </summary>
        /// <param name="data">Data.</param>
        public int Write(byte[] data)
        {
            var n = data.Length;
            InnerWriter.Write(data, 0, n);
            Interlocked.Add(ref total, n);
            Interlocked.Add(ref totalOutcoming, n);
            Interlocked.Exchange(ref last, DateTime.Now.ToBinary());
            return data.Length;
        }
    }
}

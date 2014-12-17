using System;

namespace Syncthing.Protocol.v1.Messages
{
    /// <summary>
    /// Header.
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public int MessageId { get; set; }


        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public int MessageType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Syncthing.Protocol.Messages.Header"/> is compression.
        /// </summary>
        /// <value><c>true</c> if compression; otherwise, <c>false</c>.</value>
        public bool Compression { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            return obj.GetType() == GetType() && Equals((Header)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Syncthing.Protocol.Messages.Header"/> is equal to the current <see cref="Syncthing.Protocol.Messages.Header"/>.
        /// </summary>
        /// <param name="other">The <see cref="Syncthing.Protocol.Messages.Header"/> to compare with the current <see cref="Syncthing.Protocol.Messages.Header"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Syncthing.Protocol.Messages.Header"/> is equal to the current
        /// <see cref="Syncthing.Protocol.Messages.Header"/>; otherwise, <c>false</c>.</returns>
        protected bool Equals(Header other)
        {
            return Version == other.Version && MessageId == other.MessageId && MessageType == other.MessageType && Compression.Equals(other.Compression);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Version;
                hashCode = (hashCode * 397) ^ MessageId;
                hashCode = (hashCode * 397) ^ MessageType;
                hashCode = (hashCode * 397) ^ Compression.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Encode <see cref="Header"/> into uint.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public static uint Encode(Header h)
        {
            return h.Encode();
        }

        /// <summary>
        /// Decode Uint32 into <see cref="Header"/>.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static Header Decode(uint u)
        {
            return new Header
            {
                Version = (int)(u >> 28) & 0xf,
                MessageId = (int)(u >> 16) & 0xfff,
                MessageType = (int)(u >> 8) & 0xff,
                Compression = (u & 1) == 1
            };
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        private uint Encode()
        {
            uint isComp = 0;
            if (Compression)
                isComp = 1 << 0; // the zeroth bit is the compression bit

            return (uint)(
                ((Version & 0xf) << 28) +
                ((MessageId & 0xfff) << 16) +
                ((MessageType & 0xff) << 8)) +
            isComp;
        }
    }
}

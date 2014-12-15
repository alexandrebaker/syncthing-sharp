using System;

namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Protocol Message Header
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Version of the message/
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Id of the message.
        /// </summary>
        public int MessageId { get; set; }


        /// <summary>
        /// Type of the message.
        /// </summary>
        public int MessageType { get; set; }


        /// <summary>
        /// Whether or not he message used compression.
        /// </summary>
        public bool Compression { get; set; }

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
                Version = (int) (u >> 28) & 0xf,
                MessageId = (int) (u >> 16) & 0xfff,
                MessageType = (int) (u >> 8) & 0xff,
                Compression = (u & 1) == 1
            };
        }

        private uint Encode()
        {
            uint isComp = 0;
            if (Compression)
                isComp = 1 << 0; // the zeroth bit is the compression bit

            return (uint) (
                (Version & 0xf) << 28 +
                (MessageId & 0xfff) << 16 +
                (MessageType & 0xff) << 8) +
                   isComp;
        }
    }
}

using System;
using System.Linq;
using Syncthing.IO.Xdr;

namespace Syncthing.Protocol.v1.Messages
{
    /// <summary>
    /// Max length attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public uint Length { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.Messages.MaxLengthAttribute"/> class.
        /// </summary>
        public MaxLengthAttribute()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.Messages.MaxLengthAttribute"/> class.
        /// </summary>
        /// <param name="length">Length.</param>
        public MaxLengthAttribute(uint length)
        {
            Length = length;
        }
    }
}


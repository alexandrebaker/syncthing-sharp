using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;
using Syncthing.Protocol.v1.Messages;


namespace Syncthing.Protocol.v1.Messages
{
    /*
    Option Structure:
     0                   1                   2                   3
     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                         Length of Key                         |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                     Key (variable length)                     \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    |                        Length of Value                        |
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /                                                               /
    \                    Value (variable length)                    \
    /                                                               /
    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+


    */
    /// <summary>
    /// Option.
    /// </summary>
    public class Option : IXdrEncodable, IXdrDecodable
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [MaxLength(64)]
        public string Key { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [MaxLength(1024)]
        public string Value { get; set; }
        //max:1024

        /// <summary>
        /// Encodes the xdr.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public void EncodeXdr([In, Out]XdrWriter writer)
        {
            this.ValidateLength();

            writer.WriteString(Key);
            writer.WriteString(Value);
        }

        /// <summary>
        /// Decode the xdr.
        /// </summary>
        /// <param name="reader">Reader.</param>
        public void DecodeXdr([In, Out] XdrReader reader)
        {
            this.Key = reader.ReadStringMax(64);
            this.Value = reader.ReadStringMax(1024);
        }
    }
}

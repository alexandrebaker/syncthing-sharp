
namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// Option.
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }
        // max:64

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
        //max:1024
    }
}

using Syncthing.IO.Xdr;
using System.Runtime.InteropServices;

namespace Syncthing.Protocol.Messages
{
    /// <summary>
    /// IMessage definition.
    /// </summary>
    public interface IMessage
    {
        void EncodeXdr([In, Out]XdrWriter writer);
    }
}

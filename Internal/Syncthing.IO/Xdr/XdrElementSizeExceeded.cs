using System;

namespace Syncthing.IO.Xdr
{
    public class XdrElementSizeExceeded : Exception
    {
        public XdrElementSizeExceeded(string elem, int size, int maxSize)
            : base(String.Format("Length of '{0}' ({1}) > Max Length ({2})", elem, size, maxSize))
        {
        }
    }
}


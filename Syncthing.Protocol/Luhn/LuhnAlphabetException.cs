using System;

namespace Syncthing.Protocol.Luhn
{
    public class LuhnAlphabetException : Exception
    {
        public LuhnAlphabetException(string message) : base(message) { }
    }
}

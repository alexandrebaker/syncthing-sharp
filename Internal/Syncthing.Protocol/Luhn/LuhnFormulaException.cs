using System;

namespace Syncthing.Protocol.v1.Luhn
{
    /// <summary>
    /// Luhn formula exception.
    /// </summary>
    public class LuhnFormulaException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.Luhn.LuhnFormulaException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public LuhnFormulaException(string message)
            : base(message)
        {
        }
    }
}

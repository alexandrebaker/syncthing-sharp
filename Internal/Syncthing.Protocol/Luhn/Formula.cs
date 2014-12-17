using System;
using System.Globalization;
using System.Text;

namespace Syncthing.Protocol.v1.Luhn
{
    /// <summary>
    /// Formula.
    /// </summary>
    public class Formula
    {
        private readonly string _base32Alphabet = Utf8Encode("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567");
        private readonly string _alphabet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.Luhn.Formula"/> class.
        /// </summary>
        /// <param name="alphabet">Alphabet.</param>
        public Formula(string alphabet)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new LuhnFormulaException("The alphabet cannot be null or empty string.");

            Check(alphabet);
            _alphabet = alphabet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syncthing.Protocol.Luhn.Formula"/> class.
        /// </summary>
        public Formula()
        {
            if (string.IsNullOrEmpty(_base32Alphabet))
                throw new LuhnFormulaException("The alphabet cannot be null or empty string.");

            Check(_base32Alphabet);
            _alphabet = _base32Alphabet;
        }

        /// <summary>
        /// Generate returns a check digit for the string s.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public char Generate(string s)
        {
            int factor = 1;
            int sum = 0;
            int n = _alphabet.Length;

            // Starting from the right and working leftwards is easier since 
            // the initial "factor" will always be "1" 
            foreach (char t in s)
            {
                int codepoint = CodePointFromCharacter(t);
                var addend = factor * codepoint;

                // Alternate the "factor" that each "codePoint" is multiplied by
                factor = factor == 2 ? 1 : 2;

                // Sum the digits of the "addend" as expressed in base "n"
                addend = (addend / n) + (addend % n);
                sum += addend;
            }

            // Calculate the number that must be added to the "sum" 
            // to make it divisible by "n"
            var remainder = sum % n;
            var checkCodepoint = (n - remainder) % n;

            return CharacterFromCodePoint(checkCodepoint);
        }

        /// <summary>
        /// Validate returns true if the last character of the string s is correct.
        /// </summary>
        /// <param name="s">S.</param>
        public bool Validate(string s)
        {
            int factor = 1;
            int sum = 0;
            int n = _alphabet.Length;

            // Starting from the right, work leftwards
            // Now, the initial "factor" will always be "1" 
            // since the last character is the check character
            foreach (char t in s)
            {
                int codepoint = CodePointFromCharacter(t);
                var addend = factor * codepoint;

                // Alternate the "factor" that each "codePoint" is multiplied by
                factor = factor == 2 ? 1 : 2;

                // Sum the digits of the "addend" as expressed in base "n"
                addend = (addend / n) + (addend % n);
                sum += addend;
            }

            var remainder = sum % n;
            return remainder == 0;
        }

        /// <summary>
        /// Check the specified alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet.</param>
        public void Check(string alphabet)
        {
            string str = string.Empty;
            foreach (char t in alphabet)
            {
                if (str.Contains(t.ToString(CultureInfo.InvariantCulture)))
                    throw new LuhnFormulaException(string.Format("Digit {0} non-unique in alphabet {1}", t, alphabet));
                str += t;
            }
        }

        /// <summary>
        /// Codes the point from character.
        /// </summary>
        /// <returns>The point from character.</returns>
        /// <param name="c">C.</param>
        private int CodePointFromCharacter(char c)
        {
            var codepoint = _alphabet.IndexOf(c);
            ;
            if (codepoint == -1)
                throw new LuhnFormulaException(string.Format("Digit {0} not valid in alphabet {1}", c, _alphabet));
            return codepoint;
        }

        /// <summary>
        /// Characters from code point.
        /// </summary>
        /// <returns>The from code point.</returns>
        /// <param name="cp">Cp.</param>
        private char CharacterFromCodePoint(int cp)
        {
            return _alphabet[cp];
        }

        /// <summary>
        /// UTF8s the encode.
        /// </summary>
        /// <returns>The encode.</returns>
        /// <param name="val">Value.</param>
        private static string Utf8Encode(string val)
        {
            byte[] bytes = Encoding.Default.GetBytes(val);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}

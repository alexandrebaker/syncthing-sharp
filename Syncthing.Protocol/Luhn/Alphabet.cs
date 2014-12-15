using System;
using System.Globalization;
using System.Text;

namespace Syncthing.Protocol.Luhn
{
    public class Alphabet
    {
        private readonly string _base32Alphabet = Utf8Encode("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567");
        private readonly string _alphabet;

        public Alphabet(string alphabet)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new LuhnAlphabetException("The alphabet cannot be null or empty string.");

            Check(alphabet);
            _alphabet = alphabet;
        }

        public Alphabet()
        {
            if (string.IsNullOrEmpty(_base32Alphabet))
                throw new LuhnAlphabetException("The alphabet cannot be null or empty string.");

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
        ///  Validate returns true if the last character of the string s is correct.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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

        public void Check(string alphabet)
        {
            string str = string.Empty;
            foreach (char t in alphabet)
            {
                if (str.Contains(t.ToString(CultureInfo.InvariantCulture)))
                    throw new LuhnAlphabetException(string.Format("Digit {0} non-unique in alphabet {1}", t, alphabet));
                str += t;
            }
        }

        private int CodePointFromCharacter(char c)
        {
            var codepoint = _alphabet.IndexOf(c); ;
                if (codepoint == -1)
                    throw new LuhnAlphabetException(string.Format("Digit {0} not valid in alphabet {1}", c, _alphabet));
            return codepoint;
        }

        private char CharacterFromCodePoint(int cp)
        {
            return _alphabet[cp];
        }

        private static string Utf8Encode(string val)
        {
            byte[] bytes = Encoding.Default.GetBytes(val);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}

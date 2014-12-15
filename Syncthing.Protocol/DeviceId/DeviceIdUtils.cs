using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Syncthing.Protocol
{
    internal static class DeviceIdUtils
    {
        internal const int UnluhnifiedStringLength = 52;
        internal const int UnLuhnifiedSplitLength = 13;

        internal const int LuhnifiedStringLength = 56;
        internal const int LuhnifiedSplitLength = 14;

        internal const string TrailingEqual = "====";

        internal static DeviceId DeviceIdFromString(string s)
        {
            var n = new DeviceId();
            n.UnMarshalText(Encoding.UTF8.GetBytes(s));
            return n;
        }

        internal static DeviceId DeviceIdFromBytes(byte[] bs)
        {
            return new DeviceId(bs);
        }

        internal static string Chunkify(string s)
        {
            var tmp = new Regex("(.{7})", RegexOptions.Compiled).Replace(s, "$1-");
            return tmp.Trim('-');
        }

        internal static string UnChukify(string s)
        {
            return s.Replace("-", String.Empty).Replace(" ", String.Empty);
        }

        internal static string Untypeoify(string s)
        {
            return s.Replace("0", "O").Replace("1", "I").Replace("8", "B");
        }


        internal static string Luhnify(string s)
        {
            if (s.Length != UnluhnifiedStringLength)
            {
                throw new Exception("unsupported string length");
            }

            var result = new string[4];
            for (int i = 0; i < 4; i++)
            {
                var p = s.Substring(i * UnLuhnifiedSplitLength, UnLuhnifiedSplitLength);
                var l = new Luhn.Alphabet().Generate(p);

                result[i] = p + l;
            }
            return result[0] + result[1] + result[2] + result[3];
        }

        internal static string UnLuhnify(string s)
        {
            if (s.Length != LuhnifiedStringLength)
            {
                throw new Exception("unsupported string length");
            }

            var result = new string[4];
            for (int i = 0; i < 4; i++)
            {
                var p = s.Substring(i * LuhnifiedSplitLength, LuhnifiedSplitLength - 1);
                var l = new Luhn.Alphabet().Generate(p);


                if ((p + l) != s.Substring(i * LuhnifiedSplitLength, LuhnifiedSplitLength))
                    throw new Exception("Check digit incorrect");

                result[i] = p;
            }
            return result[0] + result[1] + result[2] + result[3];
        }

        // Copyright (c) 2008-2013 Hafthor Stefansson
        // Distributed under the MIT/X11 software license
        // Ref: http://www.opensource.org/licenses/mit-license.php.
        internal static unsafe bool UnsafeCompare(byte[] a1, byte[] a2)
        {
            if (a1 == null || a2 == null || a1.Length != a2.Length)
                return false;
            fixed (byte* p1 = a1, p2 = a2)
            {
                byte* x1 = p1, x2 = p2;
                int l = a1.Length;
                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                    if (*((long*)x1) != *((long*)x2))
                        return false;

                if ((l & 4) != 0)
                {
                    if (*((int*)x1) != *((int*)x2))
                        return false;
                    x1 += 4; x2 += 4;
                }

                if ((l & 2) != 0)
                {
                    if (*((short*)x1) != *((short*)x2))
                        return false;
                    x1 += 2; x2 += 2;
                }

                if ((l & 1) == 0)
                    return true;
                return *((byte*)x1) == *((byte*)x2);
            }
        }
    }
}

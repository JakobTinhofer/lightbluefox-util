using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LightBlueFox.Util.Types
{
    public struct Color
    {
        #region Private Members
        private byte r;
        private byte g;
        private byte b;
        private byte a;
        private string hexRGB;
        private string hexRGBA;
        #endregion

        #region Hex String Parsing
        private void UpdateStrings()
        {
            hexRGB = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"); hexRGBA = HexRGB + a.ToString("X2");
        }
        private void ParseString(string value)
        {
            value = value.Trim().ToLower();
            if (value.StartsWith("#"))
                value = value.Substring(1, value.Length - 1);
            else if (value.StartsWith("0x"))
                value = value.Substring(2, value.Length - 2);
            if (Regex.IsMatch(value, "(?i)[abcdef0123456789]{6}([abcdef0123456789]{2})?"))
            {
                r = byte.Parse(value.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(value.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(value.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                if (value.Length == 8)
                    a = byte.Parse(value.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                UpdateStrings();
            }
            else
            {
                throw new ArgumentException("Invalid format for hex string! Expected: 0xFF00AA or FF00AA or FF00AA44 or 0xFF00AA44 or #FF00AA or #FF00AA44!");
            }
        }
        #endregion

        #region Operator Overrrides
        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(Color c1, Color c2)
        {
            if (c1.R == c2.R && c1.G == c2.G && c1.B == c2.B && c1.Alpha == c2.Alpha) return true;
            return false;
        }
        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(Color c1, Color c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// Auto-generated Equals method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Color color &&
                   r == color.r &&
                   g == color.g &&
                   b == color.b &&
                   a == color.a;
        }

        /// <summary>
        /// Auto-generated GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 397963109;
            hashCode = hashCode * -1521134295 + r.GetHashCode();
            hashCode = hashCode * -1521134295 + g.GetHashCode();
            hashCode = hashCode * -1521134295 + b.GetHashCode();
            hashCode = hashCode * -1521134295 + a.GetHashCode();
            return hashCode;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The value of the red channel, from 0 to 255
        /// </summary>
        public byte R { get { return r; } set {
                r = value;
                UpdateStrings();
            }
        }
        /// <summary>
        /// The value of the green channel, from 0 to 255
        /// </summary>
        public byte G { get { return g; } set {
                g = value;
                UpdateStrings();
            }
        }
        /// <summary>
        /// The value of the blue channel, from 0 to 255
        /// </summary>
        public byte B { get { return b; } set {
                b = value;
                UpdateStrings();
            }
        }
        /// <summary>
        /// The value of the alpha (transparency) channel, from 0 to 255
        /// </summary>
        public byte Alpha { get { return a; } set {
                a = value;
                UpdateStrings();
            }
        }
        /// <summary>
        /// The hex represantation of <see cref="R"/> + <see cref="G"/> + <see cref="B"/> like: 0xFF00AA or FF00AA or #FF00AA
        /// </summary>
        public string HexRGB { get { return hexRGB; } set {
                ParseString(value);
            }
        }
        /// <summary>
        /// The hex represantation of <see cref="R"/> + <see cref="G"/> + <see cref="B"/> + <see cref="Alpha"/> like: FF00AA44 or 0xFF00AA44 or #FF00AA44
        /// </summary>
        public string HexRGBA { get { return hexRGBA; } set {
                ParseString(value);
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new color struct using the values given
        /// </summary>
        /// <param name="r">The red channel</param>
        /// <param name="g">The green channel</param>
        /// <param name="b">The blue channel</param>
        /// <param name="alpha">The alpha (transparency) channel. Default: 255 (0xFF)</param>
        public Color(byte r, byte g, byte b, byte alpha = 255)
        {
            this.r = r; this.g = g; this.b = b; this.a = alpha; hexRGB = ""; hexRGBA = ""; UpdateStrings();
        }

        /// <summary>
        /// Creates a new color struct using the given string of format like FFAA00 or 0xFFAA00 or FFAA0044 or 0xFFAA0044 or #FF00AA or #FF00AA44.
        /// </summary>
        /// <param name="str">The color hex string of formats like FFAA00 or 0xFFAA00 or FFAA0044 or 0xFFAA0044 or #FF00AA or #FF00AA44.</param>
        public Color(string str)
        {
            r = 0;
            g = 0;
            b = 0;
            a = 255;
            hexRGB = "";
            hexRGBA = "";
            ParseString(str);
        }
        #endregion

        #region Converters
        /// <summary>
        /// Gets the console color closest to the current color.
        /// </summary>
        /// <returns></returns>
        public ConsoleColor ToClosestConsoleColor()
        {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
        #endregion

    }
}

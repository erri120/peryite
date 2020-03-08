using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Peryite.Common.Skyrim
{
    /// <summary>
    /// A string prefixed with a uint16 length. NOT zero terminated.
    /// Uses the Windows-1252 encoding.
    /// </summary>
    public struct WString
    {
        /// <summary>
        /// Length of <see cref="String"/>
        /// </summary>
        public ushort Prefix;

        /// <summary>
        /// The actual String
        /// </summary>
        public string String;

        #region Operators

        public static implicit operator WString(string s)
        {
            return new WString { String = s } ;
        }

        public static bool operator == (WString s1, WString s2)
        {
            return s1.String == s2.String;
        }

        public static bool operator !=(WString s1, WString s2)
        {
            return s1.String != s2.String;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj is string s)
            {
                return String == s;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String;
        }

        #endregion
    }

    /// <summary>
    ///     FormIDs in save files are stored as 3 bytes, rather than the usual 4 byte uint32 formID. These will be referred to
    ///     as RefID.
    /// </summary>
    public struct RefID
    {
        public byte PluginID;
        public uint ID;
    }

    /// <summary>
    /// VSVAL (Variable-sized value)
    /// </summary>
    public struct VSVAL
    {
        public byte Type;
        public uint Value;

        #region Operators

        public static implicit operator VSVAL(int i)
        {
            return new VSVAL { Value = (uint)i, Type = 2 } ;
        }

        public static bool operator ==(VSVAL v1, VSVAL v2)
        {
            return v1.Value == v2.Value;
        }

        public static bool operator !=(VSVAL v1, VSVAL v2)
        {
            return v1.Value != v2.Value;
        }

        public static bool operator <(VSVAL v1, VSVAL v2)
        {
            return v1.Value < v2.Value;
        }

        public static bool operator >(VSVAL v1, VSVAL v2)
        {
            return v1.Value > v2.Value;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj is int i)
            {
                return Value == i;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion
    }

    public static partial class BinaryReaderExtensions
    {
        public static WString ReadWString([NotNull] this BinaryReader br)
        {
            var result = new WString
            {
                Prefix = br.ReadUInt16()
            };

            result.String = new string(br.ReadChars(result.Prefix));

            return result;
        }

        public static DateTime ReadFileTime([NotNull] this BinaryReader br)
        {
            byte[] b = br.ReadBytes(8);
            var fileTime = BitConverter.ToInt64(b, 0);
            return DateTime.FromFileTimeUtc(fileTime);
        }

        public static RefID ReadRefID([NotNull] this BinaryReader br)
        {
            var byte1 = br.ReadByte();
            var byte2 = br.ReadByte();
            var byte3 = br.ReadByte();

            //the global variable "DragonsAbsorbed" (0x0001C0F2) becomes the bytes: 41 C0 F2
            
            var type = byte1 >> 2;
            var raw = (byte1 + byte2 + byte3) << 2;

            switch (type)
            {
                case 0: break;
                case 1: break;
                case 2: break;
                case 3: break;
                //Impossible
                default: break;
            }

            return default;
        }

        public static VSVAL ReadVSVAL([NotNull] this BinaryReader br)
        {
            var res = new VSVAL();

            var byte1 = br.ReadByte();

            res.Type = (byte) (byte1 & 00000011);

            if (res.Type == 0)
            {
                res.Value = (uint)(byte1 >> 2);
            } else if (res.Type >= 1)
            {
                var byte2 = br.ReadByte();
                if(res.Type == 1)
                {
                    res.Value = (uint) (byte1 | byte2 << 8) >> 2;
                }
                else
                {
                    var byte3 = br.ReadByte();
                    res.Value = (uint) (byte1 | (byte2 << 8) | (byte3 << 16) >> 2);
                }
            }

            return res;
        }
    }
}

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
    }
}

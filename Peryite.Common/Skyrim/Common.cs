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
    }
}

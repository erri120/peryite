using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Peryite.Common.Skyrim
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public enum ChangeFormType : byte
    {
        REFR = 0,
        ACHR = 1,
        PMIS = 2,
        PGRE = 3,
        PBEA = 4,
        PFLA = 5,
        CELL = 6,
        INFO = 7,
        QUST = 8,
        NPC_ = 9,
        ACTI = 10,
        TACT = 11,
        ARMO = 12,
        BOOK = 13,
        CONT = 14,
        DOOR = 15,
        INGR = 16,
        LIGH = 17,
        MISC = 18,
        APPA = 19,
        STAT = 20,
        MSTT = 21,
        FURN = 22,
        WEAP = 23,
        AMMO = 24,
        KEYM = 25,
        ALCH = 26,
        IDLM = 27,
        NOTE = 28,
        ECZN = 29,
        CLAS = 30,
        FACT = 31,
        PACK = 32,
        NAVM = 33,
        WOOP = 34,
        MGEF = 35,
        SMQN = 36,
        LCTN = 38,
        RELA = 39,
        PHZD = 40,
        PBAR = 41,
        PCON = 42,
        FLST = 43,
        LVLN = 44,
        LVLI = 45,
        LVSP = 46,
        PARW = 47,
        ENCH = 48
    }

    public class ChangeForm
    {
        public RefID FormID;
        public uint ChangeFlags;
        public ChangeFormType ChangeFormType;
        public byte Version;
        public uint Length1;
        public uint Length2;
        public SkyrimSaveFileCompression Compression;
        public byte[]? Data;
    }

    public partial class BinaryReaderExtensions
    {
        public static ChangeForm ReadChangeForm(this BinaryReader br)
        {
            var res = new ChangeForm
            {
                FormID = br.ReadRefID(),
                ChangeFlags = br.ReadUInt32()
            };

            var typeByte = br.ReadByte();

            var length = typeByte >> 6;

            if(length < 0 || length > 2)
                throw new CorruptedSaveFileException("Length can only be 0, 1 or 2 while reading a ChangeForm type!", br);

            var type = typeByte & 0x3f;

            if(type >= 64)
                throw new CorruptedSaveFileException("Type can not be greater than 63 while reading a ChangeForm type!", br);

            res.ChangeFormType = (ChangeFormType) type;
            res.Version = br.ReadByte();

            if (length == 0)
            {
                res.Length1 = br.ReadByte();
                res.Length2 = br.ReadByte();
            } else if (length == 1)
            {
                res.Length1 = br.ReadUInt16();
                res.Length2 = br.ReadUInt16();
            }
            else
            {
                res.Length1 = br.ReadUInt32();
                res.Length2 = br.ReadUInt32();
            }

            return res;
        }
    }
}

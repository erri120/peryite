using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class TES : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.TES;

        public VSVAL Count1;
        public Unknown0[] Unknown1 = default!;
        public uint Count2;
        public RefID[] Unknown2 = default!;
        public VSVAL Count3;
        public RefID[] Unknown3 = default!;

        public IGlobalData ReadData(BinaryReader br)
        {
            Count1 = br.ReadVSVAL();
            Unknown1 = new Unknown0[Count1.Value];

            for (var i = 0; i < Count1; i++)
            {
                Unknown1[i] = new Unknown0
                {
                    FormID = br.ReadRefID(),
                    Unknown = br.ReadUInt16()
                };
            }

            Count2 = br.ReadUInt32();
            Unknown2 = new RefID[Count2 * Count2];

            for (var i = 0; i < Unknown2.Length; i++)
            {
                Unknown2[i] = br.ReadRefID();
            }

            Count3 = br.ReadVSVAL();

            Unknown3 = new RefID[Count3.Value];

            for (var i = 0; i < Count3; i++)
            {
                Unknown3[i] = br.ReadRefID();
            }

            return this;
        }

        public struct Unknown0
        {
            public RefID FormID;
            public ushort Unknown;
        }
    }
}

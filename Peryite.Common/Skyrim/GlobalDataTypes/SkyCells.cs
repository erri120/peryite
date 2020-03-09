using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class SkyCells : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.SkyCells;

        public VSVAL Count;
        public Unknown0[] Unknown = default!;

        public IGlobalData ReadData(BinaryReader br)
        {
            Count = br.ReadVSVAL();
            Unknown = new Unknown0[Count.Value];

            for (var i = 0; i < Count; i++)
            {
                Unknown[i] = new Unknown0
                {
                    Unknown1 = br.ReadRefID(),
                    Unknown2 = br.ReadRefID()
                };
            }


            return this;
        }

        public struct Unknown0
        {
            public RefID Unknown1;
            public RefID Unknown2;
        }
    }
}

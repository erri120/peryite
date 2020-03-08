using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class GlobalVariables : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.GlobalVariables;

        public VSVAL Count;
        public GlobalVariable[] Globals;

        public IGlobalData ReadData(BinaryReader br)
        {
            Count = br.ReadVSVAL();

            Globals = new GlobalVariable[Count.Value];

            for (var i = 0; i < Count; i++)
            {
                Globals[i] = new GlobalVariable
                {
                    FormID = br.ReadRefID(),
                    Value = br.ReadSingle()
                };
            }

            return this;
        }

        public class GlobalVariable
        {
            public RefID FormID;
            public float Value;
        }
    }
}

using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Effects : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Effects;

        public VSVAL Count;
        public Effect[] ImageSpaceModifiers = default!;
        public float Unknown1;
        public float Unknown2;

        public IGlobalData ReadData(BinaryReader br)
        {
            Count = br.ReadVSVAL();
            ImageSpaceModifiers = new Effect[Count.Value];

            for (var i = 0; i < Count; i++)
            {
                ImageSpaceModifiers[i] = new Effect
                {
                    Strength = br.ReadSingle(),
                    Timestamp = br.ReadSingle(),
                    Unknown = br.ReadUInt32(),
                    EffectID = br.ReadRefID()
                }; 
            }

            Unknown1 = br.ReadSingle();
            Unknown2 = br.ReadSingle();

            return this;
        }

        public struct Effect
        {
            /// <summary>
            /// Value from 0 to 1 with 0 being no effect and 1 full effect
            /// </summary>
            public float Strength;

            /// <summary>
            /// Time from the effect beginning
            /// </summary>
            public float Timestamp;

            /// <summary>
            /// Might be a flag
            /// </summary>
            public uint Unknown;

            /// <summary>
            /// The RefID of the effect
            /// </summary>
            public RefID EffectID;
        }
    }
}

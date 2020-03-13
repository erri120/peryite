namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Effects : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Effects;

        private VSVAL _count;

        [Read(1)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                ImageSpaceModifiers = new Effect[_count.Value];
            }
        }
        [Read(2)]
        public Effect[]? ImageSpaceModifiers;

        [Read(3)]
        public float Unknown1;
        [Read(4)]
        public float Unknown2;

        public struct Effect
        {
            /// <summary>
            /// Value from 0 to 1 with 0 being no effect and 1 full effect
            /// </summary>
            [Read(1)]
            public float Strength;

            /// <summary>
            /// Time from the effect beginning
            /// </summary>
            [Read(2)]
            public float Timestamp;

            /// <summary>
            /// Might be a flag
            /// </summary>
            [Read(3)]
            public uint Unknown;

            /// <summary>
            /// The RefID of the effect
            /// </summary>
            [Read(4)]
            public RefID EffectID;
        }
    }
}

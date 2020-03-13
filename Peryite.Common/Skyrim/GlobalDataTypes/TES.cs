namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class TES : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.TES;

        private VSVAL _count1;

        [Read(1)]
        public VSVAL Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                Unknown1 = new Unknown0[_count1.Value];
            }
        }

        [Read(2)]
        public Unknown0[]? Unknown1;

        private uint _count2;

        [Read(3)]
        public uint Count2
        {
            get => _count2;
            set
            {
                _count2 = value;
                Unknown2 = new RefID[_count2 * _count2];
            }
        }

        [Read(4)]
        public RefID[]? Unknown2;

        private VSVAL _count3;

        [Read(5)]
        public VSVAL Count3
        {
            get => _count3;
            set
            {
                _count3 = value;
                Unknown3 = new RefID[_count3.Value];
            }
        }

        [Read(6)]
        public RefID[]? Unknown3;

        public struct Unknown0
        {
            [Read(1)]
            public RefID FormID;

            [Read(2)]
            public ushort Unknown;
        }
    }
}

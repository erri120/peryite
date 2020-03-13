namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class SkyCells : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.SkyCells;

        private VSVAL _count;

        [Read(1)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                Unknown = new Unknown0[_count.Value];
            }
        }
        [Read(2)]
        public Unknown0[]? Unknown;

        public struct Unknown0
        {
            [Read(1)]
            public RefID Unknown1;
            [Read(2)]
            public RefID Unknown2;
        }
    }
}

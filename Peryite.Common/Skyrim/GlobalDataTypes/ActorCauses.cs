namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class ActorCauses : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.ActorCauses;
        
        [Read(1)]
        public uint NextNumber;

        private VSVAL _count;

        [Read(2)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                UnknownArray = new Unknown0[_count.Value];
            }
        }
        [Read(3)] 
        public Unknown0[]? UnknownArray;

        public struct Unknown0
        {
            [Read(1)]
            public float X;
            [Read(2)]
            public float Y;
            [Read(3)]
            public float Z;
            [Read(4)]
            public uint SerialNumber;
            [Read(5)]
            public RefID ActorID;
        }
    }
}

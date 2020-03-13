namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class LocationMetaData : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.LocationMetaData;

        private VSVAL _count;
        
        [Read(1)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                UnknownArray = new Unknown0[_count.Value];
            }
        }
        
        [Read(2, IsCustomType = true)]
        public Unknown0[]? UnknownArray;

        public struct Unknown0
        {
            [Read(1)]
            public RefID UnknownRefID;
            [Read(1)]
            public uint UnknownUInt;
        }
    }
}

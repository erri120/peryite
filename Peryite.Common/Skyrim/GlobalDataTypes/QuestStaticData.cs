namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class QuestStaticData : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.QuestStaticData;

        private uint _count0;
        
        [Read(1)]
        public uint Count0
        {
            get => _count0;
            set
            {
                _count0 = value;
                QuestRunDataItem3Array1 = new QuestRunDataItem3[_count0];
            }
        }
        
        [Read(2, IsCustomType = true)]
        public QuestRunDataItem3[]? QuestRunDataItem3Array1;

        private uint _count1;
        
        [Read(3)]
        public uint Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                QuestRunDataItem3Array2 = new QuestRunDataItem3[_count1];
            }
        }
        [Read(4, IsCustomType = true)]
        public QuestRunDataItem3[]? QuestRunDataItem3Array2;

        private uint _count2;
        
        [Read(5)]
        public uint Count2
        {
            get => _count2;
            set
            {
                _count2 = value;
                UnknownRefIDArray1 = new RefID[_count2];
            }
        }
        [Read(6)]
        public RefID[]? UnknownRefIDArray1;

        private uint _count3;
        
        [Read(7)]
        public uint Count3
        {
            get => _count3;
            set
            {
                _count3 = value;
                UnknownRefIDArray2 = new RefID[_count3];
            }
        }
        [Read(8)]
        public RefID[]? UnknownRefIDArray2;

        private uint _count4;
        
        [Read(9)]
        public uint Count4
        {
            get => _count4;
            set
            {
                _count4 = value;
                UnknownRefIDArray3 = new RefID[_count4];
            }
        }
        [Read(10)]
        public RefID[]? UnknownRefIDArray3;

        private VSVAL _count5;
        
        [Read(11)]
        public VSVAL Count5
        {
            get => _count5;
            set
            {
                _count5 = value;
                Unknown0Array = new Unknown0[_count5.Value];
            }
        }
        
        [Read(12, IsCustomType = true)]
        public Unknown0[]? Unknown0Array;
        
        [Read(13)]
        public byte UnknownByte;

        public struct Unknown0
        {
            [Read(1)]
            public RefID UnknownRefID;
            private VSVAL _count;
            
            [Read(2)]
            public VSVAL Count
            {
                get => _count;
                set
                {
                    _count = value;
                    Unknown1Array = new Unknown1[_count.Value];
                }
            }
            
            [Read(3, IsCustomType = true)]
            public Unknown1[]? Unknown1Array;

            public struct Unknown1
            {
                [Read(1)]
                public uint UnknownUInt1;
                [Read(2)]
                public uint UnknownUInt2;
            }
        }
    }
}

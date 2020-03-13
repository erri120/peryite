namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Interface : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Interface;

        private uint _shownHelpMessageCount;
        
        [Read(1)]
        public uint ShownHelpMessageCount
        {
            get => _shownHelpMessageCount;
            set
            {
                _shownHelpMessageCount = value;
                ShownHelpMessages = new uint[_shownHelpMessageCount];
            }
        }
        [Read(2)]
        public uint[]? ShownHelpMessages;
        [Read(3)]
        public byte UnknownByte1;
        private VSVAL _lastUsedWeaponsCount;
        
        [Read(4)]
        public VSVAL LastUsedWeaponsCount
        {
            get => _lastUsedWeaponsCount;
            set
            {
                _lastUsedWeaponsCount = value;
                LastUsedWeapons = new RefID[_lastUsedWeaponsCount.Value];
            }
        }
        [Read(5)]
        public RefID[]? LastUsedWeapons;
        private VSVAL _lastUsedSpellsCount;
        
        [Read(6)]
        public VSVAL LastUsedSpellsCount
        {
            get => _lastUsedSpellsCount;
            set
            {
                _lastUsedSpellsCount = value;
                LastUsedSpells =new RefID[_lastUsedSpellsCount.Value]; 
            }
        }
        [Read(7)]
        public RefID[]? LastUsedSpells;
        private VSVAL _lastUsedShoutsCount;
        
        [Read(8)]
        public VSVAL LastUsedShoutsCount
        {
            get => _lastUsedShoutsCount;
            set
            {
                _lastUsedShoutsCount = value;
                LastUsedShouts = new RefID[_lastUsedShoutsCount.Value];
            }
        }
        [Read(9)]
        public RefID[]? LastUsedShouts;
        [Read(10)]
        public byte UnknownByte2;
        [Read(11, IsCustomType = true)]
        public Unknown0? Unknown;

        public class Unknown0
        {
            private VSVAL _count1;
            
            [Read(1)]
            public VSVAL Count1
            {
                get => _count1;
                set
                {
                    _count1 = value;
                    UnknownArray = new Unknown0[_count1.Value];
                }
            }
            [Read(2, IsCustomType = true)]
            public Unknown0[]? UnknownArray;
            private VSVAL _count2;
            
            [Read(3)]
            public VSVAL Count2
            {
                get => _count2;
                set
                {
                    _count2 = value;
                    UnknownWStringArray = new WString[_count2.Value];
                }
            }
            [Read(4)]
            public WString[]? UnknownWStringArray;
            [Read(5)]
            public uint UnknownUInt;

            public struct Unknown00
            {
                [Read(1)]
                public WString UnknownWString1;
                [Read(2)]
                public WString UnknownWString2;
                [Read(3)]
                public uint UnknownUInt1;
                [Read(4)]
                public uint UnknownUInt2;
                [Read(5)]
                public uint UnknownUInt3;
                [Read(6)]
                public uint UnknownUInt4;
            }
        }
    }
}

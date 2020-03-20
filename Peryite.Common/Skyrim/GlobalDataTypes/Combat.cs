namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Combat : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Combat;

        [Read(1)]
        public uint NextNumber;

        private VSVAL _count0;

        [Read(2)]
        public VSVAL Count0
        {
            get => _count0;
            set
            {
                _count0 = value;
                Unknown0Array = new Unknown0[_count0.Value];
            }
        }
        [Read(3)]
        public Unknown0[]? Unknown0Array;

        private VSVAL _count1;

        [Read(4)]
        public VSVAL Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                Unknown1Array = new Unknown1[_count1.Value];
            }
        }
        [Read(5)]
        public Unknown1[]? Unknown1Array;
        
        [Read(6)]
        public float UnknownFloat1;
        [Read(7)]
        public VSVAL UnknownVSVAL1;

        private VSVAL _count2;

        [Read(8)]
        public VSVAL Count2
        {
            get => _count2;
            set
            {
                _count2 = value;
                UnknownRefIDArray = new RefID[_count2.Value];
            }
        }
        [Read(9)]
        public RefID[]? UnknownRefIDArray;
        
        [Read(10)]
        public float UnknownFloat2;
        [Read(11, IsCustomType = true)]
        public UnknownStruct UnknownStruct1;
        [Read(12, IsCustomType = true)]
        public UnknownStruct UnknownStruct2;

        public class Unknown0
        {
            [Read(1)]
            public uint UnknownUInt32;
            [Read(2)]
            public uint SerialNumber;
            [Read(3, IsCustomType = true)]
            public Unknown00? Unknown;

            public class Unknown00
            {
                private VSVAL _count0;

                [Read(1)]
                public VSVAL Count0
                {
                    get => _count0;
                    set
                    {
                        _count0 = value;
                        Unknown000Array = new Unknown000[_count0.Value];
                    }
                }
                [Read(2, IsCustomType = true)]
                public Unknown000[]? Unknown000Array;

                private VSVAL _count1;

                [Read(3)]
                public VSVAL Count1
                {
                    get => _count1;
                    set
                    {
                        _count1 = value;
                        Unknown001Array = new Unknown001[_count1.Value];
                    }
                }
                [Read(4, IsCustomType = true)]
                public Unknown001[]? Unknown001Array;
                
                [Read(5, IsCustomType = true)]
                public UnknownStruct UnknownStruct1;
                [Read(6, IsCustomType = true)]
                public UnknownStruct UnknownStruct2;
                [Read(7, IsCustomType = true)]
                public UnknownStruct UnknownStruct3;
                [Read(8, IsCustomType = true)]
                public UnknownStruct UnknownStruct4;
                [Read(9, IsCustomType = true)]
                public UnknownStruct[] UnknownStructArray = new UnknownStruct[11];
                
                [Read(10)]
                public uint UnknownFlag;
                
                /// <summary>
                /// Only present if <see cref="UnknownFlag"/> is not zero
                /// </summary>
                [Read(11, IsCustomType = true)]
                [ConditionalParsing(Name = nameof(UnknownFlag), And = true, Chaining = new[] {(object) (uint)0},
                    ChainingType = typeof(uint), Not = true)]
                public Unknown002? Unknown002Field;
                
                [Read(12, IsCustomType = true)]
                public UnknownStruct UnknownStruct5;
                
                [Read(13)]
                public float UnknownFloat1;
                [Read(14)]
                public float UnknownFloat2;
                [Read(15)]
                public float UnknownFloat3;
                [Read(16)]
                public float UnknownFloat4;
                
                [Read(17, IsCustomType = true)]
                public UnknownStruct UnknownStruct6;
                
                [Read(18)]
                public byte UnknownByte;

                public class Unknown000
                {
                    [Read(1)]
                    public RefID UnknownRefID;
                    [Read(2)]
                    public uint UnknownUInt32;
                    [Read(3)]
                    public float UnknownFloat1;
                    [Read(4)]
                    public ushort UnknownUshort1;
                    [Read(5)]
                    public ushort UnknownUshort2;
                    
                    [Read(6)]
                    public RefID Target;
                    
                    [Read(7, IsCustomType = true)]
                    public Position UnknownPosition1;
                    [Read(8, IsCustomType = true)]
                    public Position UnknownPosition2;
                    [Read(9, IsCustomType = true)]
                    public Position UnknownPosition3;
                    [Read(10, IsCustomType = true)]
                    public Position UnknownPosition4;
                    [Read(11, IsCustomType = true)]
                    public Position UnknownPosition5;
                    
                    [Read(12)]
                    public float UnknownFloat2;
                    [Read(13)]
                    public float UnknownFloat3;
                    [Read(14)]
                    public float UnknownFloat4;
                    [Read(15)]
                    public float UnknownFloat5;
                    [Read(16)]
                    public float UnknownFloat6;
                    [Read(17)]
                    public float UnknownFloat7;
                }

                public class Unknown001
                {
                    [Read(1)]
                    public RefID UnknownRefID;
                    [Read(2)]
                    public float UnknownFloat1;
                    [Read(3)]
                    public float UnknownFloat2;
                }

                public class Unknown002
                {
                    [Read(1)]
                    public RefID UnknownRefID;
                    [Read(2, IsCustomType = true)]
                    public UnknownStruct UnknownStruct1;
                    [Read(3, IsCustomType = true)]
                    public UnknownStruct UnknownStruct2;
                    
                    [Read(4)]
                    public float UnknownFloat1;
                    [Read(5, IsCustomType = true)]
                    public Position UnknownPosition;
                    [Read(6)]
                    public float UnknownFloat2;

                    private VSVAL _count0;

                    [Read(7)]
                    public VSVAL Count0
                    {
                        get => _count0;
                        set
                        {
                            _count0 = value;
                            Unknown0020Array = new Unknown0020[_count0.Value];
                        }
                    }
                    [Read(8, IsCustomType = true)]
                    public Unknown0020[]? Unknown0020Array;

                    private VSVAL _count1;

                    [Read(9)]
                    public VSVAL Count1
                    {
                        get => _count1;
                        set
                        {
                            _count1 = value;
                            Unknown0021Array = new Unknown0021[_count1.Value];
                        }
                    }
                    [Read(10, IsCustomType = true)]
                    public Unknown0021[]? Unknown0021Array;

                    [Read(11)]
                    public byte UnknownFlag;

                    /// <summary>
                    /// Only present if <see cref="UnknownFlag"/> is not zero
                    /// </summary>
                    [Read(12, IsCustomType = true)]
                    [ConditionalParsing(Name = nameof(UnknownFlag), And = true, Chaining = new[] {(object) (byte)0},
                        ChainingType = typeof(byte), Not = true)]
                    public Unknown0022? Unknown0022Field;

                    public class Unknown0020
                    {
                        [Read(1, IsCustomType = true)]
                        public Position UnknownPosition;
                        [Read(2)]
                        public uint UnknownUInt;
                        [Read(3)]
                        public float UnknownFloat;
                    }

                    public class Unknown0021
                    {
                        [Read(1)]
                        public RefID UnknownRefID1;
                        [Read(2)]
                        public RefID UnknownRefID2;
                        
                        [Read(3)]
                        public byte UnknownByte1;
                        [Read(4)]
                        public byte UnknownByte2;
                        [Read(5)]
                        public byte UnknownByte3;
                    }

                    public class Unknown0022
                    {
                        [Read(1)]
                        public uint UnknownUInt1;
                        [Read(2)]
                        public uint UnknownUInt2;

                        private uint _count0;

                        [Read(3)]
                        public uint Count0
                        {
                            get => _count0;
                            set
                            {
                                _count0 = value;
                                Unknown00220Array = new Unknown00220[_count0];
                            }
                        }
                        [Read(4, IsCustomType = true)]
                        public Unknown00220[]? Unknown00220Array;
                        
                        [Read(5)]
                        public RefID UnknownRefID1;
                        [Read(6)]
                        public float UnknownFloat1;
                        [Read(7)]
                        public float UnknownFloat2;
                        [Read(8)]
                        public float UnknownFloat3;
                        [Read(9)]
                        public float UnknownFloat4;
                        [Read(10)]
                        public float UnknownFloat5;
                        [Read(11)]
                        public RefID UnknownRefID2;
                        
                        [Read(12)]
                        public float UnknownFloat6;
                        [Read(13)]
                        public RefID UnknownRefID3;
                        
                        [Read(14)]
                        public ushort UnknownUShort;
                        [Read(15)]
                        public byte UnknownByte1;
                        [Read(16)]
                        public byte UnknownByte2;
                        
                        [Read(17)]
                        public float UnknownFloat7;
                        [Read(18)]
                        public float UnknownFloat8;

                        public class Unknown00220
                        {
                            [Read(1)]
                            public byte UnknownByte;

                            private uint _count0;

                            [Read(2)]
                            public uint Count0
                            {
                                get => _count0;
                                set
                                {
                                    _count0 = value;
                                    UnknownByteArray = new byte[_count0];
                                }
                            }
                            [Read(3)]
                            public byte[]? UnknownByteArray;
                            [Read(4)]
                            public RefID UnknownRefID;
                            [Read(5)]
                            public uint UnknownUInt32;
                        }
                    }
                }

                public struct Position
                {
                    [Read(1)]
                    public float X;
                    [Read(2)]
                    public float Y;
                    [Read(3)]
                    public float Z;
                    [Read(4)]
                    public RefID CellID;
                }
            }
        }

        public class Unknown1
        {
            [Read(1)]
            public RefID UnknownRefID;
            [Read(2)]
            public float UnknownFloat;
            [Read(3, IsCustomType = true)]
            public Unknown10? Unknown;

            public class Unknown10
            {
                [Read(1)]
                public RefID UnknownRefID1;
                [Read(2)]
                public RefID UnknownRefID2;
                
                [Read(3)]
                public float UnknownFloat1;
                [Read(4)]
                public float UnknownFloat2;
                [Read(5)]
                public float UnknownFloat3;
                
                [Read(6)]
                public float X;
                [Read(7)]
                public float Y;
                [Read(8)]
                public float Z;
                
                [Read(9)]
                public float UnknownFloat4;
                [Read(10)]
                public float UnknownFloat5;
                [Read(11)]
                public float UnknownFloat6;
                [Read(12)]
                public float UnknownFloat7;
                [Read(13)]
                public float UnknownFloat8;
                [Read(14)]
                public float UnknownFloat9;
                [Read(15)]
                public float UnknownFloat10;
                [Read(16)]
                public float UnknownFloat11;
                
                [Read(17)]
                public RefID UnknownRefID3;
            }
        }

        public struct UnknownStruct
        {
            [Read(1)]
            public float UnknownFloat1;
            [Read(2)]
            public float UnknownFloat2;
        }
    }
}

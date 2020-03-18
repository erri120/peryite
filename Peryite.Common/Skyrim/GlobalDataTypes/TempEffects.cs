using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class TempEffects : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.TempEffects;

        private VSVAL _count0;
        
        [Read(1)]
        public VSVAL Count0
        {
            get => _count0;
            set
            {
                _count0 = value;
                Unknown0Array = new Unknown0[_count0.Value];
            }
        }
        
        [Read(2, IsCustomType = true)]
        public Unknown0[]? Unknown0Array;

        [Read(3)]
        public uint UnknownUInt;

        private VSVAL _count1;
        
        [Read(4)]
        public VSVAL Count1
        {
            get => _count1;
            set
            {
                _count1 = value;
                Unknown1Array1 = new Unknown1[_count1.Value];
            }
        }
        
        [Read(5, IsCustomType = true)]
        public Unknown1[]? Unknown1Array1;

        private VSVAL _count2;
        
        [Read(6)]
        public VSVAL Count2
        {
            get => _count2;
            set
            {
                _count2 = value;
                Unknown1Array2 = new Unknown1[_count2.Value];
            }
        }
        
        [Read(7, IsCustomType = true)]
        public Unknown1[]? Unknown1Array2;

        public class Unknown0
        {
            [Read(1)]
            public byte Flag;
            [Read(2)]
            public RefID UnknownRefID1;
            [Read(3)]
            [ConditionalParsing(Name = nameof(Flag), Not = true, ChainingType = typeof(byte), Chaining = new object[]{(byte)0})]
            public uint UnknownUInt1;
            [Read(4)]
            public RefID UnknownRefID2;
            [Read(5)]
            public RefID UnknownRefID3;
            [Read(6)]
            public float[] UnknownFloatArray1 = new float[3];
            [Read(7)]
            public float[] UnknownFloatArray2 = new float[3];
            [Read(8)]
            public float UnknownFloat1;
            [Read(9)]
            public float UnknownFloat2;
            [Read(10)]
            public float UnknownFloat3;
            [Read(11)]
            public float[] UnknownFloatArray3 = new float[4];
            [Read(12)]
            public byte UnknownByte1;
            [Read(13)]
            public byte UnknownByte2;
            [Read(14)]
            public byte UnknownByte3;
            [Read(15)]
            public byte UnknownByte4;
            [Read(16)]
            public float UnknownFloat5;
            [Read(17)]
            public byte UnknownByte5;
            [Read(18)]
            public float UnknownFloat6;
            [Read(19)]
            public float UnknownFloat7;
            [Read(20)]
            public float UnknownFloat8;
            [Read(21)]
            public float UnknownFloat9;
            [Read(22)]
            public float UnknownFloat10;
            [Read(23)]
            public byte UnknownByte6;
            [Read(24)]
            public byte UnknownByte7;
            [Read(25)]
            public uint UnknownUInt2;
        }

        public interface IUnknownTypeDef { }

        public class Unknown1 : ICustomRead
        {
            public uint Type;
            public IUnknownTypeDef? UnknownType;

            public void ReadData(BinaryReader br)
            {
                Type = br.ReadUInt32();

                if (Type == 1 || Type == 3 || Type == 4 || Type == 5 || Type == 7)
                {
                    UnknownType = new Unknown1Def().ReadThis(br);
                } else if (Type == 0)
                {
                    UnknownType = new Unknown10().ReadThis(br);
                } else if (Type == 6)
                {
                    UnknownType = new Unknown16().ReadThis(br);
                } else if (Type == 8)
                {
                    UnknownType = new Unknown18().ReadThis(br);
                } else if (Type == 9)
                {
                    UnknownType = new Unknown19().ReadThis(br);
                }
                else
                {
                    throw new CorruptedSaveFileException($"Unknown type {Type}!", br);
                }
            }
        }

        public class Unknown1Def : IUnknownTypeDef
        {
            [Read(1)]
            public float UnknownFloat1;
            [Read(2)]
            public float UnknownFloat2;
            [Read(3)]
            public byte UnknownByte;
            [Read(4)]
            public uint UnknownUInt;
        }

        public class Unknown10 : IUnknownTypeDef
        {
            [Read(1, IsCustomType = true)]
            public Unknown1Def? Unknown1Def;
            [Read(2)]
            public uint[] UnknownUIntArray1 = new uint[4];
            [Read(3)]
            public uint[] UnknownUIntArray2 = new uint[3];
            [Read(4)]
            public uint[] UnknownUIntArray3 = new uint[12];
            [Read(5)]
            public WString UnknownWString;
            [Read(6)]
            public RefID UnknownRefID1;
            [Read(7)]
            public RefID UnknownRefID2;
            [Read(8)]
            public uint UnknownUInt;
        }

        public class Unknown16 : IUnknownTypeDef
        {
            [Read(1, IsCustomType = true)]
            public Unknown1Def? Unknown1Def;
            [Read(2)]
            public RefID UnknownRefID;
            [Read(3)]
            public uint UnknownUInt1;
            [Read(4)]
            public uint UnknownUInt2;
            [Read(5)]
            public byte UnknownByte;
        }

        public class Unknown18 : IUnknownTypeDef
        {
            [Read(1, IsCustomType = true)]
            public Unknown1Def? Unknown1Def;
            [Read(2)]
            public byte UnknownByte;
            [Read(3)]
            public RefID UnknownRefID;
            [Read(4)]
            public byte Flag;
            [Read(5)]
            [ConditionalParsing(Name = nameof(Flag), Type = typeof(byte), ChainingType = typeof(byte), Not = true, Chaining = new object[]{(byte)0})]
            public RefID[] UnknownRefIDArray = new RefID[4];
        }

        public class Unknown19 : IUnknownTypeDef
        {
            [Read(1, IsCustomType = true)]
            public Unknown18? Unknown18;
            [Read(2)]
            public RefID UnknownRefID;
            [Read(3, IsCustomType = true)] 
            public Unknown190? Unknown190Field;

            public class Unknown190
            {
                [Read(1)]
                public VSVAL Length;
                [Read(2)]
                public byte UnknownByte;

                private uint _count0;

                [Read(3)]
                public uint Count0
                {
                    get => _count0;
                    set
                    {
                        _count0 = value;
                        Unknown1900Array = new Unknown1900[_count0];
                    }
                }
                [Read(4, IsCustomType = true)]
                public Unknown1900[]? Unknown1900Array;

                public class Unknown1900
                {
                    [Read(1)]
                    public WString UnknownWString1;

                    private uint _count0;
                    
                    [Read(2)]
                    public uint Count0
                    {
                        get => _count0;
                        set
                        {
                            _count0 = value;
                            UnknownStruct1Array = new UnknownStruct1[_count0];
                        }
                    }
                    
                    [Read(3, IsCustomType = true)]
                    public UnknownStruct1[]? UnknownStruct1Array;

                    private uint _count1;
                    
                    [Read(3)]
                    public uint Count1
                    {
                        get => _count1;
                        set
                        {
                            _count1 = value;
                            UnknownStruct2Array1 = new UnknownStruct2[_count1];
                        }
                    }
                    
                    [Read(4, IsCustomType = true)]
                    public UnknownStruct2[]? UnknownStruct2Array1;

                    private uint _count2;
                    
                    [Read(5)]
                    public uint Count2
                    {
                        get => _count2;
                        set
                        {
                            _count2 = value;
                            UnknownStruct3Array = new UnknownStruct3[_count2];
                        }
                    }
                    
                    [Read(6, IsCustomType = true)]
                    public UnknownStruct3[]? UnknownStruct3Array;

                    private uint _count3;
                    
                    [Read(7)]
                    public uint Count3
                    {
                        get => _count3;
                        set
                        {
                            _count3 = value;
                            UnknownStruct4Array1 = new UnknownStruct4[_count3];
                        }
                    }
                    
                    [Read(8, IsCustomType = true)]
                    public UnknownStruct4[]? UnknownStruct4Array1;

                    private uint _count4;
                    
                    [Read(9)]
                    public uint Count4
                    {
                        get => _count4;
                        set
                        {
                            _count4 = value;
                            UnknownStruct5Array = new UnknownStruct5[_count4];
                        }
                    }
                    
                    [Read(10, IsCustomType = true)]
                    public UnknownStruct5[]? UnknownStruct5Array;

                    private uint _count5;
                    
                    [Read(11)]
                    public uint Count5
                    {
                        get => _count5;
                        set
                        {
                            _count5 = value;
                            UnknownStruct2Array2 = new UnknownStruct2[_count5];
                        }
                    }
                    
                    [Read(12, IsCustomType = true)]
                    public UnknownStruct2[]? UnknownStruct2Array2;

                    private uint _count6;
                    
                    [Read(13)]
                    public uint Count6
                    {
                        get => _count6;
                        set
                        {
                            _count6 = value;
                            UnknownStruct2Array3 = new UnknownStruct2[_count6];
                        }
                    }
                    
                    [Read(14, IsCustomType = true)]
                    public UnknownStruct2[]? UnknownStruct2Array3;

                    private uint _count7;
                    
                    [Read(15)]
                    public uint Count7
                    {
                        get => _count7;
                        set
                        {
                            _count7 = value;
                            UnknownStruct6Array = new UnknownStruct6[_count7];
                        }
                    }
                    
                    [Read(16, IsCustomType = true)]
                    public UnknownStruct6[]? UnknownStruct6Array;

                    private uint _count8;
                    
                    [Read(17)]
                    public uint Count8
                    {
                        get => _count8;
                        set
                        {
                            _count8 = value;
                            UnknownStruct4Array2 = new UnknownStruct4[_count8];
                        }
                    }
                    
                    [Read(18, IsCustomType = true)]
                    public UnknownStruct4[]? UnknownStruct4Array2;

                    private uint _count9;
                    
                    [Read(19)]
                    public uint Count9
                    {
                        get => _count9;
                        set
                        {
                            _count9 = value;
                            UnknownStruct7Array = new UnknownStruct7[_count9];
                        }
                    }
                    
                    [Read(20, IsCustomType = true)]
                    public UnknownStruct7[]? UnknownStruct7Array;

                    private uint _count10;
                    
                    [Read(21)]
                    public uint Count10
                    {
                        get => _count10;
                        set
                        {
                            _count10 = value;
                            UnknownStruct8Array = new UnknownStruct8[_count10];
                        }
                    }
                    
                    [Read(22, IsCustomType = true)]
                    public UnknownStruct8[]? UnknownStruct8Array;

                    private uint _count11;
                    
                    [Read(23)]
                    public uint Count11
                    {
                        get => _count11;
                        set
                        {
                            _count11 = value;
                            UnknownStruct9Array = new UnknownStruct9[_count11];
                        }
                    }
                    
                    [Read(24, IsCustomType = true)]
                    public UnknownStruct9[]? UnknownStruct9Array;

                    private uint _count12;
                    
                    [Read(25)]
                    public uint Count12
                    {
                        get => _count12;
                        set
                        {
                            _count12 = value;
                            UnknownStruct10Array = new UnknownStruct10[_count12];
                        }
                    }
                    
                    [Read(26, IsCustomType = true)]
                    public UnknownStruct10[]? UnknownStruct10Array;


                    public struct UnknownStruct1
                    {
                        [Read(1)]
                        public WString UnknownWString1;
                        [Read(2)]
                        public uint UnknownUInt1;
                        [Read(3)]
                        public WString UnknownWString2;
                        [Read(4)]
                        public uint UnknownUInt2;
                        [Read(5)]
                        public uint UnknownUInt3;
                    }

                    public struct UnknownStruct2
                    {
                        [Read(1)]
                        public WString UnknownWString1;
                        [Read(2)]
                        public uint UnknownUInt1;
                    }

                    public class UnknownStruct3
                    {
                        [Read(1)]
                        public WString UnknownWString1;
                        [Read(2)]
                        public float UnknownFloat;
                        [Read(3, IsCustomType = true)]
                        public UnknownStruct31[] UnknownStruct31Array = new UnknownStruct31[2];

                        public struct UnknownStruct31
                        {
                            [Read(1)]
                            public WString UnknownWString;
                            [Read(2)]
                            public byte UnknownByte;
                            [Read(3)]
                            public uint UnknownUInt;
                        }
                    }

                    public struct UnknownStruct4
                    {
                        [Read(1)]
                        public WString UnknownWString;
                        [Read(2)]
                        public byte UnknownByte;
                    }

                    public struct UnknownStruct5
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
                        public ushort UnknownUShort1;
                        [Read(6)] 
                        public ushort UnknownUShort2;
                        [Read(7)] 
                        public ushort UnknownUShort3;
                        [Read(8)]
                        public byte UnknownByte1;
                        [Read(9)]
                        public byte UnknownByte2;
                    }

                    public struct UnknownStruct6
                    {
                        [Read(1)]
                        public WString UnknownWString1;

                        private uint _count0;

                        [Read(2)]
                        public uint Count0
                        {
                            get => _count0;
                            set
                            {
                                _count0 = value;
                                UnknownUShortArray1 = new ushort[_count0];
                            }
                        }
                        
                        private uint _count1;

                        [Read(3)]
                        public uint Count1
                        {
                            get => _count1;
                            set
                            {
                                _count1 = value;
                                UnknownUShortArray2 = new ushort[_count1];
                            }
                        }

                        [Read(4)]
                        public ushort[]? UnknownUShortArray1;

                        [Read(5)]
                        public ushort[]? UnknownUShortArray2;
                    }

                    public class UnknownStruct7
                    {
                        [Read(1)]
                        public WString UnknownWString;
                        [Read(2)]
                        public uint[] UnknownUIntArray1 = new uint[4];
                        [Read(3)]
                        public uint[] UnknownUIntArray2 = new uint[4];
                        [Read(4)]
                        public uint[] UnknownUIntArray3 = new uint[4];
                        [Read(5)]
                        public uint UnknownUInt1;
                        [Read(6)]
                        public uint UnknownUInt2;
                    }

                    public class UnknownStruct8
                    {
                        [Read(1)]
                        public WString UnknownWString;

                        [Read(2)]
                        public uint[] UnknownUIntArray10 = new uint[4];
                        [Read(3)]
                        public uint[] UnknownUIntArray11 = new uint[4];
                        [Read(4)]
                        public uint[] UnknownUIntArray12 = new uint[4];
                        
                        [Read(5)]
                        public uint[] UnknownUIntArray20 = new uint[4];
                        [Read(6)]
                        public uint[] UnknownUIntArray21 = new uint[4];
                        [Read(7)]
                        public uint[] UnknownUIntArray22 = new uint[4];
                        
                        [Read(8)]
                        public uint[] UnknownUIntArray30 = new uint[4];
                        [Read(9)]
                        public uint[] UnknownUIntArray31 = new uint[4];
                        [Read(10)]
                        public uint[] UnknownUIntArray32 = new uint[4];
                        
                        [Read(11)]
                        public uint UnknownUInt;
                        [Read(12)]
                        public byte UnknownByte;
                    }

                    public struct UnknownStruct9
                    {
                        [Read(1)]
                        public WString UnknownWString1;
                        [Read(2)]
                        public WString UnknownWString2;
                    }

                    public struct UnknownStruct10
                    {
                        [Read(1)]
                        public ushort UnknownUShort;
                        [Read(2)]
                        public uint UnknownUInt1;
                        [Read(3)]
                        public uint UnknownUInt2;
                        [Read(4)]
                        public uint UnknownUInt3;
                        [Read(5)]
                        public byte UnknownByte;
                        [Read(6)]
                        public uint UnknownUInt4;
                    }
                }
            }
        }

        public class Unknown110 : IUnknownTypeDef
        {
            [Read(1, IsCustomType = true)]
            public Unknown18? Unknown18;
            [Read(2)]
            public float UnknownFloat1;
            [Read(3)]
            public float UnknownFloat2;
            [Read(4)]
            public float UnknownFloat3;
            [Read(5)]
            public float UnknownFloat4;
            [Read(6)]
            public uint UnknownUInt1;
            [Read(7)]
            public RefID UnknownRefID1;
            [Read(8)]
            public RefID UnknownRefID2;
            [Read(9)]
            public uint UnknownUInt2;
        }

        public class Unknown111 : IUnknownTypeDef
        {
            [Read(1, IsCustomType = true)]
            public Unknown18? Unknown18;
            [Read(2)]
            public RefID UnknownRefID;
            [Read(3)]
            public byte UnknownByte;
            [Read(4)]
            public uint[] UnknownUIntArray = new uint[3];
            [Read(5, IsCustomType = true)]
            public Unknown19.Unknown190? Unknown190;
        }
    }
}

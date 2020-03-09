using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Combat : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Combat;

        public uint NextNumber;

        public VSVAL Count0;
        public Unknown0[] Unknown0Array = default!;

        public VSVAL Count1;
        public Unknown1[] Unknown1Array = default!;

        public float UnknownFloat1;
        public VSVAL UnknownVSVAL1;

        public VSVAL Count2;
        public RefID[] UnknownRefIDArray = default!;

        public float UnknownFloat2;
        public UnknownStruct UnknownStruct1;
        public UnknownStruct UnknownStruct2;


        public IGlobalData ReadData(BinaryReader br)
        {
            NextNumber = br.ReadUInt32();

            Count0 = br.ReadVSVAL();
            Unknown0Array = new Unknown0[Count0.Value];

            for (var i = 0; i < Count0; i++)
            {
                Unknown0Array[i] = new Unknown0().ReadData(br);
            }

            Count1 = br.ReadVSVAL();
            Unknown1Array = new Unknown1[Count1.Value];

            for (var i = 0; i < Count1; i++)
            {
                Unknown1Array[i] = new Unknown1().ReadData(br);
            }

            UnknownFloat1 = br.ReadSingle();
            UnknownVSVAL1 = br.ReadVSVAL();

            Count2 = br.ReadVSVAL();
            UnknownRefIDArray = new RefID[Count2.Value];

            for (var i = 0; i < Count2; i++)
            {
                UnknownRefIDArray[i] = br.ReadRefID();
            }

            UnknownFloat2 = br.ReadSingle();
            UnknownStruct1 = new UnknownStruct().ReadData(br);
            UnknownStruct2 = new UnknownStruct().ReadData(br);

            return this;
        }

        public class Unknown0
        {
            public uint UnknownUInt32;
            public uint SerialNumber;
            public Unknown00 Unknown = default!;

            public Unknown0 ReadData(BinaryReader br)
            {
                UnknownUInt32 = br.ReadUInt32();
                SerialNumber = br.ReadUInt32();
                Unknown = new Unknown00().ReadData(br);

                return this;
            }

            public class Unknown00
            {
                public VSVAL Count0;
                public Unknown000[] Unknown000Array = default!;

                public VSVAL Count1;
                public Unknown001[] Unknown001Array = default!;

                public UnknownStruct UnknownStruct1;
                public UnknownStruct UnknownStruct2;
                public UnknownStruct UnknownStruct3;
                public UnknownStruct UnknownStruct4;
                public UnknownStruct[] UnknownStructArray = new UnknownStruct[11];

                public uint UnknownFlag;
                
                /// <summary>
                /// Only present if <see cref="UnknownFlag"/> is not zero
                /// </summary>
                public Unknown002? Unknown002Field;

                public UnknownStruct UnknownStruct5;

                public float UnknownFloat1;
                public float UnknownFloat2;
                public float UnknownFloat3;
                public float UnknownFloat4;

                public UnknownStruct UnknownStruct6;

                public byte UnknownByte;

                public Unknown00 ReadData(BinaryReader br)
                {
                    Count0 = br.ReadVSVAL();
                    Unknown000Array = new Unknown000[Count0.Value];

                    for (var i = 0; i < Count0; i++)
                    {
                        Unknown000Array[i] = new Unknown000().ReadData(br);
                    }

                    Count1 = br.ReadVSVAL();
                    Unknown001Array = new Unknown001[Count0.Value];

                    for (var i = 0; i < Count1; i++)
                    {
                        Unknown001Array[i] = new Unknown001().ReadData(br);
                    }

                    UnknownStruct1 = new UnknownStruct().ReadData(br);
                    UnknownStruct2 = new UnknownStruct().ReadData(br);
                    UnknownStruct3 = new UnknownStruct().ReadData(br);
                    UnknownStruct4 = new UnknownStruct().ReadData(br);

                    for (var i = 0; i < UnknownStructArray.Length; i++)
                    {
                        UnknownStructArray[i] = new UnknownStruct().ReadData(br);
                    }

                    UnknownFlag = br.ReadByte();

                    if(UnknownFlag != 0)
                        Unknown002Field = new Unknown002().ReadData(br);

                    UnknownStruct5 = new UnknownStruct().ReadData(br);

                    UnknownFloat1 = br.ReadSingle();
                    UnknownFloat2 = br.ReadSingle();
                    UnknownFloat3 = br.ReadSingle();
                    UnknownFloat4 = br.ReadSingle();

                    UnknownStruct6 = new UnknownStruct().ReadData(br);
                    UnknownByte = br.ReadByte();

                    return this;
                }

                public class Unknown000
                {
                    public RefID UnknownRefID;
                    public uint UnknownUInt32;
                    public float UnknownFloat1;
                    public ushort UnknownUshort1;
                    public ushort UnknownUshort2;

                    public RefID Target;

                    public Position UnknownPosition1;
                    public Position UnknownPosition2;
                    public Position UnknownPosition3;
                    public Position UnknownPosition4;
                    public Position UnknownPosition5;

                    public float UnknownFloat2;
                    public float UnknownFloat3;
                    public float UnknownFloat4;
                    public float UnknownFloat5;
                    public float UnknownFloat6;
                    public float UnknownFloat7;

                    public Unknown000 ReadData(BinaryReader br)
                    {
                        UnknownRefID = br.ReadRefID();
                        UnknownUInt32 = br.ReadUInt32();
                        UnknownFloat1 = br.ReadSingle();
                        UnknownUshort1 = br.ReadUInt16();
                        UnknownUshort2 = br.ReadUInt16();

                        Target = br.ReadRefID();

                        UnknownPosition1 = new Position().ReadData(br);
                        UnknownPosition2 = new Position().ReadData(br);
                        UnknownPosition3 = new Position().ReadData(br);
                        UnknownPosition4 = new Position().ReadData(br);
                        UnknownPosition5 = new Position().ReadData(br);

                        UnknownFloat2 = br.ReadSingle();
                        UnknownFloat3 = br.ReadSingle();
                        UnknownFloat4 = br.ReadSingle();
                        UnknownFloat5 = br.ReadSingle();
                        UnknownFloat6 = br.ReadSingle();
                        UnknownFloat7 = br.ReadSingle();

                        return this;
                    }
                }

                public class Unknown001
                {
                    public RefID UnknownRefID;
                    public float UnknownFloat1;
                    public float UnknownFloat2;

                    public Unknown001 ReadData(BinaryReader br)
                    {
                        UnknownRefID = br.ReadRefID();
                        UnknownFloat1 = br.ReadSingle();
                        UnknownFloat2 = br.ReadSingle();

                        return this;
                    }
                }

                public class Unknown002
                {
                    public RefID UnknownRefID;
                    public UnknownStruct UnknownStruct1;
                    public UnknownStruct UnknownStruct2;

                    public float UnknownFloat1;
                    public Position UnknownPosition;
                    public float UnknownFloat2;

                    public VSVAL Count0;
                    public Unknown0020[] Unknown0020Array = default!;
                    public VSVAL Count1;
                    public Unknown0021[] Unknown0021Array = default!;

                    public byte UnknownFlag;

                    /// <summary>
                    /// Only present if <see cref="UnknownFlag"/> is not zero
                    /// </summary>
                    public Unknown0022? Unknown0022Field;

                    public Unknown002 ReadData(BinaryReader br)
                    {
                        UnknownRefID = br.ReadRefID();
                        UnknownStruct1 = new UnknownStruct
                        {
                            UnknownFloat1 = br.ReadSingle(),
                            UnknownFloat2 = br.ReadSingle()
                        };

                        UnknownStruct2 = new UnknownStruct
                        {
                            UnknownFloat1 = br.ReadSingle(),
                            UnknownFloat2 = br.ReadSingle()
                        };

                        UnknownFloat1 = br.ReadSingle();
                        UnknownPosition = new Position().ReadData(br);
                        UnknownFloat2 = br.ReadSingle();

                        Count0 = br.ReadVSVAL();
                        Unknown0020Array = new Unknown0020[Count0.Value];

                        for (var i = 0; i < Count0; i++)
                        {
                            Unknown0020Array[i] = new Unknown0020().ReadData(br);
                        }

                        Count1 = br.ReadVSVAL();
                        Unknown0021Array = new Unknown0021[Count1.Value];

                        for (var i = 0; i < Count1; i++)
                        {
                            Unknown0021Array[i] = new Unknown0021().ReadData(br);
                        }

                        UnknownFlag = br.ReadByte();

                        if(UnknownFlag != 0)
                            Unknown0022Field = new Unknown0022().ReadData(br);

                        return this;
                    }

                    public class Unknown0020
                    {
                        public Position UnknownPosition;
                        public uint UnknownUInt;
                        public float UnknownFloat;

                        public Unknown0020 ReadData(BinaryReader br)
                        {
                            UnknownPosition = new Position().ReadData(br);
                            UnknownUInt = br.ReadUInt32();
                            UnknownFloat = br.ReadSingle();
                            
                            return this;
                        }
                    }

                    public class Unknown0021
                    {
                        public RefID UnknownRefID1;
                        public RefID UnknownRefID2;

                        public byte UnknownByte1;
                        public byte UnknownByte2;
                        public byte UnknownByte3;

                        public Unknown0021 ReadData(BinaryReader br)
                        {
                            UnknownRefID1 = br.ReadRefID();
                            UnknownRefID2 = br.ReadRefID();

                            UnknownByte1 = br.ReadByte();
                            UnknownByte2 = br.ReadByte();
                            UnknownByte3 = br.ReadByte();

                            return this;
                        }
                    }

                    public class Unknown0022
                    {
                        public uint UnknownUInt1;
                        public uint UnknownUInt2;

                        public uint Count0;
                        public Unknown00220[] Unknown00220Array = default!;

                        public RefID UnknownRefID1;
                        public float UnknownFloat1;
                        public float UnknownFloat2;
                        public float UnknownFloat3;
                        public float UnknownFloat4;

                        public float UnknownFloat5;
                        public RefID UnknownRefID2;
                        
                        public float UnknownFloat6;
                        public RefID UnknownRefID3;

                        public ushort UnknownUShort;
                        public byte UnknownByte1;
                        public byte UnknownByte2;

                        public float UnknownFloat7;
                        public float UnknownFloat8;


                        public Unknown0022 ReadData(BinaryReader br)
                        {
                            UnknownUInt1 = br.ReadUInt32();
                            UnknownUInt2 = br.ReadUInt32();

                            Count0 = br.ReadUInt32();
                            Unknown00220Array = new Unknown00220[Count0];

                            for (var i = 0; i < Count0; i++)
                            {
                                Unknown00220Array[i] = new Unknown00220().ReadData(br);
                            }

                            UnknownRefID1 = br.ReadRefID();
                            UnknownFloat1 = br.ReadSingle();
                            UnknownFloat2 = br.ReadSingle();
                            UnknownFloat3 = br.ReadSingle();
                            UnknownFloat4 = br.ReadSingle();

                            
                            UnknownFloat5 = br.ReadSingle();
                            UnknownRefID2 = br.ReadRefID();

                            UnknownFloat6 = br.ReadSingle();
                            UnknownRefID3 = br.ReadRefID();

                            UnknownUShort = br.ReadUInt16();
                            UnknownByte1 = br.ReadByte();
                            UnknownByte2 = br.ReadByte();

                            UnknownFloat7 = br.ReadSingle();
                            UnknownFloat8 = br.ReadSingle();

                            return this;
                        }

                        public class Unknown00220
                        {
                            public byte UnknownByte;
                            public uint Count0;
                            public byte[] UnknownByteArray = default!;
                            public RefID UnknownRefID;
                            public uint UnknownUInt32;

                            public Unknown00220 ReadData(BinaryReader br)
                            {
                                UnknownByte = br.ReadByte();
                                Count0 = br.ReadUInt32();
                                UnknownByteArray = new byte[Count0];

                                for (var i = 0; i < Count0; i++)
                                {
                                    UnknownByteArray[i] = br.ReadByte();
                                }

                                UnknownRefID = br.ReadRefID();
                                UnknownUInt32 = br.ReadUInt32();

                                return this;
                            }
                        }
                    }
                }

                public struct Position
                {
                    public float X;
                    public float Y;
                    public float Z;
                    public RefID CellID;

                    public Position ReadData(BinaryReader br)
                    {
                        X = br.ReadSingle();
                        Y = br.ReadSingle();
                        Z = br.ReadSingle();
                        CellID = br.ReadRefID();

                        return this;
                    }
                }
            }
        }

        public class Unknown1
        {
            public RefID UnknownRefID;
            public float UnknownFloat;
            public Unknown10 Unknown = default!;

            public Unknown1 ReadData(BinaryReader br)
            {
                UnknownRefID = br.ReadRefID();
                UnknownFloat = br.ReadSingle();
                Unknown = new Unknown10().ReadData(br);

                return this;
            }

            public class Unknown10
            {
                public RefID UnknownRefID1;
                public RefID UnknownRefID2;

                public float UnknownFloat1;
                public float UnknownFloat2;
                public float UnknownFloat3;

                public float X;
                public float Y;
                public float Z;

                public float UnknownFloat4;
                public float UnknownFloat5;
                public float UnknownFloat6;
                public float UnknownFloat7;
                public float UnknownFloat8;
                public float UnknownFloat9;
                public float UnknownFloat10;
                public float UnknownFloat11;

                public RefID UnknownRefID3;

                public Unknown10 ReadData(BinaryReader br)
                {
                    UnknownRefID1 = br.ReadRefID();
                    UnknownRefID2 = br.ReadRefID();

                    UnknownFloat1 = br.ReadSingle();
                    UnknownFloat2 = br.ReadSingle();
                    UnknownFloat3 = br.ReadSingle();

                    X = br.ReadSingle();
                    Y = br.ReadSingle();
                    Z = br.ReadSingle();

                    UnknownFloat4 = br.ReadSingle();
                    UnknownFloat5 = br.ReadSingle();
                    UnknownFloat6 = br.ReadSingle();
                    UnknownFloat7 = br.ReadSingle();
                    UnknownFloat8 = br.ReadSingle();
                    UnknownFloat9 = br.ReadSingle();
                    UnknownFloat10 = br.ReadSingle();
                    UnknownFloat11 = br.ReadSingle();

                    UnknownRefID3 = br.ReadRefID();

                    return this;
                }
            }
        }

        public struct UnknownStruct
        {
            public float UnknownFloat1;
            public float UnknownFloat2;

            public UnknownStruct ReadData(BinaryReader br)
            {
                UnknownFloat1 = br.ReadSingle();
                UnknownFloat2 = br.ReadSingle();

                return this;
            }
        }
    }
}

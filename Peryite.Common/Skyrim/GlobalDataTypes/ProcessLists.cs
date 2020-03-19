using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class ProcessLists : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.ProcessLists;

        [Read(1)]
        public float Unknown1;
        [Read(2)]
        public float Unknown2;
        [Read(3)]
        public float Unknown3;
        [Read(4)]
        public uint NextNumber;
        [Read(5, IsCustomType = true)]
        public CrimeGroup[] AllCrimes = new CrimeGroup[7];

        public class CrimeGroup
        {
            private VSVAL _count;

            [Read(1)]
            public VSVAL Count
            {
                get => _count;
                set
                {
                    _count = value;
                    Crimes = new Crime[_count.Value];
                }
            }
            
            [Read(2, IsCustomType = true)]
            public Crime[]? Crimes;
        }

        public enum CrimeType : uint
        {
            Theft = 0,
            Pickpocketing = 1,
            Trespassing = 2,
            Assault = 3,
            Murder = 4,
            Unknown = 5,
            Lycanthropy = 6
        }

        public class Crime// : ICustomRead
        {
            [Read(1)]
            public uint WitnessNumber;

            [Read(2, EnumType = typeof(uint))]
            public CrimeType CrimeType;

            [Read(3)]
            public byte UnknownByte1;

            /// <summary>
            /// Number of stolen items, only for thefts
            /// </summary>
            [Read(4)]
            [ConditionalParsing(Type = typeof(CrimeType), And = false, Chaining = new []
            {
                (object)0, 1
            }, ChainingType = typeof(uint))]
            public uint Quantity;

            /// <summary>
            /// Assigned in accordance with <see cref="NextNumber"/>
            /// </summary>
            [Read(5)]
            public uint SerialNumber;
            
            [Read(6)]
            public byte UnknownByte2;

            /// <summary>
            /// Maybe date of crime? Little byte is the day
            /// </summary>
            [Read(7)]
            public uint UnknownUInt;

            /// <summary>
            /// Negative value measured from moment of crime
            /// </summary>
            [Read(8)]
            public float ElapsedTime;
            
            [Read(9)]
            public RefID VictimID;
            [Read(10)]
            public RefID CriminalID;

            /// <summary>
            /// Only for thefts
            /// </summary>
            [Read(11)]
            [ConditionalParsing(Type = typeof(CrimeType), And = false, Chaining = new []
            {
                (object)0, 1
            }, ChainingType = typeof(uint))]
            public RefID ItemBaseID;

            /// <summary>
            /// Only for thefts 
            /// </summary>
            [Read(12)]
            [ConditionalParsing(Type = typeof(CrimeType), And = false, Chaining = new[]
            {
                (object)0, 1
            }, ChainingType = typeof(uint))]
            public RefID OwnershipID;

            private VSVAL _witnessCount;

            [Read(13)]
            public VSVAL WitnessCount
            {
                get => _witnessCount;
                set
                {
                    _witnessCount = value;
                    Witnesses = new RefID[_witnessCount.Value];
                }
            }

            [Read(14)]
            public RefID[]? Witnesses;
            [Read(15)]
            public uint Bounty;
            [Read(16)]
            public RefID CrimeFactionID;

            /// <summary>
            /// 0 = active crime, 1 = it was atoned
            /// </summary>
            [Read(17)]
            public byte IsCleared;
            
            [Read(18)]
            public ushort UnknownUShort;

            /*public void ReadData(BinaryReader br)
            {
                WitnessNumber = br.ReadUInt32();
                CrimeType = (CrimeType) br.ReadUInt32();
                UnknownByte1 = br.ReadByte();

                if (CrimeType == CrimeType.Theft || CrimeType == CrimeType.Pickpocketing)
                    Quantity = br.ReadUInt32();

                SerialNumber = br.ReadUInt32();
                UnknownByte2 = br.ReadByte();
                UnknownUInt = br.ReadUInt32();
                ElapsedTime = br.ReadSingle();
                VictimID = br.ReadRefID();
                CriminalID = br.ReadRefID();

                if (CrimeType == CrimeType.Theft || CrimeType == CrimeType.Pickpocketing)
                {
                    ItemBaseID = br.ReadRefID();
                    OwnershipID = br.ReadRefID();
                }

                if (CrimeType == CrimeType.Trespassing)
                    OwnershipID = br.ReadRefID();

                WitnessCount = br.ReadVSVAL();
                Witnesses = new RefID[WitnessCount.Value];
                for (var i = 0; i < WitnessCount; i++)
                {
                    Witnesses[i] = br.ReadRefID();
                }

                Bounty = br.ReadUInt32();
                CrimeFactionID = br.ReadRefID();
                IsCleared = br.ReadByte();
                UnknownUShort = br.ReadUInt16();
            }*/
        }
    }
}

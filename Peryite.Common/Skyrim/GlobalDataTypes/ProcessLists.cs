using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class ProcessLists : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.ProcessLists;

        public float Unknown1;
        public float Unknown2;
        public float Unknown3;
        public uint NextNumber;
        public CrimeGroup[] AllCrimes = new CrimeGroup[7];

        public IGlobalData ReadData(BinaryReader br)
        {
            Unknown1 = br.ReadSingle();
            Unknown2 = br.ReadSingle();
            Unknown3 = br.ReadSingle();

            NextNumber = br.ReadUInt32();

            for (var i = 0; i < AllCrimes.Length; i++)
            {
                var res = new CrimeGroup
                {
                    Count = br.ReadVSVAL()
                };

                res.Crimes = new Crime[res.Count.Value];

                for (var j = 0; j < res.Count; j++)
                {
                    res.Crimes[j] = new Crime().ReadData(br);
                }

                AllCrimes[i] = res;
            }

            return this;
        }

        public class CrimeGroup
        {
            public VSVAL Count;
            public Crime[] Crimes = default!;
        }

        public enum CrimeType
        {
            Theft = 0,
            Pickpocketing = 1,
            Trespassing = 2,
            Assault = 3,
            Murder = 4,
            Unknown = 5,
            Lycanthropy = 6
        }

        public class Crime
        {
            public uint WitnessNumber;
            public CrimeType CrimeType;
            public byte Unknown1;

            /// <summary>
            /// Number of stolen items, only for thefts
            /// </summary>
            public uint Quantity;

            /// <summary>
            /// Assigned in accordance with <see cref="ProcessLists.NextNumber"/>
            /// </summary>
            public uint SerialNumber;

            public byte Unknown2;

            /// <summary>
            /// Maybe date of crime? Little byte is the day
            /// </summary>
            public uint Unknown3;

            /// <summary>
            /// Negative value measured from moment of crime
            /// </summary>
            public float ElapsedTime;

            public RefID VictimID;
            public RefID CriminalID;

            /// <summary>
            /// Only for thefts
            /// </summary>
            public RefID ItemBaseID;

            /// <summary>
            /// Only for thefts 
            /// </summary>
            public RefID OwnershipID;

            public VSVAL WitnessCount;
            public RefID[] Witnesses = default!;
            public uint Bounty;
            public RefID CrimeFactionID;

            /// <summary>
            /// 0 = active crime, 1 = it was atoned
            /// </summary>
            public byte IsCleared;

            public ushort Unknown4;

            public Crime ReadData(BinaryReader br)
            {
                WitnessNumber = br.ReadUInt32();
                CrimeType = (CrimeType) br.ReadUInt32();
                Unknown1 = br.ReadByte();

                if (CrimeType == CrimeType.Theft || CrimeType == CrimeType.Pickpocketing)
                    Quantity = br.ReadUInt32();

                SerialNumber = br.ReadUInt32();
                Unknown2 = br.ReadByte();
                Unknown3 = br.ReadUInt32();
                ElapsedTime = br.ReadSingle();

                VictimID = br.ReadRefID();
                CriminalID = br.ReadRefID();

                if (CrimeType == CrimeType.Theft || CrimeType == CrimeType.Pickpocketing)
                {
                    ItemBaseID = br.ReadRefID();
                    OwnershipID = br.ReadRefID();
                }

                WitnessCount = br.ReadVSVAL();
                Witnesses = new RefID[WitnessCount.Value];

                for (var i = 0; i < WitnessCount; i++)
                {
                    Witnesses[i] = br.ReadRefID();
                }

                Bounty = br.ReadUInt32();
                CrimeFactionID = br.ReadRefID();
                IsCleared = br.ReadByte();
                Unknown4 = br.ReadUInt16();

                return this;
            }
        }
    }
}

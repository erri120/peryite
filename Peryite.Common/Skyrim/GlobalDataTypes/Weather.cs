using System;
using System.IO;

namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Weather : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Weather;

        public RefID Climate;
        public RefID CurrentWeather;
        public RefID PrevWeather;
        public RefID UnknownWeather1;
        public RefID UnknownWeather2;
        public RefID UnknownWeather3;

        public float CurrentTime;
        public float StartTime;
        public float WeatherTransition;

        public uint Unknown1;
        public uint Unknown2;
        public uint Unknown3;
        public uint Unknown4;
        public uint Unknown5;
        public uint Unknown6;
        public float Unknown7;
        public uint Unknown8;

        public byte Flags;

        public IGlobalData ReadData(BinaryReader br)
        {
            Climate = br.ReadRefID();
            CurrentWeather = br.ReadRefID();
            PrevWeather = br.ReadRefID();
            UnknownWeather1 = br.ReadRefID();
            UnknownWeather2 = br.ReadRefID();
            UnknownWeather3 = br.ReadRefID();

            CurrentTime = br.ReadSingle();
            StartTime = br.ReadSingle();
            WeatherTransition = br.ReadSingle();

            Unknown1 = br.ReadUInt32();
            Unknown2 = br.ReadUInt32();
            Unknown3 = br.ReadUInt32();
            Unknown4 = br.ReadUInt32();
            Unknown5 = br.ReadUInt32();
            Unknown6 = br.ReadUInt32();
            Unknown7 = br.ReadSingle();
            Unknown8 = br.ReadUInt32();

            Flags = br.ReadByte();

            return this;
        }
    }
}

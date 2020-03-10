namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class Weather : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.Weather;

        [Read(1)]
        public RefID Climate;
        [Read(2)]
        public RefID CurrentWeather;
        [Read(3)]
        public RefID PrevWeather;
        [Read(4)]
        public RefID UnknownWeather1;
        [Read(5)]
        public RefID UnknownWeather2;
        [Read(6)]
        public RefID UnknownWeather3;
        
        [Read(7)]
        public float CurrentTime;
        [Read(8)]
        public float StartTime;
        [Read(9)]
        public float WeatherTransition;

        [Read(10)]
        public uint Unknown1;
        [Read(11)]
        public uint Unknown2;
        [Read(12)]
        public uint Unknown3;
        [Read(13)]
        public uint Unknown4;
        [Read(14)]
        public uint Unknown5;
        [Read(15)]
        public uint Unknown6;
        [Read(16)]
        public float Unknown7;
        [Read(17)]
        public uint Unknown8;
        
        [Read(18)]
        public byte Flags;
    }
}

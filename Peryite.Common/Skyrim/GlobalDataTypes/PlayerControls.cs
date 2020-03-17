namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class PlayerControls : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.PlayerControls;

        [Read(1)]
        public byte UnknownByte1;
        [Read(2)]
        public byte UnknownByte2;
        [Read(3)]
        public byte UnknownByte3;
        [Read(4)]
        public ushort UnknownUShort;
        [Read(5)]
        public byte UnknownByte4;
    }
}

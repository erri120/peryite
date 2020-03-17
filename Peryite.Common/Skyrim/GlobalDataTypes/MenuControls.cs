namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class MenuControls : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.MenuControls;

        [Read(1)]
        public byte UnknownByte1;
        [Read(2)]
        public byte UnknownByte2;
    }
}

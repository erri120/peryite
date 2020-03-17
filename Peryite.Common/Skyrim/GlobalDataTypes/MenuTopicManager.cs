namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class MenuTopicManager : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.MenuTopicManager;
        
        [Read(1)]
        public RefID UnknownRefID1;
        [Read(2)]
        public RefID UnknownRefID2;
    }
}

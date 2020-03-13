namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class StoryTeller : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.StoryTeller;

        [Read(1)]
        public byte Flag;
    }
}

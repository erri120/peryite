namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class StoryEventManager : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.StoryEventManager;

        [Read(1)]
        public uint UnknownUInt;

        private VSVAL _count;
        
        [Read(2)]
        public VSVAL Count
        {
            get => _count;
            set
            {
                _count = value;
                if(_count > 0)
                    throw new CorruptedSaveFileException("Count is greater than zero for StoryEventManager. Please open a new issue on the GitHub page containing your save file.");
            }
        }

        //public Unknown[]? Unknown;
    }
}

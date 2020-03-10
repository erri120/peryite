namespace Peryite.Common.Skyrim.GlobalDataTypes
{
    public class MiscStats : IGlobalData
    {
        public GlobalDataType Type => GlobalDataType.MiscStats;

        private uint _count;

        [Read(1)]
        public uint Count
        {
            get => _count;
            set
            {
                _count = value;
                Stats = new MiscStat[_count];
            }
        }

        [Read(2)]
        public MiscStat[]? Stats;
    }

    public enum MiscStatCategory
    {
        General = 0,
        Quest = 1,
        Combat = 2,
        Magic = 3,
        Crafting = 4,
        Crime = 5,
        DLC = 6
    }

    public struct MiscStat
    {
        [Read(1)]
        public WString Name;

        [Read(2, EnumType = typeof(byte))]
        public MiscStatCategory Category;

        [Read(3)]
        public int Value;
    }
}
